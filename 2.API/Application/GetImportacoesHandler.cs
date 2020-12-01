using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using _2.API.Models;
using _2.API.Repository;
using _Support;
using MediatR;

namespace _2.API.Application
{
    internal class GetImportacoesHandler : IRequestHandler<GetImportacoesRequest, IEnumerable<Importacao>>
    {
        private readonly IImportacaoRepository _importacaoRepository;

        public GetImportacoesHandler(IImportacaoRepository importacaoRepository)
        {
            _importacaoRepository = importacaoRepository;
        }

        public Task<IEnumerable<Importacao>> Handle(GetImportacoesRequest request, CancellationToken cancellationToken)
        {
            return Task.FromResult(_importacaoRepository.GetImportacoes());
        }
    }
}
