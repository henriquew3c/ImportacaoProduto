using MediatR;
using Microsoft.AspNetCore.Http;

namespace _2.API.Application
{
    public class CreateImportacoesRequest : IRequest
    {
        public CreateImportacoesRequest(IFormFile arquivo)
        {
            Arquivo = arquivo;
        }

        public IFormFile Arquivo { get; }
    }
}
