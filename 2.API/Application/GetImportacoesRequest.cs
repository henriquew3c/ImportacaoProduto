

using _2.API.Models;
using _Support;
using MediatR;

namespace _2.API.Application
{
    public class GetImportacoesRequest : PaginationRequest, IRequest<PaginationResult<Importacao>>
    {
    }
}