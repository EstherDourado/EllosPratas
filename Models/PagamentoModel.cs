using System.ComponentModel.DataAnnotations;

namespace EllosPratas.Models
{
    public class PagamentoModel
    {
        [Key]   
        public int id_pagamento { get; set; }
        public required string forma_pagamento { get; set; }
        public decimal valor_pago { get; set; }
        public DateTime data_pagamento { get; set; }
        public int parcelas { get; set; }
    }
}
