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
                };

                UltimaPosicion = entity.Posicion;
                Repository.Insert(entity);

                var todo = Repository.GetAllTurnosWithInclude()
                .Select(x => new TurnoDTO
                {
                    IdTurno = x.IdTurno,
                    NombreCaja = x.Caja.NombreCaja,
                    EstadoTurno = x.Estado.Estado,
                    Posicion = x.Posicion
                })
                 .LastOrDefault(x => x.IdTurno == entity.IdTurno);

                

                await Clients.All.SendAsync("TurnoNuevo", todo);
            }
        }

        /// <summary>
        /// ABANDONAR TURNO.
        /// </summary>
        /// <returns></returns>
        public async Task DeleteTurno(int idTurno)
        {
            if(idTurno != 0)
            {
                //selecciono de la cola solo mi turno.
                var turno = Repository.GetAllTurnosWithInclude()
                    .Where(x => x.IdTurno == idTurno).FirstOrDefault();

                //Turnos entity = new()
                //{
                //    IdTurno = turno.IdTurno,
                //    CajaId = turno.CajaId,
                //    EstadoId = 1,    ///En default esta en espera.
                //};
                if(turno != null)
                {
                    Repository.Delete(turno);
                }
                await Clients.All.SendAsync("AbandonarTurno");
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
                        });

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
                        });

                    await Clients.All.SendAsync("Terminar", turnoactualizado);
                }
            }
        }
    }
}
