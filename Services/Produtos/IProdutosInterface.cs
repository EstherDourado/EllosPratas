using EllosPratas.Dto;
using EllosPratas.Models;

namespace EllosPratas.Services.Produtos
{
    public interface IProdutosInterface 
    {
        Task<ProdutosModel> CadastrarProduto(ProdutosCriacaoDto produtosCriacaoDto, IFormFile foto);

        Task<ProdutosModel> AtualizarProduto(int id, ProdutosCriacaoDto produtosAtualizaDto, IFormFile foto);

        //Retorna todas os produtos
        Task<List<ProdutosModel>> GetProdutos();

        //Retorna apenas um produto pelo Id
        Task<ProdutosModel> GetProdutoId(int id);
    }

}
