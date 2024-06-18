using APPCLIENTEPRUEBA1.Models.DTOs;
using APPCLIENTEPRUEBA1.Models.Entities;
using APPCLIENTEPRUEBA1.Repositories;
using APPCLIENTEPRUEBA1.Services;
//using AVFoundation;
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
        //private static readonly string ClientId = Guid.NewGuid().ToString();
        CajasRepository CajasRepository = new();
        TurnosRepository TurnosRepository = new();
        HubConnection hub;

        public ObservableCollection<Caja> ListaCajas { get; set; } = new();
        public ObservableCollection<Turno> ListaTurnos { get; set; } = new();   

        Service service = new();


        [ObservableProperty]
        private Caja? caja;

        [ObservableProperty]
        private TurnoDTO? turno;

        [ObservableProperty]
        private TurnoDTO? turnocopy;

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
            if(Turnocopy != null)
            {
                await hub.InvokeAsync("DeleteTurno", Turnocopy.IdTurno);
            }
        }


        public TicketViewModel()
        {
            Turnocopy = new();
            Activo = true;
            CargarCajas();
            CargarTurnos();
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
            await service.GetCajas();

            MainThread.BeginInvokeOnMainThread(() =>
            {
                ListaCajas.Clear();

                foreach (var caja in CajasRepository.GetAll())
                {
                    ListaCajas.Add(caja);
                }
            });
        }

        async void CargarTurnos()
        {
            await service.GetTurnos();

            MainThread.BeginInvokeOnMainThread(() =>
            {
                ListaTurnos.Clear();

                foreach (var turno in TurnosRepository.GetAll())
                {
                    if (turno.EstadoTurno != "Terminado")
                    {
                        ListaTurnos.Add(turno);
                    }
                }
            });

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
                //Turno = x;
                Turnocopy = x;
                CargarTurnos();
            });

            hub.On<TurnoDTO>("LlamadoCliente", x =>
            {
                //Turno = x;
                Turnocopy = x;
                CargarTurnos();
            });

            hub.On<TurnoDTO>("AbandonarTurno", x =>
            {
                //Turno = x;
                Turnocopy = null;
                Mensaje = "Usted ha abandonado la fila.";
            });
            hub.On<TurnoDTO>("AtenderCliente", x =>
            {
                //Turno = x;
                Turnocopy = x;
                CargarTurnos();
            });
            hub.On<TurnoDTO>("Terminar", x =>
            {
                //Turno = x;
                Turnocopy = x;
                CargarTurnos();
            });
        } 
    }
}

