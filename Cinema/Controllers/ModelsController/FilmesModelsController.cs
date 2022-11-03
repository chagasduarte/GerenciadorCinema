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
    public class FilmesModelsController : Controller
    {
        private readonly BancoContext _context;

        public FilmesModelsController(BancoContext context)
        {
            _context = context;
        }

        // GET: FilmesModels
        public async Task<IActionResult> Index()
        {
            return View(await _context.Filme.ToListAsync());
        }

        // GET: FilmesModels/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Filme == null)
            {
                return NotFound();
            }

            var filmesModel = await _context.Filme
                .FirstOrDefaultAsync(m => m.Id_Filme == id);
            if (filmesModel == null)
            {
                return NotFound();
            }

            return View(filmesModel);
        }

        // GET: FilmesModels/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: FilmesModels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdFilme,Imagem,Titulo,Descricao,Duracao")] FilmesModel filmesModel)
        {
            try
            {
                var film = (from f in _context.Filme where f.Titulo == filmesModel.Titulo select f).ToList();
                if (film.Count() == 0) {
                    if (ModelState.IsValid)
                    {
                        _context.Add(filmesModel);
                        await _context.SaveChangesAsync();
                        return RedirectToAction(nameof(Index));
                    }
                    return View(filmesModel);
                }
                else
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (Exception e)
            {
                throw new Exception( "erro conexao", e);
            }
           
        }

        // GET: FilmesModels/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Filme == null)
            {
                return NotFound();
            }

            var filmesModel = await _context.Filme.FindAsync(id);
            if (filmesModel == null)
            {
                return NotFound();
            }
            return View(filmesModel);
        }

        // POST: FilmesModels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdFilme,Imagem,Titulo,Descricao,Duracao")] FilmesModel filmesModel)
        {
            if (id != filmesModel.Id_Filme)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(filmesModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FilmesModelExists(filmesModel.Id_Filme))
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
            return View(filmesModel);
        }

        // GET: FilmesModels/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Filme == null)
            {
                return NotFound();
            }

            var filmesModel = await _context.Filme
                .FirstOrDefaultAsync(m => m.Id_Filme == id);
            if (filmesModel == null)
            {
                return NotFound();
            }

            return View(filmesModel);
        }

        // POST: FilmesModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Filme == null)
            {
                return Problem("Entity set 'BancoContext.Filme'  is null.");
            }
            var filmesModel = await _context.Filme.FindAsync(id);
            if (filmesModel != null)
            {
                _context.Filme.Remove(filmesModel);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FilmesModelExists(int id)
        {
            return _context.Filme.Any(e => e.Id_Filme == id);
        }
    }
}
