using EllosPratas.Data;
using EllosPratas.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace EllosPratas.Controllers
{
    public class CategoriasController : Controller
    {
        private readonly BancoContext _context;

        public CategoriasController(BancoContext context)
        {
            _context = context;
        }

        // Endpoint para o Select2 buscar as categorias
        [HttpGet]
        public async Task<IActionResult> Listar(string q)
        {
            var query = _context.Categorias.AsQueryable();

            if (!string.IsNullOrEmpty(q))
            {
                query = query.Where(c => c.nome_categoria.Contains(q));
            }

            var categorias = await query.Select(c => new { id = c.nome_categoria, text = c.nome_categoria }).ToListAsync();
            return Json(categorias);
        }

        // Lista TODAS as categorias para a área de gestão
        [HttpGet]
        public async Task<IActionResult> ListarTodas()
        {
            var categorias = await _context.Categorias
                                           .OrderBy(c => c.nome_categoria)
                                           .ToListAsync();
            return Json(categorias);
        }

        // Endpoint para cadastrar uma nova categoria via AJAX
        [HttpPost]
        public async Task<IActionResult> Cadastrar([FromForm] CategoriaModel categoria)
        {
            if (ModelState.IsValid)
            {
                // Verifica se a categoria já existe para evitar duplicatas
                var existe = await _context.Categorias.AnyAsync(c => c.nome_categoria.ToLower() == categoria.nome_categoria.ToLower());
                if (existe)
                {
                    return BadRequest("Esta categoria já existe.");
                }

                _context.Categorias.Add(categoria);
                await _context.SaveChangesAsync();
                // Retorna o objeto criado, que será usado pelo JavaScript
                return Ok(new { id = categoria.nome_categoria, text = categoria.nome_categoria });
            }
            return BadRequest("Dados inválidos.");
        }
        // Método para salvar a edição de uma categoria
        [HttpPost]
        public async Task<IActionResult> Editar([FromForm] CategoriaModel categoriaEditada)
        {
            if (ModelState.IsValid)
            {
                var categoria = await _context.Categorias.FindAsync(categoriaEditada.id_categoria);
                if (categoria == null)
                {
                    return NotFound();
                }

                categoria.nome_categoria = categoriaEditada.nome_categoria;
                await _context.SaveChangesAsync();
                return Ok();
            }
            return BadRequest("Dados inválidos.");
        }

        // Método para ativar ou inativar uma categoria
        [HttpPost]
        public async Task<IActionResult> AlterarStatus(int id)
        {
            var categoria = await _context.Categorias.FindAsync(id);
            if (categoria == null)
            {
                return NotFound();
            }

            categoria.ativo = !categoria.ativo; // Inverte o status atual
            await _context.SaveChangesAsync();
            return Ok(new { novoStatus = categoria.ativo });
        }
    }
}
