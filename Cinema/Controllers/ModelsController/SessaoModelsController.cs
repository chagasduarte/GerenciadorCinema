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
    public class SessaoModelsController : Controller
    {
        private readonly BancoContext _context;

        public SessaoModelsController(BancoContext context)
        {
            _context = context;
        }

        // GET: SessaoModels
        public async Task<IActionResult> Index()
        {
            
            var se = (from s in _context.Sessao 
                         join f in _context.Filme on s.Id_Filme equals f.Id_Filme
                         join sl in _context.Sala on s.Id_Sala equals sl.Id_Sala
                         select new {f.Titulo, sl.Nome, s.Id, s.Data,s.Hr_Inicio, s.Hr_Fim,s.Valor_Ingresso,s.Tipo_Animacao, s.Tipo_Audio}).ToList();

            List<SessaoFilmeSalaRelModel> sessoes = new List<SessaoFilmeSalaRelModel>();

            foreach (var item in se)
            {
                SessaoFilmeSalaRelModel sessaoFilmeSalaRelModel = new SessaoFilmeSalaRelModel
                {
                    Id = item.Id,
                    Data = item.Data,
                    Hr_Fim = item.Hr_Fim,
                    Hr_Inicio = item.Hr_Inicio,
                    Nome = item.Nome,
                    Tipo_Animacao = item.Tipo_Animacao,
                    Tipo_Audio = item.Tipo_Audio,
                    Titulo = item.Titulo,
                    Valor_Ingresso = item.Valor_Ingresso 
                };
                sessoes.Add(sessaoFilmeSalaRelModel);
            }

            
            return View(sessoes);
        }

        // GET: SessaoModels/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Sessao == null)
            {
                return NotFound();
            }

            var sessaoModel = await _context.Sessao
                .FirstOrDefaultAsync(m => m.Id == id);
            if (sessaoModel == null)
            {
                return NotFound();
            }

            return View(sessaoModel);
        }

        // GET: SessaoModels/Create
        public IActionResult Create()
        {
           
            return View();
        }

        // POST: SessaoModels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Id_Filme,Id_Sala,Data,Hr_Inicio,Hr_Fim,Valor_Ingresso,Tipo_Animacao,Tipo_Audio")] SessaoModel sessaoModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(sessaoModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(sessaoModel);
        }

        // GET: SessaoModels/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Sessao == null)
            {
                return NotFound();
            }

            var sessaoModel = await _context.Sessao.FindAsync(id);
            if (sessaoModel == null)
            {
                return NotFound();
            }
            return View(sessaoModel);
        }

        // POST: SessaoModels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Id_Filme,Id_Sala,Data,Hr_Inicio,Hr_Fim,Valor_Ingresso,Tipo_Animacao,Tipo_Audio")] SessaoModel sessaoModel)
        {
            if (id != sessaoModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(sessaoModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SessaoModelExists(sessaoModel.Id))
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
            return View(sessaoModel);
        }

        // GET: SessaoModels/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Sessao == null)
            {
                return NotFound();
            }

            var sessaoModel = await _context.Sessao
                .FirstOrDefaultAsync(m => m.Id == id);
            if (sessaoModel == null)
            {
                return NotFound();
            }

            return View(sessaoModel);
        }

        // POST: SessaoModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Sessao == null)
            {
                return Problem("Entity set 'BancoContext.Sessao'  is null.");
            }
            var sessaoModel = await _context.Sessao.FindAsync(id);
            if (sessaoModel != null)
            {
                _context.Sessao.Remove(sessaoModel);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SessaoModelExists(int id)
        {
          return _context.Sessao.Any(e => e.Id == id);
        }
    }
}
