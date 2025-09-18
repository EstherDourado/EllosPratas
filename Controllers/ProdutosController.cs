using EllosPratas.Dto;
using EllosPratas.Services.Produtos;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.AccessControl;
using System.Threading.Tasks;

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

        //public async Task<IActionResult> Editar(int id_produto)
        //{
        //    var produto = await _produtosInterface.GetProdutoId(id_produto);
        //    return View(produto);
        //}

        [HttpPost]

        public async Task<IActionResult> Cadastrar(ProdutosCriacaoDto produtosCriacaoDto, IFormFile imagem)
        {
            if (ModelState.IsValid)
            {
                var produto = await _produtosInterface.CadastrarProduto(produtosCriacaoDto, imagem);

                TempData["MensagemSucesso"] = "Produto cadastrado com sucesso!";
                return RedirectToAction("Index", "Produtos");
            }
            else
            {
                TempData["MensagemErro"] = $"Ops, não foi possível cadastrar o produto.";
                return View(produtosCriacaoDto);
            }
        }
    }
}
