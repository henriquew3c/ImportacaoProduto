using System.Collections.Generic;
using _1.Cliente.Models;
using MediatR;

namespace _1.Cliente.Application
{
    public class GetImportacoesRequest : IRequest<List<Importacao>>
    {

    }
}
