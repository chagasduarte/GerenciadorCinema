using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Cinema.Dados;
using Cinema.Models;
using Cinema.Filters;

namespace Cinema.Controllers.ModelsController
{
    [UsuarioLogado]
    public class UsuarioModelsController : Controller
    {
        private readonly BancoContext _context;

        public UsuarioModelsController(BancoContext context)
        {
            _context = context;
        }

        // GET: UsuarioModels
        public async Task<IActionResult> Index()
        {
              return View(await _context.Usuario.ToListAsync());
        }

        // GET: UsuarioModels/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Usuario == null)
            {
                return NotFound();
            }

            var usuarioModel = await _context.Usuario
                .FirstOrDefaultAsync(m => m.Id_Usuario == id);
            if (usuarioModel == null)
            {
                return NotFound();
            }

            return View(usuarioModel);
        }

        // GET: UsuarioModels/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: UsuarioModels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Email,Login,Senha")] UsuarioModel usuarioModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(usuarioModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(usuarioModel);
        }

        // GET: UsuarioModels/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Usuario == null)
            {
                return NotFound();
            }

            var usuarioModel = await _context.Usuario.FindAsync(id);
            if (usuarioModel == null)
            {
                return NotFound();
            }
            return View(usuarioModel);
        }

        // POST: UsuarioModels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Email,Login,Senha")] UsuarioModel usuarioModel)
        {
            if (id != usuarioModel.Id_Usuario)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(usuarioModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UsuarioModelExists(usuarioModel.Id_Usuario))
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
            return View(usuarioModel);
        }

        // GET: UsuarioModels/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Usuario == null)
            {
                return NotFound();
            }

            var usuarioModel = await _context.Usuario
                .FirstOrDefaultAsync(m => m.Id_Usuario == id);
            if (usuarioModel == null)
            {
                return NotFound();
            }

            return View(usuarioModel);
        }

        // POST: UsuarioModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Usuario == null)
            {
                return Problem("Entity set 'BancoContext.Usuario'  is null.");
            }
            var usuarioModel = await _context.Usuario.FindAsync(id);
            if (usuarioModel != null)
            {
                _context.Usuario.Remove(usuarioModel);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UsuarioModelExists(int id)
        {
          return _context.Usuario.Any(e => e.Id_Usuario == id);
        }
    }
}
