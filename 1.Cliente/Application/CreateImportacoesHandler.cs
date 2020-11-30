using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace _1.Cliente.Application
{
    internal class CreateImportacoesHandler : IRequestHandler<CreateImportacoesRequest>
    {
        public Task<Unit> Handle(CreateImportacoesRequest request, CancellationToken cancellationToken)
        {

            //envia arquivo para a api

            throw new NotImplementedException();
        }
    }
}
