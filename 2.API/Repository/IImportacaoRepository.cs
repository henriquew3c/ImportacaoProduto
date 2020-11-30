using _2.API.Models;
using _2.API.Repository.Default;
using _Support;

namespace _2.API.Repository
{
    public interface IImportacaoRepository : IDefaultRepository<Importacao>
    {
        PaginationResult<Importacao> GetImportacoes(PaginationRequest request);

    }
}
