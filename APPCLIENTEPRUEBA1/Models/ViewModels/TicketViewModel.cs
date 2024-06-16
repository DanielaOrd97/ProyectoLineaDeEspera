using APPCLIENTEPRUEBA1.Models.DTOs;
using APPCLIENTEPRUEBA1.Models.Entities;
using APPCLIENTEPRUEBA1.Repositories;
using APPCLIENTEPRUEBA1.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace APPCLIENTEPRUEBA1.Models.ViewModels
{
    public partial class TicketViewModel : ObservableObject
    {
        CajasRepository CajasRepository = new();

        public ObservableCollection<Caja> ListaCajas { get; set; } = new();

        Service service = new();
        HubConnection hub;


        [ObservableProperty]
        private Caja? caja;

        [ObservableProperty]
        private TurnoDTO? turno;

        [ObservableProperty]
        private string? mensaje;

        [ObservableProperty]
        private bool activo;

        

        [RelayCommand]
        public async Task Generar()
        {
            if (Caja != null)
            {
                await hub.InvokeAsync("AddTurno", Caja.Id);    
                Activo = false;
            }
        }

        [RelayCommand]
        public async Task Abandonar()
        {
            if(Turno != null)
            {
                await hub.InvokeAsync("DeleteTurno", Turno.IdTurno);
            }
        }


        public TicketViewModel()
        {
            Activo = true;
            CargarCajas();
            //EVENTO
            service.DatosActualizados += Service_DatosActualizados;
            Task.Run(() => Iniciar());
        }



        private void Service_DatosActualizados()
        {
            CargarCajas();
        }

        async void CargarCajas()
        {
            if (ListaCajas.Count == 0)
            {
                await service.GetCajas();
            }

            ListaCajas.Clear();

            foreach (var caja in CajasRepository.GetAll())
            {
                ListaCajas.Add(caja);
            }
        }

        

        private async Task Iniciar()
        {
            hub = new HubConnectionBuilder()
                .WithUrl("https://localhost:44385/turnos")
                .WithAutomaticReconnect()
                .Build();

            await hub.StartAsync();

            hub.On<TurnoDTO>("TurnoNuevo", x =>
            {
                Turno = x;
            });

            hub.On<TurnoDTO>("LlamadoCliente", x =>
            {
                Turno = x;
            });

            hub.On<string>("AbandonarTurno", x =>
            {
                Mensaje = x;
                Turno = null;
            });
        }
    }
}

