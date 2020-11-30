using _2.API.Models;
using Microsoft.EntityFrameworkCore;

namespace _2.API.Infra
{
    internal partial class ApplicationDbContext
    {
        public DbSet<Importacao> Importacoes { get; set; }

        public DbSet<Produto> Produtos { get; set; }
    }
}
