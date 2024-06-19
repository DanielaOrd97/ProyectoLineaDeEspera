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
		Repositories.TurnosRepository repositoryturnos = new();

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

			var response = await cliente.GetFromJsonAsync<List<CajaDTO>>($"Cajas/Cliente");
			var existentes = repository.GetAll().ToList();

			if (response != null)
			{
				if (response.Count != existentes.Count)
				{
					foreach (var item in existentes)
					{
						repository.Delete(item);
					}
				}

				foreach (var caja in response)
				{
					var entidad = repository.Get(caja.Id);

					if (entidad == null && caja.Estado == 1)
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
						if (entidad != null)
						{
							if (caja.NombreCaja != entidad.NombreCaja || caja.Estado != entidad.Estado)
							{
								repository.Update(entidad);
								aviso = true;
							}
						}
					}
				}
			}


			//public async Task GetCajas()
			//{
			//    bool aviso = false;

			//    var response = await cliente.GetFromJsonAsync<List<CajaDTO>>($"Cajas/Cliente");

			//    if(response != null)
			//    {
			//        foreach (var caja in response)
			//        {
			//            var entidad = repository.Get(caja.Id);

			//            if(entidad == null && caja.Estado == 1)
			//            {
			//                entidad = new()
			//                {
			//                    Id = caja.Id,
			//                    NombreCaja = caja.NombreCaja,
			//                    Estado = (byte)caja.Estado
			//                };
			//                repository.Insert(entidad);
			//                aviso = true;
			//            }
			//            else
			//            {
			//                if(entidad != null)
			//                {
			//                    if(caja.NombreCaja != entidad.NombreCaja || caja.Estado != entidad.Estado)
			//                    {
			//                        repository.Update(entidad);
			//                        aviso = true;
			//                    }
			//                }
			//            }
			//        }
			//    }


			//    if (aviso)
			//    {

			//        _ = MainThread.InvokeOnMainThreadAsync(() =>
			//        {
			//            DatosActualizados?.Invoke();
			//        });
			//    }

			//}


			//public async Task GetTurnos()
			//{
			//    bool aviso = false;

			//    var response = await cliente.GetFromJsonAsync<List<TurnoDTO>>($"Turnos");

			//    if(response != null)
			//    {

			//        var turnosExistentes = repositoryturnos.GetAll();

			//        if(turnosExistentes != response)
			//        {
			//            foreach (var turnoExistente in turnosExistentes)
			//            {
			//                repositoryturnos.Delete(turnoExistente);
			//            }
			//        }

			//        foreach (TurnoDTO turno in response)
			//        {
			//            var entidad = repositoryturnos.Get(turno.IdTurno ?? 0);

			//            if (entidad == null)   //QUE NO ESTE COOMO TERMINADO
			//            {
			//                entidad = new()
			//                {
			//                    IdTurno = turno.IdTurno ?? 0,
			//                    NombreCaja = turno.NombreCaja,
			//                    EstadoTurno = turno.EstadoTurno,
			//                    Posicion = turno.Posicion
			//                };

			//                repositoryturnos.Insert(entidad);
			//                aviso = true;
			//            }
			//            else
			//            {
			//                if(entidad != null)
			//                {
			//                    if(turno.EstadoTurno == "Terminado")
			//                    {
			//                        repositoryturnos.Delete(entidad);
			//                        aviso = true;
			//                    }
			//                    else
			//                    {
			//                        if(turno.EstadoTurno != entidad.EstadoTurno)
			//                        {
			//                            repositoryturnos.Update(entidad);
			//                            aviso = true;
			//                        }
			//                    }
			//                }
			//            }
			//        }

			//        if (aviso)
			//        {

			//            _ = MainThread.InvokeOnMainThreadAsync(() =>
			//            {
			//                DatosActualizados?.Invoke();
			//            });
			//        }
			//    }
			//}

		}
	}
}
