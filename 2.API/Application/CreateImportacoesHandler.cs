using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using _2.API.Infra;
using _2.API.Models;
using _2.API.Repository;
using MediatR;
using OfficeOpenXml;

namespace _2.API.Application
{
    internal class CreateImportacoesHandler : AsyncRequestHandler<CreateImportacoesRequest>
    {
        private readonly IImportacaoRepository _importacaoRepository;
        private readonly IUnityOfWork _unityOfWork;

        public CreateImportacoesHandler(
            IImportacaoRepository importacaoRepository,
            IUnityOfWork unityOfWork)
        {
            _importacaoRepository = importacaoRepository;
            _unityOfWork = unityOfWork;
        }

        protected override Task Handle(CreateImportacoesRequest request, CancellationToken cancellationToken)
        {
            if (request.Arquivo == null)
            {
                throw new ArgumentException("O Arquivo deve ser informado.");
            }

            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            var produtos = new List<Produto>();
            var importacao = new Importacao(Guid.NewGuid(), DateTime.Now);
            var mensagemsErro = new List<string>();

            using (var package = new ExcelPackage(request.Arquivo.OpenReadStream()))
            {
                foreach (ExcelWorksheet worksheet in package.Workbook.Worksheets)
                {
                    //loop all rows
                    for (var i = 2; i <= worksheet.Dimension.End.Row; i++)
                    {
                        //loop all columns in a row
                        var dataEntrega = worksheet.Cells[i, 1].Text;
                        var nome = worksheet.Cells[i, 2].Text;
                        var quantidade = worksheet.Cells[i, 3].Text;
                        var valorUnitario = worksheet.Cells[i, 4].Text;

                        var errosAtuais = new List<string>();

                        if (dataEntrega == null)
                        {
                            errosAtuais.Add($"A data de entrega do produto não pode ser nula. Erro na Linha: {i}.");
                        }

                        if (DataIsValid(dataEntrega) == false)
                        {
                            errosAtuais.Add($"A data de entrega do produto não é uma data válida. Erro na Linha: {i}.");
                        }

                        if (nome == null)
                        {
                            errosAtuais.Add($"O nome do produto não pode ser nulo. Erro na Linha: {i}.");
                        }

                        if (quantidade == null)
                        {
                            errosAtuais.Add($"A quantidade do produto não pode ser nulo. Erro na Linha: {i}.");
                        }

                        if (int.TryParse(quantidade, out _) == false)
                        {
                            errosAtuais.Add($"A quantidade do produto deve corresponder a um valor inteiro. Erro na Linha: {i}.");
                        }

                        if (double.TryParse(valorUnitario, out _) == false)
                        {
                            errosAtuais.Add($"O valor unitário do produto deve corresponder a um valor flutuante (ex: 10,1 ou 10,0). Erro na Linha: {i}.");
                        }

                        if (errosAtuais.Count != 0)
                        {
                            mensagemsErro.AddRange(errosAtuais);
                        }

                        if (errosAtuais.Count == 0)
                        {
                            var produto = new Produto(
                                Guid.NewGuid(),
                                DateTime.Parse(dataEntrega),
                                nome,
                                int.Parse(quantidade),
                                double.Parse(valorUnitario),
                                importacao
                            );

                            importacao.QuantidadeProdutosImportados++;

                            produtos.Add(produto);
                        }
                    }

                }
            }

            importacao.Produtos = produtos;
            _importacaoRepository.Store(importacao);

            return _unityOfWork.SaveChangesIfDomainIsValid();
        }

        private static bool DataIsValid(string data)
        {
            var regex = new Regex(@"(((0|1)[0-9]|2[0-9]|3[0-1])\/(0[1-9]|1[0-2])\/((19|20)\d\d))$");

            //Verify whether date entered in dd/MM/yyyy format.
            return regex.IsMatch(data.Trim());
        }
    }
}
