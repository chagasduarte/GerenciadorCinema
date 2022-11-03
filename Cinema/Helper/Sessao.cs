using Cinema.Models;
using Newtonsoft.Json;
using System.Drawing;

namespace Cinema.Helper
{
    public class Sessao : ISessao
    {
        private readonly IHttpContextAccessor _contextAccessor;

        public Sessao(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }
        public UsuarioModel BuscarSessaoUsuario()
        {
            string sessao = _contextAccessor.HttpContext.Session.GetString("sessaoUsuario");
            if (string.IsNullOrEmpty(sessao))
            {
                return null;
            }
            return JsonConvert.DeserializeObject<UsuarioModel>(sessao);
        }

        public void CriarSessaodoUruario(UsuarioModel usuario)
        {
            string valor = JsonConvert.SerializeObject(usuario);
            _contextAccessor.HttpContext.Session.SetString("sessaoUsuario", valor);
        }

        public void RemoverSessaoUsuario()
        {
            _contextAccessor.HttpContext.Session.Remove("sessaoUsuario");
        }
    }
}
