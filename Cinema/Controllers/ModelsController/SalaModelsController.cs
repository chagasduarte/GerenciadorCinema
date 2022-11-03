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
    public class SalaModelsController : Controller
    {
        private readonly BancoContext _context;

        public SalaModelsController(BancoContext context)
        {
            _context = context;
        }

        // GET: SalaModels
        public async Task<IActionResult> Index()
        {
            return View(await _context.Sala.ToListAsync());
        }

        // GET: SalaModels/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Sala == null)
            {
                return NotFound();
            }

            var salaModel = await _context.Sala
                .FirstOrDefaultAsync(m => m.Id_Sala == id);
            if (salaModel == null)
            {
                return NotFound();
            }

            return View(salaModel);
        }

        // GET: SalaModels/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: SalaModels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id_Sala,Name,Qtd_Assentos")] SalaModel salaModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(salaModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(salaModel);
        }

        // GET: SalaModels/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Sala == null)
            {
                return NotFound();
            }

            var salaModel = await _context.Sala.FindAsync(id);
            if (salaModel == null)
            {
                return NotFound();
            }
            return View(salaModel);
        }

        // POST: SalaModels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id_Sala,Name,Qtd_Assentos")] SalaModel salaModel)
        {
            if (id != salaModel.Id_Sala)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(salaModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SalaModelExists(salaModel.Id_Sala))
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
            return View(salaModel);
        }

        // GET: SalaModels/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Sala == null)
            {
                return NotFound();
            }

            var salaModel = await _context.Sala
                .FirstOrDefaultAsync(m => m.Id_Sala == id);
            if (salaModel == null)
            {
                return NotFound();
            }

            return View(salaModel);
        }

        // POST: SalaModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Sala == null)
            {
                return Problem("Entity set 'BancoContext.Sala'  is null.");
            }
            var salaModel = await _context.Sala.FindAsync(id);
            if (salaModel != null)
            {
                _context.Sala.Remove(salaModel);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SalaModelExists(int id)
        {
            return _context.Sala.Any(e => e.Id_Sala == id);
        }
    }
}
