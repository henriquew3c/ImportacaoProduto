using System;
using _2.API.Models;
using MediatR;

namespace _2.API.Application
{
    public class GetImportacaoRequest : IRequest<Importacao>
    {
        public Guid Id { get; set; }
    }
}