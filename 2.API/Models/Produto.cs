using System;
using System.ComponentModel.DataAnnotations;

namespace _2.API.Models
{
    public class Produto : Entity
    {
        public Produto(
            Guid id, 
            DateTime dataEntrega, 
            string nome,
            int quantidade,
            double valorUnitario, 
            Importacao importacao)
        {
            Id = id;
            DataEntrega = dataEntrega;
            Nome = nome;
            Quantidade = quantidade;
            ValorUnitario = valorUnitario;
            Importacao = importacao;
        }

        public Produto()
        {
            
        }

        [Required]
        public DateTime DataEntrega { get; set; }

        [Required]
        [MaxLength(255)]
        public string Nome { get; set; }

        [Required]
        public int Quantidade { get; set; }

        [Required]
        public double ValorUnitario { get; set; }

        public Importacao Importacao { get; set; }
    }
}
