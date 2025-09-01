using System.ComponentModel.DataAnnotations;

namespace EllosPratas.Models
{
    public class EstoqueModel
    {
        [Key]   
        public int id_estoque { get; set; }
        public int id_produto { get; set; }
        public int quantidade { get; set; }
    }
}
