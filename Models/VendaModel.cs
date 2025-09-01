using System.ComponentModel.DataAnnotations;

namespace EllosPratas.Models
{
    public class VendaModel
    {
        [Key]
        public int id_venda { get; set; }
        public int id_loja { get; set; }
        public int id_cliente { get; set; }
        public int id_funcionario { get; set; }
        public DateTime data_venda { get; set; }
        public decimal valor_total { get; set; }
        public decimal valor_desconto { get; set; }
        public decimal valor_final { get; set; }
        public required string forma_pagamento { get; set; }
    }
}
