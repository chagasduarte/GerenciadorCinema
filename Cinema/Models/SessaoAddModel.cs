using Microsoft.AspNetCore.Mvc.Rendering;

namespace Cinema.Models
{
    public class SessaoAddModel
    {
        public SelectList filmesModel { set; get; }
        public SelectList salaModel { set; get; }
        public SessaoModel sessaoModel { set; get; }
    }
}
