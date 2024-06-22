using API_Linea_Espera.Models.DTOs;
using API_Linea_Espera.Models.Entities;
using API_Linea_Espera.Repositories;
using Microsoft.AspNetCore.SignalR;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace API_Linea_Espera.Hubs
{
    public class TurnosHub : Hub
    {
        public IRepository<Turnos> Repository { get; }
        public static int UltimaPosicion { get; set; }

        public TurnosHub(IRepository<Turnos> repository)
        {
            this.Repository = repository;
            UltimaPosicion = UltimaPosicion;
        }

        public async Task AddTurno(int id)
        {
            if (id != 0)
            {
                var ultimo = Repository.GetAllTurnosWithInclude()
                    .Select(x => x.Posicion)
                    .LastOrDefault();

                Turnos entity = new()
                {
                    IdTurno = 0,
                    CajaId = id,
                    EstadoId = 1,    ///En default esta en espera.
                    Posicion = ultimo += 1
                    //TiempoInicio = DateTime.UtcNow
                };

                UltimaPosicion = entity.Posicion;
                Repository.Insert(entity);

                TurnoDTO turnoGenerado = new()
                {
                    IdTurno = entity.IdTurno,
                    NombreCaja = entity.Caja.NombreCaja,
                    EstadoTurno = entity.Estado.Estado,
                    Posicion = entity.Posicion
                };

                await Groups.AddToGroupAsync(Context.ConnectionId, turnoGenerado.IdTurno.ToString());
                await Clients.Group(turnoGenerado.IdTurno.ToString()).SendAsync("TicketCreado", turnoGenerado);

				await Clients.All.SendAsync("TurnoNuevo", turnoGenerado);

				//var todo = Repository.GetAllTurnosWithInclude()
				//.Select(x => new TurnoDTO
				//{
				//    IdTurno = x.IdTurno,
				//    NombreCaja = x.Caja.NombreCaja,
				//    EstadoTurno = x.Estado.Estado,
				//    Posicion = x.Posicion
				//})
				// .LastOrDefault(x => x.IdTurno == entity.IdTurno);



				//await Clients.All.SendAsync("TurnoNuevo", todo);
			}
        }

        /// <summary>
        /// ABANDONAR TURNO.
        /// </summary>
        /// <returns></returns>
        public async Task DeleteTurno(int id)
        {
            if(id != 0)
            {
                //selecciono de la cola solo mi turno.
                var turno = Repository.GetAllTurnosWithInclude()
                    .Where(x => x.IdTurno == id)
                    .FirstOrDefault();

                

                if (turno != null)
                {
                    Repository.Delete(turno);
                }

                TurnoDTO t = new()
                {
                    IdTurno = turno.IdTurno,
                    NombreCaja = turno.Caja.NombreCaja,
                    EstadoTurno = turno.Estado.Estado,
                    Posicion = turno.Posicion
                };

                await Clients.All.SendAsync("AbandonarTurno", t);

                //await Clients.All.SendAsync("AbandonarTurno","Usted ha abandonado la fila.");
            }
        }

        ///<summary>
        ///LLAMAR AL CLIENTE ACTUAL PARA SER ATENDIDO.
        /// </summary>
        /// 
        public async Task LlamarCliente(int idTurno)
        {
            if(idTurno != 0)
            {
                var turno = Repository.Get(idTurno);

                if(turno != null)
                {
                    turno.EstadoId = 2;

                    Repository.Update(turno);

                    var turnoactualizado = Repository.GetAllTurnosWithInclude()
                        .Where(x => x.IdTurno == idTurno)
                        .Select(x => new TurnoDTO
                        {
                            IdTurno = turno.IdTurno,
                            NombreCaja = turno.Caja.NombreCaja,
                            EstadoTurno = turno.Estado.Estado
                        }).FirstOrDefault();

                    await Clients.All.SendAsync("LlamadoCliente", turnoactualizado);
                }
            }
        }

        public async Task AtenderCliente(int idTurno)
        {
            if (idTurno != 0)
            {
                var turno = Repository.Get(idTurno);

                if (turno != null)
                {
                    turno.EstadoId = 3;

                    Repository.Update(turno);

                    var turnoactualizado = Repository.GetAllTurnosWithInclude()
                        .Where(x => x.IdTurno == idTurno)
                        .Select(x => new TurnoDTO
                        {
                            IdTurno = turno.IdTurno,
                            NombreCaja = turno.Caja.NombreCaja,
                            EstadoTurno = turno.Estado.Estado
                            //TiempoFin = DateTime.UtcNow
                        }).FirstOrDefault();

                    await Clients.All.SendAsync("AtenderCliente", turnoactualizado);
                }
            }
        }

        public async Task TerminarAtencion(int idTurno)
        {
            if (idTurno != 0)
            {
                var turno = Repository.Get(idTurno);

                if (turno != null)
                {
                    turno.EstadoId = 4;

                    Repository.Update(turno);

                    var turnoactualizado = Repository.GetAllTurnosWithInclude()
                        .Where(x => x.IdTurno == idTurno)
                        .Select(x => new TurnoDTO
                        {
                            IdTurno = turno.IdTurno,
                            NombreCaja = turno.Caja.NombreCaja,
                            EstadoTurno = turno.Estado.Estado
                        }).FirstOrDefault();

                    await Clients.All.SendAsync("Terminar", turnoactualizado);
                }
            }
        }

        public string NombreCaja { get; set; }
        public async Task CancelarTurnos(int idcaja)
        {
            if (idcaja != 0)
            {
                //SOLO MARCAR COMO ELIMINADOS LOS TURNOS QUE NO HAN SIDO TERMINADOS.
                var turnos = Repository.GetAllTurnosWithInclude()
                    .Where(x => x.Caja.IdCaja == idcaja && x.Estado.IdEstado != 4).ToList();
                

                foreach (var turno in turnos)
                {
                    NombreCaja = turno.Caja.NombreCaja;
                    Repository.Delete(turno);
                }

                await Clients.All.SendAsync("Cerrar", NombreCaja);
                await Clients.All.SendAsync("CerrarServicio");

				//await Clients.All.SendAsync("CerrarServicio", "La caja elegida ha sido cerrada, favor de elegir otro servicio."); ;
			}
        }
    }
}
