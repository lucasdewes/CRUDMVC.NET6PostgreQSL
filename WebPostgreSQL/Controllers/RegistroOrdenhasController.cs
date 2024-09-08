using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebPostgreSQL.Models;

namespace WebPostgreSQL.Controllers
{
    public class RegistroOrdenhasController : Controller
    {
        private readonly DbContextAplicacao _context;

        public RegistroOrdenhasController(DbContextAplicacao context)
        {
            _context = context;
        }

        // GET: RegistroOrdenhas
        public async Task<IActionResult> Index()
        {
            var registroOrdenhas = await _context.RegistroOrdenhas
                .Include(ro => ro.usuario)  // Inclui o usuário
                .Include(ro => ro.OrdenhaAnimais) // Inclui a relação entre Ordenha e Animais
                    .ThenInclude(oa => oa.Animal)  // Inclui o Animal na relação
                .ToListAsync();

            return View(registroOrdenhas);
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
            model.ListaAnimais = _context.Animais.ToList(); // Carregar os animais
            model.ListaUsuarios = _context.Usuarios.ToList();

            return View(model);
        }

        // POST: RegistroOrdenhas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create([Bind("Id,Descricao,DataOrdenha,VolumeLeite,UsuarioId")] RegistroOrdenhaModel registroOrdenha)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        // Adicionar a ordenha ao contexto
        //        _context.Add(registroOrdenha);

        //        // Relacionar os animais selecionados com a ordenha
        //        if (registroOrdenha.AnimaisSelecionadosIds != null && registroOrdenha.AnimaisSelecionadosIds.Any())
        //        {
        //            foreach (var animalId in registroOrdenha.AnimaisSelecionadosIds)
        //            {
        //                _context.Add(new OrdenhaAnimal
        //                {
        //                    OrdenhaId = registroOrdenha.Id,
        //                    AnimalId = animalId
        //                });
        //            }
        //        }

        //        await _context.SaveChangesAsync();
        //        return RedirectToAction(nameof(Index));
        //    }

        //    // Se ocorrer um erro, recarregue as listas para a View
        //    registroOrdenha.ListaAnimais = _context.Animais.ToList();
        //    return View(registroOrdenha);
        //}

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(RegistroOrdenhaModel registroOrdenhaModel)
        {
            if (ModelState.IsValid)
            {
                // Adicionar a ordenha ao contexto
                var registroOrdenha = new RegistroOrdenha
                {
                    Descricao = registroOrdenhaModel.Descricao,
                    DataOrdenha = registroOrdenhaModel.DataOrdenha,
                    VolumeLeite = registroOrdenhaModel.VolumeLeite,
                    UsuarioId = registroOrdenhaModel.UsuarioId,
                    // Inicializa a lista de animais relacionados
                    OrdenhaAnimais = registroOrdenhaModel.AnimaisSelecionadosIds?.Select(animalId => new OrdenhaAnimal
                    {
                        AnimalId = animalId
                    }).ToList()
                };

                _context.Add(registroOrdenha);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            // Exibir erros de validação
            foreach (var modelState in ModelState)
            {
                foreach (var error in modelState.Value.Errors)
                {
                    Console.WriteLine($"Key: {modelState.Key}, Error: {error.ErrorMessage}");
                }
            }

            // Se ocorrer um erro, recarregue os dados necessários para a view
            registroOrdenhaModel.ListaUsuarios = _context.Usuarios.ToList();
            registroOrdenhaModel.ListaAnimais = _context.Animais.ToList();
            return View(registroOrdenhaModel);
        }

        // GET: RegistroOrdenhas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.RegistroOrdenhas == null)
            {
                return NotFound();
            }

            var registroOrdenha = await _context.RegistroOrdenhas
                                        .Include(r => r.OrdenhaAnimais) // Inclua a relação de OrdenhaAnimais
                                        .ThenInclude(oa => oa.Animal)   // Inclua também os dados dos animais
                                        .FirstOrDefaultAsync(m => m.Id == id);

            if (registroOrdenha == null)
            {
                return NotFound();
            }

            var teste = registroOrdenha.OrdenhaAnimais.Select(oa => oa.AnimalId).ToList();

            var model = new RegistroOrdenhaModel
            {
                Id = registroOrdenha.Id,
                Descricao = registroOrdenha.Descricao,
                DataOrdenha = registroOrdenha.DataOrdenha,
                VolumeLeite = registroOrdenha.VolumeLeite,
                UsuarioId = registroOrdenha.UsuarioId,
                AnimaisSelecionadosIds = registroOrdenha.OrdenhaAnimais.Select(oa => oa.AnimalId).ToList(),
                ListaAnimais = await _context.Animais.ToListAsync(), // Preencher a lista de animais para seleção
                ListaUsuarios = await _context.Usuarios.ToListAsync() // Preencher a lista de usuários
            };

            return View(model);
        }

        // POST: RegistroOrdenhas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Descricao,DataOrdenha,VolumeLeite,UsuarioId,AnimaisSelecionadosIds")] RegistroOrdenhaModel registroOrdenha)
        {
            if (id != registroOrdenha.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // Atualizar a ordenha
                    var ordenhaExistente = await _context.RegistroOrdenhas
                        .Include(ro => ro.OrdenhaAnimais)
                        .FirstOrDefaultAsync(ro => ro.Id == id);

                    if (ordenhaExistente == null)
                    {
                        return NotFound();
                    }

                    // Atualizar os detalhes da ordenha
                    ordenhaExistente.Descricao = registroOrdenha.Descricao;
                    ordenhaExistente.DataOrdenha = registroOrdenha.DataOrdenha;
                    ordenhaExistente.VolumeLeite = registroOrdenha.VolumeLeite;
                    ordenhaExistente.UsuarioId = registroOrdenha.UsuarioId;

                    // Atualizar os animais relacionados
                    _context.OrdenhaAnimais.RemoveRange(ordenhaExistente.OrdenhaAnimais); // Remover associações antigas
                    if (registroOrdenha.AnimaisSelecionadosIds != null)
                    {
                        foreach (var animalId in registroOrdenha.AnimaisSelecionadosIds)
                        {
                            _context.OrdenhaAnimais.Add(new OrdenhaAnimal { OrdenhaId = ordenhaExistente.Id, AnimalId = animalId });
                        }
                    }

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
            }

            // Recarregar os dados para a View se houver erro
            registroOrdenha.ListaAnimais = await _context.Animais.ToListAsync();
            registroOrdenha.ListaUsuarios = await _context.Usuarios.ToListAsync();
            return View(registroOrdenha);
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