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

        public string GeraCaminhoArquivo(IFormFile imagem)
        {
            var codigoUnico = Guid.NewGuid().ToString(); //Gera uma cadeira de caracteres com numeros e letras
            var nomeCaminhoImagem = imagem.FileName.Replace(" ", "").ToLower() + codigoUnico + ".png";

            var caminhoSalvaImagem = _sistema + "\\imagens\\";

            if (!Directory.Exists(caminhoSalvaImagem))
            {
                Directory.CreateDirectory(caminhoSalvaImagem);
            }

            using (var stream = File.Create(caminhoSalvaImagem + nomeCaminhoImagem))
            {
                imagem.CopyToAsync(stream).Wait();
            }
            return nomeCaminhoImagem;
        }

        public async Task<ProdutosModel> CadastrarProduto(ProdutosCriacaoDto produtosCriacaoDto, IFormFile imagem)
        {
            using (var transaction = await _context.Database.BeginTransactionAsync()){
                try
                {
                    // Verifica se já existe um produto com o mesmo código de barras
                    //var existe = await _context.Produtos
                    //    .AnyAsync(p => p.codigo_barras == produtosCriacaoDto.codigo_barras);

                    //if (existe)
                    //    throw new Exception("Já existe um produto com este código de barras.");
                    byte[] imagemBytes = null;
                    var nomeCaminhoImagem = GeraCaminhoArquivo(imagem);

                    // Lê o arquivo de imagem como um array de bytes
                    if (imagem != null && imagem.Length > 0)
                    {
                        using (var memoryStream = new MemoryStream())
                        {
                            await imagem.CopyToAsync(memoryStream);
                            imagemBytes = memoryStream.ToArray();
                        }
                    }

                    var produto = new ProdutosModel
                    {
                        nome_produto = produtosCriacaoDto.nome_produto,
                        descricao = produtosCriacaoDto.descricao,
                        preco_venda = produtosCriacaoDto.preco_venda,
                        nome_categoria = produtosCriacaoDto.nome_categoria,
                        codigo_barras = produtosCriacaoDto.codigo_barras,
                        ativo = produtosCriacaoDto.ativo,
                        imagem = imagemBytes // Corrigido: agora é byte[]
                    };
                    //Salvando no banco
                    _context.Produtos.Add(produto);

                    //Espera que o processo seja realizado. Await só é possível utilizar se o metodo for async Task
                    await _context.SaveChangesAsync(); // salva produto primeiro para ter o ID
                
                    // Agora cria o estoque para esse produto
                    var estoque = new EstoqueModel
                    {
                        // Usa o ID do produto que acabamos de criar
                        id_produto = produto.id_produto,
                        quantidade = produtosCriacaoDto.quantidade
                    };

                    _context.Estoque.Add(estoque);
                    await _context.SaveChangesAsync();

                    await transaction.CommitAsync();
                    return produto;
                }
                catch (Exception ex)
                {
                    if (ex.InnerException != null)
                        throw new Exception(ex.InnerException.Message, ex.InnerException);
                        throw new Exception("Ocorreu um erro ao salvar o produto e o estoque.", ex);
                    throw;
                }
            }
        }

        public async Task<ProdutosModel> AtualizarProduto(ProdutosEdicaoDto dto, IFormFile imagem)
        {
            var produtoDb = await _context.Produtos.FindAsync(dto.id_produto);
            if (produtoDb == null) throw new Exception("Produto não encontrado!");

            produtoDb.nome_produto = dto.nome_produto;
            produtoDb.descricao = dto.descricao;
            produtoDb.preco_venda = dto.preco_venda;
            produtoDb.nome_categoria = dto.nome_categoria;
            produtoDb.codigo_barras = dto.codigo_barras;
            produtoDb.ativo = dto.ativo;
            produtoDb.quantidade = dto.quantidade; // Mapeamento direto

            if (imagem != null && imagem.Length > 0)
            {
                using var ms = new MemoryStream();
                await imagem.CopyToAsync(ms);
                produtoDb.imagem = ms.ToArray();
            }

            _context.Produtos.Update(produtoDb);
            await _context.SaveChangesAsync();
            return produtoDb;
        }

        public async Task<bool> AlterarStatusProduto(int id)
        {
            var produtoDb = await _context.Produtos.FindAsync(id);
            if (produtoDb == null) throw new Exception("Produto não encontrado!");

            produtoDb.ativo = !produtoDb.ativo;
            _context.Produtos.Update(produtoDb);
            await _context.SaveChangesAsync();
            return produtoDb.ativo;
        }
        async Task<List<ProdutosModel>> IProdutosInterface.GetProdutos()
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

        public async Task<ProdutosModel> GetProdutoPorId(int id)
        {
            // O método FindAsync é a forma mais eficiente de buscar um item pela sua chave primária.
            return await _context.Produtos.FindAsync(id);
        }
    }
}
