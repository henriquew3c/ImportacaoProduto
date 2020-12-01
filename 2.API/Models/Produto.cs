using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;

namespace _2.API.Models
{
    public class Produto : Entity
    {
        public Produto(
            Guid id, 
            DateTime dataEntrega, 
            string nome,
            int quantidade,
            double valorUnitario)
        {
            Id = id;
            DataEntrega = dataEntrega;
            Nome = nome;
            Quantidade = quantidade;
            ValorUnitario = valorUnitario;
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

        [JsonIgnore]
        public Importacao Importacao { get; set; }

        public List<string> Valida(int row)
        {
            var errosAtuais = new List<string>();

            if (DataEntregaIsValid() == false)
            {
                errosAtuais.Add($"A data de entrega do produto não é uma data válida. Erro na Linha: {row}.");
            }

            if (Nome == null)
            {
                errosAtuais.Add($"O nome do produto não pode ser nulo. Erro na Linha: {row}.");
            }

            if (int.TryParse(Quantidade.ToString(), out _) == false)
            {
                errosAtuais.Add($"A quantidade do produto deve corresponder a um valor inteiro. Erro na Linha: {row}.");
            }

            if (double.TryParse(ValorUnitario.ToString(CultureInfo.InvariantCulture), out _) == false)
            {
                errosAtuais.Add(
                    $"O valor unitário do produto deve corresponder a um valor flutuante (ex: 10,1 ou 10,0). Erro na Linha: {row}.");
            }

            return errosAtuais;
        }

        private bool DataEntregaIsValid()
        {
            var regex = new Regex(@"(((0|1)[0-9]|2[0-9]|3[0-1])\/(0[1-9]|1[0-2])\/((19|20)\d\d))$");

            //Verify whether date entered in dd/MM/yyyy format.
            return regex.IsMatch(DataEntrega.ToString().Replace("00:00:00", "").Trim());
        }
    }
}
