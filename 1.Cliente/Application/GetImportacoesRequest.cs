using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _1.Cliente.Models;
using MediatR;

namespace _1.Cliente.Application
{
    public class GetImportacoesRequest : IRequest<List<Importacao>>
    {

    }
}
