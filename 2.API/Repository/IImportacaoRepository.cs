using System;
using System.Collections.Generic;
using _2.API.Models;
using _2.API.Repository.Default;
using _Support;

namespace _2.API.Repository
{
    public interface IImportacaoRepository : IDefaultRepository<Importacao>
    {
        Importacao Find(Guid key);

        IEnumerable<Importacao> GetImportacoes();

    }
}
