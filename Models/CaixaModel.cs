using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace EllosPratas.Models
{
    public class CaixaModel
    {
        [Key]
        public int id_caixa { get; set; }
        public int id_loja { get; set; }
        public int id_funcionario{ get; set; }
        public DateTime data_abertura { get; set; }
        public DateTime data_fechamento { get; set; }
        public string observacao { get; set; } = string.Empty;  
    }
}
