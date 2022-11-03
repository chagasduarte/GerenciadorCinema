namespace Cinema.Models
{
    public class SessaoFilmeSalaRelModel
    {
        public int Id { get; set; }
        public string? Titulo { get; set; }
        public string? Nome { get; set; }
        public DateTime Data { get; set; }
        public TimeSpan Hr_Inicio { get; set; }
        public TimeSpan Hr_Fim { get; set; }
        public decimal Valor_Ingresso { get; set; }
        public string? Tipo_Animacao { get; set; }
        public string? Tipo_Audio { get; set; }
    }
}
