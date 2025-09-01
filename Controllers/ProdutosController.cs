using EllosPratas.Dto;
using EllosPratas.Services.Produtos;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.AccessControl;

namespace EllosPratas.Controllers
{
    public class ProdutosController : Controller
    {

        private readonly IProdutosInterface _produtosInterface;
        public ProdutosController(IProdutosInterface produtosInterface)
        {
            _produtosInterface = produtosInterface;
        }

        public async Task<IActionResult> Index()
        {
            var produtos =  await _produtosInterface.GetProdutos();
            return View();
        }

        public IActionResult Cadastrar()
        {
            return View();
        }

        public async Task<IActionResult> Editar(int id_produto)
        {
            var produto = await _produtosInterface.GetProdutoId(id_produto);
            return View(produto);
        }

        [HttpPost]

        public async Task<IActionResult> Cadastrar(ProdutosCriacaoDto produtosCriacaoDto, IFormFile foto)
        {
            if (ModelState.IsValid)
            {
                var produto = await _produtosInterface.CadastrarProduto(produtosCriacaoDto, foto);
                return RedirectToAction("Index", "Produtos");
            }
            else
            {
                return View(produtosCriacaoDto);
            }
        }
    }
}
