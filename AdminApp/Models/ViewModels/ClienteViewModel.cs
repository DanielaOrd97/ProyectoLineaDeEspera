using AdminApp.Areas.Administrador.Models.ViewModels;

namespace AdminApp.Models.ViewModels
{
    public class ClienteViewModel
    {
        public IEnumerable<TurnoViewModel1> ListaTurnos { get; set; }   = new List<TurnoViewModel1>();
    }
}
