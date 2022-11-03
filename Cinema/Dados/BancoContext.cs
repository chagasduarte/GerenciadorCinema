using Cinema.Dados.Mapp;
using Cinema.Models;
using Microsoft.EntityFrameworkCore;

namespace Cinema.Dados
{
    public class BancoContext : DbContext
    {
        public BancoContext(DbContextOptions<BancoContext> options) :base(options)
        { 

        }

        public DbSet<FilmesModel> Filme { get; set; }
        public DbSet<SalaModel> Sala { get; set; }
        public DbSet<SessaoModel> Sessao { get; set; }
        public DbSet<UsuarioModel> Usuario { get; set; }

        public bool ContemFilme(string titulo)
        {
            var filmes = (from f in Filme where f.Titulo == titulo select f).ToList();
            if (filmes.Count > 0)
            {
                return true;
            }
            return false;
        }
    }
}
