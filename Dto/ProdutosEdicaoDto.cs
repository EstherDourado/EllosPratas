namespace EllosPratas.Dto
{
    public class ProdutosEdicaoDto
    {
        // Chave para identificar o produto a ser editado
        public int id_produto { get; set; }
        public string codigo_barras { get; set; }
        public required string nome_produto { get; set; }
        public required string nome_categoria { get; set; }
        public string descricao { get; set; }
        public decimal preco_venda { get; set; }
        public bool ativo { get; set; }

        // A quantidade de estoque que será atualizada
        public int quantidade { get; set; }
    }
}