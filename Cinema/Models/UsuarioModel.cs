using MessagePack;
using System.ComponentModel.DataAnnotations;
using KeyAttribute = System.ComponentModel.DataAnnotations.KeyAttribute;

namespace Cinema.Models
{
	public class UsuarioModel
	{
		[Key]
		public int Id_Usuario { get; set; }
		public string? Email { set; get; }
		public string Login { set; get; }
		public string Senha { set; get; }

	}
}
