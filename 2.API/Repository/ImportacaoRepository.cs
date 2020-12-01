using System;
using System.Collections.Generic;
using System.Linq;
using _2.API.Infra;
using _2.API.Models;
using _2.API.Repository.Default;
using Microsoft.EntityFrameworkCore;

namespace _2.API.Repository
{
    internal class ImportacaoRepository : DefaultRepository<Importacao>, IImportacaoRepository
    {
        private readonly IApplicationDbContext _dbContext;

        public ImportacaoRepository(IApplicationDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public override Importacao Find(Guid key)
        {
            return
                DbSet
                .Include(x => x.Produtos)
                .AsNoTrackingWithIdentityResolution()
                .FirstOrDefault(x=>x.Id == key);
        }

        public IEnumerable<Importacao> GetImportacoes()
        {
            return
                DbSet
                .Include(x => x.Produtos)
                .AsNoTrackingWithIdentityResolution()
                .ToList();
        }
    }
}