namespace AdminApp.Models.ViewModels
{
    public class LogInViewModel
    {
        public string NombreUsuario { get; set; } = null!;
        public string Contraseña { get; set; } = null!;
        public string? Error { get; set; } = "";
    }
}
