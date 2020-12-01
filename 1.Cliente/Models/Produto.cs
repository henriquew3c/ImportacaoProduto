using System;
using System.Text.Json.Serialization;
using _2.API.Models;

namespace _1.Cliente.Models
{
    public class Produto : Entity
    {
        [JsonPropertyName("dataEntrega")]
        public DateTime DataEntrega { get; set; }

        [JsonPropertyName("nome")]
        public string Nome { get; set; }

        [JsonPropertyName("quantidade")]
        public int Quantidade { get; set; }

        [JsonPropertyName("valorUnitario")]
        public double ValorUnitario { get; set; }

        [JsonPropertyName("id")]
        public Guid Id { get; set; }

        public double GetValorTotal()
        {
            return ValorUnitario * Quantidade;
        }
    }
}
