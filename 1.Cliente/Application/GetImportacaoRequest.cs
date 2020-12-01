using System;
using System.Collections.Generic;
using _1.Cliente.Models;
using MediatR;

namespace _1.Cliente.Application
{
    public class GetImportacaoRequest : IRequest<Importacao>
    {
        public Guid Id { get; set; }
    }
}
