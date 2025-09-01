using System.ComponentModel.DataAnnotations;

namespace EllosPratas.Models
{
    public class ProdutosModel
    {
        [Key]
        public int id_produto { get; set; }
        public string codigo_barras { get; set; } = string.Empty;
        public required string nome_produto { get; set; } 
        public required string categoria { get; set; }
        public string descricao { get; set; } = string.Empty;
        public decimal preco_venda { get; set; }
        public bool ativo { get; set; }
        public string imagem { get; set; } = string.Empty;
    }
}
