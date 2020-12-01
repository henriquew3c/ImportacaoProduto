using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.Json.Serialization;

namespace _1.Cliente.Models
{
    public class Importacao
    {
        [JsonPropertyName("id")]
        public Guid Id { get; set; }

        [JsonPropertyName("dataImportacao")]
        public DateTime DataImportacao { get; set; }

        [JsonPropertyName("quantidadeProdutosImportados")]
        public int QuantidadeProdutosImportados { get; set; }

        [JsonPropertyName("produtos")]
        public IEnumerable<Produto> Produtos { get; set; }

        public string GetMenorDataEntrega()
        {
            if (Produtos.Count() == 0) return "";

            return Produtos
                .OrderBy(x => x.DataEntrega)
                .First()
                .DataEntrega
                .ToString(CultureInfo.InvariantCulture).Replace("00:00:00", "");
        }

        public double GetValorTotalImportacao()
        {
            if (Produtos.Count() == 0) return 0;

            return Produtos.Sum(x => x.ValorUnitario);
        }
    }
}
