using System.ComponentModel.DataAnnotations;

namespace EllosPratas.Models
{
    public class LojaModel
    {
        [Key]
        public int id_loja { get; set; }
        public required string nome_loja { get; set; }
        public required string id_endereco { get; set; } 
        public required string telefone { get; set; }
    }
}
