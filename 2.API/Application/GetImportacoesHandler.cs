using System.Threading;
using System.Threading.Tasks;
using _2.API.Models;
using _2.API.Repository;
using _Support;
using MediatR;

namespace _2.API.Application
{
    internal class GetImportacoesHandler : IRequestHandler<GetImportacoesRequest, PaginationResult<Importacao>>
    {
        private readonly IImportacaoRepository _importacaoRepository;

        public GetImportacoesHandler(IImportacaoRepository importacaoRepository)
        {
            _importacaoRepository = importacaoRepository;
        }

        public Task<PaginationResult<Importacao>> Handle(GetImportacoesRequest request, CancellationToken cancellationToken)
        {
            var importacoes = _importacaoRepository.GetImportacoes(request);

            return Task.FromResult(importacoes);
        }
    }
}
