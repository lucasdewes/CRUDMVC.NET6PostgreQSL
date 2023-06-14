using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebPostgreSQL.Models;

namespace WebPostgreSQL.Controllers
{
    public class RegistroOrdenhasController : Controller
    {
        private readonly Contexto _context;

        public RegistroOrdenhasController(Contexto context)
        {
            _context = context;
        }

        // GET: RegistroOrdenhas
        public async Task<IActionResult> Index()
        {
            return _context.RegistroOrdenhas != null ?
                        View(await _context.RegistroOrdenhas.Include(a => a.usuario).ToListAsync()) :
                        Problem("Entity set 'Contexto.RegistroOrdenhas'  is null.");
        }

        // GET: RegistroOrdenhas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.RegistroOrdenhas == null)
            {
                return NotFound();
            }

            var registroOrdenha = await _context.RegistroOrdenhas
                .FirstOrDefaultAsync(m => m.Id == id);
            if (registroOrdenha == null)
            {
                return NotFound();
            }

            return View(registroOrdenha);
        }

        // GET: RegistroOrdenhas/Create
        public IActionResult Create()
        {
            RegistroOrdenhaModel model = new RegistroOrdenhaModel();
            model.ListaUsuarios = _context.Usuarios.ToList();
            return View(model);
        }

        // POST: RegistroOrdenhas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Descricao,DataOrdenha,VolumeLeite,UsuarioId")] RegistroOrdenhaModel registroOrdenha)
        {
            //foreach (var modelState in ModelState.Values)
            //{
            //    foreach (ModelError error in modelState.Errors)
            //    {
            //        Console.WriteLine(error.ToString());
            //    }
            //}
            //if (ModelState.IsValid)
            //{
            _context.Add(registroOrdenha);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
            //}
            //return View(registroOrdenha);
        }

        // GET: RegistroOrdenhas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.RegistroOrdenhas == null)
            {
                return NotFound();
            }

            var registroOrdenha = await _context.RegistroOrdenhas.FindAsync(id);
            if (registroOrdenha == null)
            {
                return NotFound();
            }
            return View(registroOrdenha);
        }

        // POST: RegistroOrdenhas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Descricao,DataOrdenha,VolumeLeite, UsuarioId")] RegistroOrdenha registroOrdenha)
        {
            if (id != registroOrdenha.Id)
            {
                return NotFound();
            }

            //if (ModelState.IsValid)
            //{
                try
                {
                    _context.Update(registroOrdenha);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RegistroOrdenhaExists(registroOrdenha.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            //}
            //return View(registroOrdenha);
        }

        // GET: RegistroOrdenhas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.RegistroOrdenhas == null)
            {
                return NotFound();
            }

            var registroOrdenha = await _context.RegistroOrdenhas
                .FirstOrDefaultAsync(m => m.Id == id);
            if (registroOrdenha == null)
            {
                return NotFound();
            }

            return View(registroOrdenha);
        }

        // POST: RegistroOrdenhas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.RegistroOrdenhas == null)
            {
                return Problem("Entity set 'Contexto.RegistroOrdenhas'  is null.");
            }
            var registroOrdenha = await _context.RegistroOrdenhas.FindAsync(id);
            if (registroOrdenha != null)
            {
                _context.RegistroOrdenhas.Remove(registroOrdenha);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RegistroOrdenhaExists(int id)
        {
            return (_context.RegistroOrdenhas?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
