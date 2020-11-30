using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace _2.API.Infra
{
    internal partial class ApplicationDbContext : DbContext, IApplicationDbContext
    {
        private readonly IConfiguration _configuration;

        public ApplicationDbContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.EnableSensitiveDataLogging(); //todo remover linha em produção
            optionsBuilder.UseSqlServer(_configuration.GetConnectionString("DefaultConnection"));
        }

        public DbContext Instance => this;
    }
}
