using System;
using Microsoft.EntityFrameworkCore;

namespace _2.API.Infra
{
    internal interface IDbContext : IDisposable
    {
        DbContext Instance { get; }
    }
}