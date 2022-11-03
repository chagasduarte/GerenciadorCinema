using System.ComponentModel.DataAnnotations;

namespace Cinema.Models
{
	public class SalaModel
	{
		[Key]
		public int Id_Sala { set; get; }
		public string? Nome { set; get; }
		public int? Qtd_Assentos { set; get; }
	}
}
