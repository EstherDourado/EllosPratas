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
            try
            {
                // Verifica se já existe um produto com o mesmo código de barras
                //var existe = await _context.Produtos
                //    .AnyAsync(p => p.codigo_barras == produtosCriacaoDto.codigo_barras);

                //if (existe)
                //    throw new Exception("Já existe um produto com este código de barras.");

                var nomeCaminhoImagem = GeraCaminhoArquivo(imagem);

                // Lê o arquivo de imagem como um array de bytes
                byte[] imagemBytes;
                using (var memoryStream = new MemoryStream())
                {
                    await imagem.CopyToAsync(memoryStream);
                    imagemBytes = memoryStream.ToArray();
                }

                var produto = new ProdutosModel
                {
                    nome_produto = produtosCriacaoDto.nome_produto,
                    descricao = produtosCriacaoDto.descricao,
                    preco_venda = produtosCriacaoDto.preco_venda,
                    categoria = produtosCriacaoDto.categoria,
                    codigo_barras = produtosCriacaoDto.codigo_barras,
                    ativo = produtosCriacaoDto.ativo,
                    imagem = imagemBytes // Corrigido: agora é byte[]
                };
                //Salvando no banco
                _context.Add(produto);

                //Espera que o processo seja realizado. Await só é possível utilizar se o metodo for async Task
                await _context.SaveChangesAsync();

                return produto;
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null)
                    throw new Exception(ex.InnerException.Message, ex.InnerException);
                throw;
            }
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

        Task<ProdutosModel> IProdutosInterface.GetProdutoPorId(int id)
        {
            throw new NotImplementedException();
        }
    }
}
