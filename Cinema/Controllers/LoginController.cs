using Cinema.Dados;
using Cinema.Helper;
using Cinema.Models;
using Microsoft.AspNetCore.Mvc;

namespace Cinema.Controllers
{
    public class LoginController : Controller
    {
        private readonly BancoContext _context;
        private readonly ISessao _sessao;
        public LoginController(BancoContext context, ISessao sessao)
        {
            _context = context;
            _sessao = sessao; 
        }

        public IActionResult Index()
        {
            if (_sessao.BuscarSessaoUsuario() != null)
            {
                return RedirectToAction("Index", "SalaModels");
            }
            return View();
        }
        [HttpPost]
        public IActionResult Entrar(UsuarioModel loginModel)
        {
            try
            {
                
                if (ModelState.IsValid)
                {
                    var usuario = (from u in _context.Usuario where loginModel.Login == u.Login select u).ToList();
                    if (usuario.Count > 0)
                    {
                        if (loginModel.Senha.Trim() == usuario[0].Senha.Trim())
                        {
                            _sessao.CriarSessaodoUruario(usuario[0]);
                            return RedirectToAction("Index", "SalaModels");
                        }
                        TempData["MensagemErro"] = $"Senha Inválida";
                    }
                    else
                    {
                        TempData["MensagemErro"] = $"Usuario Inesistente";
                    }
                }
                                
                return View("Index");
            }
            catch (Exception e)
            {
                TempData["MensagemErro"] = $"Login não realizado devido ao seguinte problema: {e.Message}";
                return RedirectToAction("Index");
            }
        }

        public IActionResult Sair()
        {
            _sessao.RemoverSessaoUsuario();
            return RedirectToAction("Index", "Login");
        }
    }
}
