using _2.API.Models;
using Microsoft.EntityFrameworkCore;

namespace _2.API.Infra
{
    internal interface IApplicationManagementDbContext : IDbContext
    {
        DbSet<Importacao> Importacoes { get; }

        DbSet<Produto> Produtos { get; }
    }
}