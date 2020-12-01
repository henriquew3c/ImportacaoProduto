using System.Collections.Generic;
using _2.API.Models;
using MediatR;

namespace _2.API.Application
{
    public class GetImportacoesRequest : IRequest<IEnumerable<Importacao>>
    {
    }
}