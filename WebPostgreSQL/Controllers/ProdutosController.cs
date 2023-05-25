using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Build.Framework;
using Microsoft.EntityFrameworkCore;
using WebPostgreSQL.Models;

namespace WebPostgreSQL.Controllers
{
    public class ProdutosController : Controller
    {
        private readonly Contexto _context;

        public ProdutosController(Contexto context)
        {
            _context = context;
        }

        // GET: Produtos
        public async Task<IActionResult> Index()
        {                                               // FEITO: Ordernar por Nome!
              return _context.produtos != null ? 
                          View(await _context.produtos.OrderBy(x => x.Nome).ToListAsync()) :
                          Problem("Set da Entidade 'Contexto.produtos'  é null.");
        }

        // GET: Produtos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.produtos == null)
            {
                return NotFound();
            }

            var produto = await _context.produtos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (produto == null)
            {
                return NotFound();
            }

            return View(produto);
        }

        // GET: Produtos/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Produtos/Create
        // Para proteger de ataques overposting, ativa as propriedades especificas que deseja bind.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nome")] Produto produto)
        {
            if (ModelState.IsValid)
            {
                _context.Add(produto);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(produto);
        }

        // GET: Produtos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.produtos == null)
            {
                return NotFound();
            }

            var produto = await _context.produtos.FindAsync(id);
            if (produto == null)
            {
                return NotFound();
            }
            return View(produto);
        }

        // POST: Produtos/Edit/5
        // Para proteger de ataques overposting, ativa as propriedades especificas que deseja bind.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nome")] Produto produto)
        {
            if (id != produto.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(produto);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProdutoExists(produto.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(produto);
        }

        // GET: Produtos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.produtos == null)
            {
                return NotFound();
            }

            var produto = await _context.produtos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (produto == null)
            {
                return NotFound();
            }

            return View(produto);
        }

        // POST: Produtos/Delete/5 
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.produtos == null)
            {
                return Problem("Entidade set 'Contexto.produtos'  é null.");
            }
            var produto = await _context.produtos.FindAsync(id);
            if (produto != null)
            {
                _context.produtos.Remove(produto);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProdutoExists(int id)
        {
          return (_context.produtos?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
