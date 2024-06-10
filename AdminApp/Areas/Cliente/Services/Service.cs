using AdminApp.Areas.Administrador.Models.ViewModels;
using Newtonsoft.Json;

namespace AdminApp.Areas.Cliente.Services
{
    public class Service
    {
        HttpClient client;

        public Service()
        {
            client = new()
            {
                BaseAddress = new Uri("https://localhost:44385/api/")
            };
        }

        ///<summary>
        ///VER TODOS LOS TURNOS
        /// </summary>
        /// 
        public async Task<List<TurnoViewModel1>> GetAllTurnos()
        {
            List<TurnoViewModel1> listaTurnos = new();

            var response = await client.GetAsync($"Turnos");

            if (response.IsSuccessStatusCode)
            {
                var jsonresponse = await response.Content.ReadAsStringAsync();
                listaTurnos = JsonConvert.DeserializeObject<List<TurnoViewModel1>>(jsonresponse);
                return listaTurnos;
            }
            return null;
        }

        ///<summary>
        ///AVANZAR TURNO
        /// </summary>
        /// 
     
        //public async Task<TurnoViewModel1> Avanzar(int id) //MODIFICAR PARA ID CLAIMS
        //{
        //    var response = await client.GetAsync($"Avanzar/{id}");

        //    if (response.IsSuccessStatusCode)
        //    {
        //        var jsonresponse = await response.Content.ReadAsStringAsync();
        //        var turno = JsonConvert.DeserializeObject<TurnoViewModel1>(jsonresponse);
        //        return turno;

        //    }
        //    return null;
        //}
    }
}
