using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

using Microsoft.AspNetCore.Mvc;
using Api.Data;
using Api.ProdutoModel;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProdutoController : ControllerBase
    {

        private readonly AppDbContext _context;

        public ProdutoController(AppDbContext context)
        {
            _context = context;
        }

        // CREATE (POST)
        [HttpPost("novo")]
        public async Task<ActionResult<Produto>> NovoProduto (Produto produto)
        {
            _context.Produtos.Add(produto);
            await _context.SaveChangesAsync();
            //return CreatedAtAction("GetProduto", new { id = produto.Id }, produto);
            return CreatedAtAction(nameof(GetProdutoById), new { id = produto.Id }, produto);
        }

        // GET ALL
        [HttpGet("todos")]
        public async Task<ActionResult<IEnumerable<Produto>>> GetProdutos()
        {
            return await _context.Produtos.ToListAsync();
        }

        // GET BY ID
        [HttpGet("{id}")]
        public async Task<ActionResult<Produto>> GetProdutoById(int id)
        {
            var produto = await _context.Produtos.FindAsync(id);

            if (produto == null) return NotFound();
            
            return produto;
        }

        // UPDATE (PUT)
        [HttpPut("{id}")]
        public async Task<IActionResult> updateProduto (int id, Produto produto)
        {
            if (id != produto.Id) return BadRequest();

            _context.Entry(produto).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProdutoExists(id)) return NotFound();
                else throw;
            }

            return Ok(produto);
        }

        // DELETE
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduto(int id)
        {
            var produto = await _context.Produtos.FindAsync(id);
            if (produto == null) return NotFound();

            _context.Produtos.Remove(produto);
            await _context.SaveChangesAsync();

            return Ok("Produto deletado com sucesso!");
        }   

        private bool ProdutoExists(int id)
        {
            return _context.Produtos.Any(e => e.Id == id);
        }
    }
    
}
