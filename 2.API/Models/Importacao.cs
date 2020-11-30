using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace _2.API.Models
{
    public class Importacao : Entity
    {
        public Importacao(Guid id, DateTime dataImportacao)
        {
            Id = id;
            DataImportacao = dataImportacao;
        }

        public Importacao()
        {
            
        }

        [Required]
        public DateTime DataImportacao { get; set; }

        [Required]
        public int QuantidadeProdutosImportados { get; set; }

        public ICollection<Produto> Produtos { get; set; }
    }
}
