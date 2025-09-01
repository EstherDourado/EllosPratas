using System.ComponentModel.DataAnnotations;

namespace EllosPratas.Models
{
    public class ItensVendaModel
    {
        [Key]
        public int id_item_venda { get; set; }
        public int id_venda { get; set; }
        public int id_produto { get; set; }
        public int quantidade { get; set; }
        public decimal preco_venda { get; set; }
        public decimal subtotal { get; set; }
    }
}
