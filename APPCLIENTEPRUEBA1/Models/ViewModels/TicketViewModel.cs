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
        //public ObservableCollection<Turno> ListaTurnos { get; set; } = new();   

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

        [ObservableProperty]
        private string? aviso;

		[ObservableProperty]
		private bool indicador;

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
            Indicador = false;
            CargarCajas();
           // CargarTurnos();
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

        //async void CargarTurnos()
        //{
        //    await service.GetTurnos();

        //    MainThread.BeginInvokeOnMainThread(() =>
        //    {
        //        ListaTurnos.Clear();

        //        foreach (var turno in TurnosRepository.GetAll())
        //        {
        //            if (turno.EstadoTurno != "Terminado")
        //            {
        //                ListaTurnos.Add(turno);
        //            }
        //        }
        //    });

        //}

        private async Task Iniciar()
        {
            hub = new HubConnectionBuilder()
                .WithUrl("https://bancotec.websitos256.com/turnos")
                //.WithUrl("https://localhost:44385/turnos")
                .WithAutomaticReconnect()
                .Build();

            await hub.StartAsync();

            //        hub.On<TurnoDTO>("TurnoNuevo", x =>
            //        {
            //Turnocopy = x;
            //        });

            hub.On<TurnoDTO>("TicketCreado", x =>
            {
                Turnocopy = x;
            });


            //hub.On<TurnoDTO>("LlamadoCliente", x =>
            //{
            //    Turnocopy = x;
            //});

            hub.On<TurnoDTO>("TicketLlamado", x =>
            {
                Turnocopy = x;
            });



            //hub.On<TurnoDTO>("AbandonarTurno", x =>
            //{
            //    //Turno = x;
            //    Turnocopy = null;
            //    Mensaje = "Usted ha abandonado la fila.";
            //    Activo = true;
            //});

            hub.On<TurnoDTO>("TicketAbandonado", x =>
            {
                Turnocopy = null;
                Mensaje = "Usted ha abandonado la fila.";
                Activo = true;
            });



            //hub.On<TurnoDTO>("AtenderCliente", x =>
            //{
            //    Turnocopy = x;
            //});

            hub.On<TurnoDTO>("TicketAtendido", x =>
            {
                Turnocopy = x;
            });


            //hub.On<TurnoDTO>("Terminar", x =>
            //{
            //    Turnocopy = x;
            //    Activo = true;
            //    CargarCajas();
            //});

            hub.On<TurnoDTO>("TicketTerminado", x =>
            {
                Turnocopy = x;
                Activo = true;
                CargarCajas();
            });


            hub.On<string>("CerrarServicio", x =>
            {
               //CargarCajas();

                foreach (var item in ListaCajas)
                {
                    if(Turnocopy.NombreCaja == item.NombreCaja)
                    {
                        Indicador = true;
                    }
                }

                if(Indicador == false)
                {
					CargarCajas();
					Turnocopy = null;
                    //Mensaje = x;
                    Mensaje = "La caja elegida ha sido cerrada. Por favor, genere su ticket nuevamente.";
                    Activo = true;
                }
				
            });
        } 
    }
}

