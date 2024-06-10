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

                //var todo = Repository.GetAllTurnosWithInclude()
                //    .Select(x => new TurnoDTO
                //    {
                //        IdTurno = entity.IdTurno,
                //        IdCaja = entity.
                //        EstadoTurno = entity.Estado.Estado
                //    })
                //    .LastOrDefault(x => x.IdTurno == entity.IdTurno);

                var todo = Repository.GetAllTurnosWithInclude()
                .Select(x => new TurnoDTO
                {
                    IdTurno = x.IdTurno,
                    //NombreCliente = x.Usuario.Nombre,
                    //IdCaja = x.CajaId,
                    NombreCaja = x.Caja.NombreCaja,
                    EstadoTurno = x.Estado.Estado,
                    Posicion = x.Posicion
                })
                 .LastOrDefault(x => x.IdTurno == entity.IdTurno);

                

                await Clients.All.SendAsync("TurnoNuevo", todo);
            }
        }
    }
}
