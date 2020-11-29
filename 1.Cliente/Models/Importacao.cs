using System;

namespace _1.Cliente.Models
{
    public class Importacao
    {
        public Guid Id { get; set; }

        public DateTime DataImportacao { get; set; }

        public int QuantidadeProdutosImportados { get; set; }
    }
}
