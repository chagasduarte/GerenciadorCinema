using System.ComponentModel.DataAnnotations;

namespace Cinema.Models
{
	public class SessaoModel
	{
		[Key]
		public int Id { get; set; }
		public int Id_Filme { get; set; }
		public int Id_Sala { get; set; }
		public DateTime Data { get; set; }
		public TimeSpan Hr_Inicio { get; set; }
		public TimeSpan Hr_Fim { get; set; }
		public decimal Valor_Ingresso { get; set; }
		public string? Tipo_Animacao { get; set; }
		public string? Tipo_Audio { get; set; }
		
	}
}
