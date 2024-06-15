using APPCLIENTEPRUEBA1.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace APPCLIENTEPRUEBA1.Services
{
    public class Service
    {
        HttpClient cliente;
        Repositories.CajasRepository repository = new();

        public Service()
        {
            cliente = new()
            {
                BaseAddress = new Uri("https://localhost:44385/api/")
            };
        }

        public event Action? DatosActualizados;

        public async Task GetCajas()
        {
            bool aviso = false;

            var response = await cliente.GetFromJsonAsync<List<CajaDTO>>($"Cajas");

            if(response != null)
            {
                foreach (var caja in response)
                {
                    var entidad = repository.Get(caja.Id);

                    if(entidad == null && caja.Estado == 1)
                    {
                        entidad = new()
                        {
                            Id = caja.Id,
                            NombreCaja = caja.NombreCaja,
                            Estado = (byte)caja.Estado
                        };
                        repository.Insert(entidad);
                        aviso = true;
                    }
                    else
                    {
                        if(entidad != null)
                        {
                            if(caja.NombreCaja != entidad.NombreCaja || caja.Estado != entidad.Estado)
                            {
                                repository.Update(entidad);
                                aviso = true;
                            }
                        }
                    }
                }
            }


            if (aviso)
            {

                _ = MainThread.InvokeOnMainThreadAsync(() =>
                {
                    DatosActualizados?.Invoke();
                });
            }

        }
    }
}
