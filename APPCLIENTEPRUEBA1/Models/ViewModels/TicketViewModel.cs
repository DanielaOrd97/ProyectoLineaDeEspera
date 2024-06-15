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
        public int IdTurno { get; set; }
        public string EstadoTurno { get; set; }

        //[ObservableProperty]
        //private TurnoDTO? turno;



        [RelayCommand]
        public async Task Generar()
        {
            if (Caja != null)
            {
                await hub.InvokeAsync("AddTurno", Caja.Id);            }
        }


        public TicketViewModel()
        {
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



            //    }

            //public partial class GenerarTicketView : ContentPage
            //{
            //    HubConnection hub;
            //    TurnoDTO dto = new();

            //    public GenerarTicketView()
            //    {
            //        InitializeComponent();

            //        Task.Run(() => Iniciar());
            //    }


            //    private async Task Iniciar()
            //    {
            //        hub = new HubConnectionBuilder()
            //            .WithUrl("https://localhost:44385/turnos")
            //            .WithAutomaticReconnect()
            //            .Build();

            //        await hub.StartAsync();


            //        hub.On<TurnoDTO>("TurnoNuevo", x =>
            //        {
            //            MainThread.BeginInvokeOnMainThread(() =>
            //            {
            //                NumTurno.Text = x.IdTurno.ToString();
            //                prueba.Text = x.EstadoTurno;
            //            });
            //        });



            //    }



            //    private async void generar_Clicked(object sender, EventArgs e)
            //    {
            //        //dto.IdCaja = 1;
            //        await hub.InvokeAsync("AddTurno", 1);
            //    }

            //}



        }
    }
}

