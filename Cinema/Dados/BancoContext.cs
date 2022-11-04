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

    }
}
