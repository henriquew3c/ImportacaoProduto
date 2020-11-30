using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using _1.Cliente.Models;
using MediatR;

namespace _1.Cliente.Application
{
    internal class ListIgrejaHandler : IRequestHandler<GetImportacoesRequest, List<Importacao>>
    {
        public async Task<List<Importacao>> Handle(GetImportacoesRequest request, CancellationToken cancellationToken)
        {
            var importacoes = new List<Importacao>
            {
                new Importacao
                {
                    DataImportacao = DateTime.Now,
                    QuantidadeProdutosImportados = 10,
                    Id = Guid.NewGuid()
                },
                new Importacao
                {
                    DataImportacao = DateTime.Now,
                    QuantidadeProdutosImportados = 10,
                    Id = Guid.NewGuid()
                },
                new Importacao
                {
                    DataImportacao = DateTime.Now,
                    QuantidadeProdutosImportados = 10,
                    Id = Guid.NewGuid()
                }
            };

            return importacoes;
        }
    }
}
