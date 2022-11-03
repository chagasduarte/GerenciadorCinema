using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.SqlTypes;

namespace Cinema.Models
{
	public class FilmesModel
	{
		[Key]
		public int Id_Filme { set; get; }
		
		public string? Imagem { set; get; }
		public string Titulo { set; get; }
		public string? Descricao { set; get; }
		
		public TimeSpan Duracao { set; get; }


	}
}
