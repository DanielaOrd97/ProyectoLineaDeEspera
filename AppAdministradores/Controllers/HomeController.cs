using AppAdministradores.Models.ViewModels;
using AppAdministradores.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.ObjectModel;

namespace AppAdministradores.Controllers
{
    public class HomeController : Controller
    {
        public List<UsuarioViewModel> ListaUsuarios { get; set; } = new();
        Uri baseAddress = new Uri("https://localhost:44385/api");
        HttpClient client;
        public HomeController()
        {
            client = new HttpClient();
            client.BaseAddress = baseAddress;
        }

        public IActionResult Index()
        {
            HttpResponseMessage response = client.GetAsync(client.BaseAddress + "/Usuarios").Result;

            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                ListaUsuarios = JsonConvert.DeserializeObject<List<UsuarioViewModel>>(data);
            }
            
            return View();
        }
    }
}
