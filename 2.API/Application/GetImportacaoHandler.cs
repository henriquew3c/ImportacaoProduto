using System.Threading;
using System.Threading.Tasks;
using _2.API.Models;
using _2.API.Repository;
using MediatR;

namespace _2.API.Application
{
    internal class GetImportacaoHandler : IRequestHandler<GetImportacaoRequest, Importacao>
    {
        private readonly IImportacaoRepository _importacaoRepository;

        public GetImportacaoHandler(IImportacaoRepository importacaoRepository)
        {
            _importacaoRepository = importacaoRepository;
        }

        public Task<Importacao> Handle(GetImportacaoRequest request, CancellationToken cancellationToken)
        {
            return Task.FromResult(_importacaoRepository.Find(request.Id));
        }
    }
}