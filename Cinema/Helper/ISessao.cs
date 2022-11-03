using Cinema.Models;

namespace Cinema.Helper
{
    public interface ISessao
    {
        void CriarSessaodoUruario(UsuarioModel usuario);
        void RemoverSessaoUsuario();
        UsuarioModel BuscarSessaoUsuario();
    }
}
