using System.ComponentModel.DataAnnotations;

namespace EllosPratas.Models
{
    public class EnderecoModel
    {
        [Key]
        public int id_endereco { get; set; }
        public required string rua { get; set; }
        public required string numero { get; set; }
        public required string bairro { get; set; }
        public required string cidade { get; set; }
    }
}
