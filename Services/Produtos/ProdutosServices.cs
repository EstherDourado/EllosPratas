using EllosPratas.Data;
using EllosPratas.Dto;
using EllosPratas.Models;
using Microsoft.EntityFrameworkCore;

namespace EllosPratas.Services.Produtos
{
    public class ProdutosServices : IProdutosInterface
    {
        private readonly BancoContext _context;
        private readonly string _sistema;
        public ProdutosServices(BancoContext context, IWebHostEnvironment sistema)
        {
            _context = context;
            _sistema = sistema.WebRootPath;
        }


        public string GeraCaminhoArquivo(IFormFile foto)
        {
            var codigoUnico = Guid.NewGuid().ToString(); //Gera uma cadeira de caracteres com numeros e letras
            var nomeCaminhoImagem = foto.FileName.Replace(" ", "").ToLower() + codigoUnico + ".png";

            var caminhoSalvaImagem = _sistema + "\\imagens\\";

            if (!Directory.Exists(caminhoSalvaImagem))
            {
                Directory.CreateDirectory(caminhoSalvaImagem);
            }

            using (var stream = File.Create(caminhoSalvaImagem + nomeCaminhoImagem))
            {
                foto.CopyToAsync(stream).Wait();
            }
            return nomeCaminhoImagem;
        }

        public async Task<ProdutosModel> CadastrarProduto(ProdutosCriacaoDto produtosCriacaoDto, IFormFile foto)
        {
            try
            {
                var nomeCaminhoImagem = GeraCaminhoArquivo(foto);

                var produto = new ProdutosModel
                {
                    nome_produto = produtosCriacaoDto.nome_produto,
                    descricao = produtosCriacaoDto.descricao,
                    preco_venda = produtosCriacaoDto.preco_venda,
                    categoria = produtosCriacaoDto.categoria,
                    codigo_barras = produtosCriacaoDto.codigo_barras,
                    ativo = produtosCriacaoDto.ativo,
                    imagem = nomeCaminhoImagem
                };

                _context.Add(produto);
                await _context.SaveChangesAsync();

                return produto;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);

            }
        }

        public Task<ProdutosModel> AtualizarProduto(int id, ProdutosCriacaoDto produtosAtualizaDto, IFormFile foto)
        {
            throw new NotImplementedException();
        }

        public async Task<List<ProdutosModel>> GetProdutos()
        {
            try
            {
                return await _context.Produtos.ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Task<ProdutosModel> GetProdutoId(int id)
        {
            try
            { 
                return await _context.Produtos.FirstOrDefaultAsync(produto => produto.id_produto == id); ;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }



    }
}
