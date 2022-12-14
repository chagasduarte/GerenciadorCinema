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
            SessaoModel sessao = GetSessao(new SessaoModel());
            
            return View(sessao);
        }

        // POST: SessaoModels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Id_Filme,Id_Sala,Data,Hr_Inicio,Hr_Fim,Valor_Ingresso,Tipo_Animacao,Tipo_Audio")] SessaoModel sessaoModel)
        {
            var horarios = (from s in _context.Sessao.Where(x => x.Id_Sala == sessaoModel.Id_Sala)
                            select new { s.Id_Sala, s.Hr_Inicio, s.Hr_Fim, s.Data }).ToList();

           
            sessaoModel.Hr_Fim = sessaoModel.Hr_Inicio + _context.Filme.Where(x => x.Id_Filme == sessaoModel.Id_Filme).FirstOrDefault().Duracao;
            foreach (var h in horarios)
            {
                if (sessaoModel.Data == h.Data) 
                {
                    if (h.Hr_Inicio < sessaoModel.Hr_Fim && h.Hr_Fim > sessaoModel.Hr_Inicio)
                    {
                        TempData["MensagemErro"] = "Conflito de horarios nessa sessão, mude o horario ou a data para poder proseguir";
                        sessaoModel = GetSessao(new SessaoModel());
                        return View(sessaoModel);
                    } 
                }
            }
            _context.Add(sessaoModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
            
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
            if (sessaoModel.Data >= DateTime.Today.AddDays(10)) 
            {
                return View(sessaoModel);
            }
            else
            {
                TempData["MensagemErro"] = "Faltam menos de 10 dias para esta sessão, não é possível exclui-la";
                return RedirectToAction(nameof(Index));
            }
            
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

        private SessaoModel GetSessao(SessaoModel sessao)
        {
            sessao.Filmes = new List<SelectListItem>();
            sessao.Salas = new List<SelectListItem>();
            List<FilmesModel> filmes = _context.Filme.ToList();
            List<SalaModel> salas = _context.Sala.ToList();

            foreach (var filme in filmes)
            {
                SelectListItem item = new SelectListItem { Text = filme.Titulo, Value = filme.Id_Filme.ToString() };

                sessao.Filmes.Add(item);
            }

            foreach (var sala in salas)
            {
                SelectListItem item = new SelectListItem { Text = sala.Nome, Value = sala.Id_Sala.ToString() };
                sessao.Salas.Add(item);
            }
            sessao.Data = DateTime.Today;

            return sessao;
        }
    }
}
