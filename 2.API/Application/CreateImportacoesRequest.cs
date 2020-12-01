using System.Collections.Generic;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace _2.API.Application
{
    public class CreateImportacoesRequest : List<string>, IRequest<List<string>>
    {
        public CreateImportacoesRequest(IFormFile arquivo)
        {
            Arquivo = arquivo;
        }

        public IFormFile Arquivo { get; }
    }
}
