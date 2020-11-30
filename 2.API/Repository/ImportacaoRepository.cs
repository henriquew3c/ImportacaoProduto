using System.Linq;
using _2.API.Infra;
using _2.API.Models;
using _2.API.Repository.Default;
using _Support;
using _Support.Extensions;

namespace _2.API.Repository
{
    internal class ImportacaoRepository : DefaultRepository<Importacao>, IImportacaoRepository
    {
        private readonly IApplicationDbContext _dbContext;

        public ImportacaoRepository(IApplicationDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public PaginationResult<Importacao> GetImportacoes(PaginationRequest paginationRequest)
        {
            var query = DbSet;

            var pagedResult = query
                .OrderByDynamic(paginationRequest.Order)
                .Skip(paginationRequest.Start)
                .Take(paginationRequest.PageSize)
                .ToList();

            return new PaginationResult<Importacao>(pagedResult, query.Count());

        }
    }
}