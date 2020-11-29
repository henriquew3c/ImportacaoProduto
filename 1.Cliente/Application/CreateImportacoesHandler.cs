using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Threading;
using System.Threading.Tasks;
using _1.Cliente.Models;
using MediatR;
using Microsoft.AspNetCore.Http;

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
