using EllosPratas.Dto;
using EllosPratas.Services.Produtos;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.AccessControl;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace EllosPratas.Controllers
{
    public class ProdutosController : Controller
    {
        //Injeção de dependencia 
        private readonly IProdutosInterface _produtosInterface;

        //Construtor
        public ProdutosController(IProdutosInterface produtosInterface)
        {
            _produtosInterface = produtosInterface;
        }

        public async Task<IActionResult> Index()
        {
            var produtos = await _produtosInterface.GetProdutos();
            return View(produtos);
        }

        public IActionResult Cadastrar()
        {
            return View();
        }

        public async Task<IActionResult> Editar(int id)
        {
            var produto = await _produtosInterface.GetProdutoPorId(id);
            if (produto == null) return NotFound();

            // Mapeia o Model para o EdicaoDto para enviar à View
            var produtoDto = new ProdutosEdicaoDto
            {
                id_produto = produto.id_produto,
                nome_produto = produto.nome_produto,
                descricao = produto.descricao,
                nome_categoria = produto.nome_categoria,
                preco_venda = produto.preco_venda,
                codigo_barras = produto.codigo_barras,
                ativo = produto.ativo,
                quantidade = produto.quantidade // Usa o operador '??' para o caso de o estoque ser nulo
            };
            ViewBag.ImagemAtual = produto.imagem;
            // Reutiliza a view 'Cadastrar', passando o DTO de edição
            return View("Cadastrar", produtoDto);
        }

        [HttpPost]
        [HttpPost]
        public async Task<IActionResult> Cadastrar(ProdutosCriacaoDto produtosCriacaoDto, IFormFile imagem)
        {
            if (!ModelState.IsValid)
            {
                // CORREÇÃO: Converte o CriacaoDto para o EdicaoDto em caso de erro.
                // Isso garante que estamos enviando o tipo de modelo que a View espera.
                var modelParaView = new ProdutosEdicaoDto
                {
                    nome_produto = produtosCriacaoDto.nome_produto,
                    descricao = produtosCriacaoDto.descricao,
                    nome_categoria = produtosCriacaoDto.nome_categoria,
                    preco_venda = produtosCriacaoDto.preco_venda,
                    quantidade = produtosCriacaoDto.quantidade,
                    codigo_barras = produtosCriacaoDto.codigo_barras,
                    ativo = produtosCriacaoDto.ativo
                };
                TempData["MensagemErro"] = "Ops, dados inválidos. Verifique os campos.";
                return View(modelParaView);
            }

            await _produtosInterface.CadastrarProduto(produtosCriacaoDto, imagem);
            TempData["MensagemSucesso"] = "Produto cadastrado com sucesso!";
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Editar(ProdutosEdicaoDto dto, IFormFile imagem)
        {
            if (ModelState.IsValid)
            {
                await _produtosInterface.AtualizarProduto(dto, imagem);
                TempData["MensagemSucesso"] = "Produto atualizado com sucesso!";
                return RedirectToAction("Index");
            }
            return View("Cadastrar", dto);
        }

        [HttpPost]
        public async Task<IActionResult> AlterarStatus(int id)
        {
            try
            {
                bool novoStatus = await _produtosInterface.AlterarStatusProduto(id);
                return Ok(new { success = true, status = novoStatus });
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }
        }
    }
}
