using AppCliente.Models.DTOs;
using AppCliente.Views;
using Microsoft.AspNetCore.SignalR.Client;

namespace AppCliente
{
    public partial class App : Application
    {
        //public static HubConnection hub { get; private set; }
        //TurnoDTO dto = new();
        public App()
        {
            InitializeComponent();

            MainPage = new AppShell();
        }

        //protected override async void OnStart()
        //{
        //    hub = new HubConnectionBuilder()
        //    .WithUrl("https://localhost:44385/turnos")
        //    .WithAutomaticReconnect()
        //    .Build();

        //    await hub.StartAsync();

        //    hub.Closed += async (error) =>
        //    {
        //        await Task.Delay(1000);
        //        await hub.StartAsync();
        //    };

        //    MainThread.BeginInvokeOnMainThread(() =>
        //   {
        //       hub.On<TurnoDTO>("TurnoNuevo", x =>
        //         {
        //          MainThread.BeginInvokeOnMainThread(() =>
        //          {
        //              GenerarTicketView.NumTurnoLabel.Text = x.IdTurno.ToString();
        //              GenerarTicketView.PruebaLabel.Text = x.EstadoTurno;
        //          });
        //      });
        //   });

        //    hub.On<TurnoDTO>("LlamadoCliente", x =>
        //    {
        //        //MainThread.BeginInvokeOnMainThread(() =>
        //        //{
        //        //    GenerarTicketView.NumTurnoLabel.Text = x.IdTurno.ToString();
        //        //    GenerarTicketView.PruebaLabel.Text = x.EstadoTurno;
        //        //});
        //        MainThread.BeginInvokeOnMainThread(() =>
        //        {
        //            GenerarTicketView.UpdateUI(x);
        //        });
        //    });

            
        //}
    }
}
