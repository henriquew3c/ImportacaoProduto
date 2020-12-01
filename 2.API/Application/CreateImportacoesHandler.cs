using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using _2.API.Infra;
using _2.API.Models;
using _2.API.Repository;
using _Support;
using MediatR;
using OfficeOpenXml;

namespace _2.API.Application
{
    internal class CreateImportacoesHandler : IRequestHandler<CreateImportacoesRequest, List<string>>
    {
        private readonly IImportacaoRepository _importacaoRepository;
        private readonly IDomainValidation _domainValidation;
        private readonly IUnityOfWork _unityOfWork;

        public CreateImportacoesHandler(
            IImportacaoRepository importacaoRepository,
            IDomainValidation _domainValidation,
            IUnityOfWork unityOfWork)
        {
            _importacaoRepository = importacaoRepository;
            this._domainValidation = _domainValidation;
            _unityOfWork = unityOfWork;
        }

        public async Task<List<string>> Handle(CreateImportacoesRequest request, CancellationToken cancellationToken)
        {
            var mensagemsErro = new List<string>();

            if (request.Arquivo == null)
            {
                mensagemsErro.Add("O Arquivo deve ser informado.");
                return mensagemsErro;
            }

            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            var produtos = new List<Produto>();
            var importacao = new Importacao(Guid.NewGuid(), DateTime.Now);

            using (var package = new ExcelPackage(request.Arquivo.OpenReadStream()))
            {
                foreach (var worksheet in package.Workbook.Worksheets)
                {
                    //loop all rows
                    for (var row = 2; row <= worksheet.Dimension.End.Row; row++)
                    {
                        //loop all columns in a row
                        var produto = 
                            GetProdutoByRow(worksheet, row);

                        var erros= produto.Valida(row);

                        if (erros.Count != 0)
                        {
                            mensagemsErro.AddRange(erros);
                        }

                        if (erros.Count != 0) continue;

                        produto.Importacao = importacao;
                        importacao.QuantidadeProdutosImportados++;
                        produtos.Add(produto);
                    }

                }
            }

            if (mensagemsErro.Count != 0)
            {
                _domainValidation.AddError("Erros foram encontrados!");
                mensagemsErro.Add("Nenhum produto foi importado, corrija a planilha e tente novamente.");
            }

            importacao.Produtos = produtos;

            _importacaoRepository.Store(importacao);

            await _unityOfWork.SaveChangesIfDomainIsValid();

            return mensagemsErro;
        }

        private static Produto GetProdutoByRow(ExcelWorksheet worksheet, int row)
        {
            var dataEntrega = worksheet.Cells[row, 1].Text;
            var nome = worksheet.Cells[row, 2].Text;
            var quantidade = worksheet.Cells[row, 3].Text;
            var valorUnitario = worksheet.Cells[row, 4].Text;

            return new Produto(
                Guid.NewGuid(),
                DateTime.Parse(dataEntrega),
                nome,
                int.Parse(quantidade),
                double.Parse(valorUnitario)
            );
        }
    }
}
