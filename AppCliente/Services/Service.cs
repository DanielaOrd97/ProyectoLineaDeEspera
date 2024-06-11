using AppCliente.Models.DTOs;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppCliente.Services
{
    public class Service
    {
        HttpClient client;

        public Service()
        {
            client = new()
            {
                BaseAddress = new Uri("https://localhost:44385/")
            };
        }

        public async Task<List<TurnoDTO>> GetAllTurnos()
        {
            var response = await client.GetAsync($"Turnos");

            if (response.IsSuccessStatusCode)
            {
                var jsonresponse = await response.Content.ReadAsStringAsync();
                var listaTurnos = JsonConvert.DeserializeObject<List<TurnoDTO>>(jsonresponse);
                return listaTurnos;
            }
            return null;
        }
    }
}
