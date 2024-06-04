using AppAdministradores.Models.ViewModels;
using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace AppAdministradores.Services
{
    public class Service
    {
        //Uri baseAddress = new Uri("https://localhost:44385/api");
        HttpClient client;

        public Service()
        {
            client = new()
            {
                BaseAddress = new Uri("https://localhost:44385/api")
            };
        }

        public async Task<List<UsuarioViewModel>> VerTodosUsuarios()
        {
            List<UsuarioViewModel> UsuariosList = new();

            var response = await client.GetAsync($"Usuarios");
            var jsonresponse = await response.Content.ReadAsStringAsync();
            UsuariosList = JsonConvert.DeserializeObject<List<UsuarioViewModel>>(jsonresponse);

            return UsuariosList;
        }

    }
}
