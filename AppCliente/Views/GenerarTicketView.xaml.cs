using AppCliente.Helpers;
using AppCliente.Models.DTOs;
using Microsoft.AspNetCore.SignalR.Client;

namespace AppCliente.Views;

public partial class GenerarTicketView : ContentPage
{
    HubConnection hub;
    TurnoDTO dto = new();

    public GenerarTicketView()
	{
		InitializeComponent();

        Task.Run(() => Iniciar());
    }

    private async Task Iniciar()
    {
        hub = new HubConnectionBuilder()
            .WithUrl("https://localhost:44385/turnos")
            .WithAutomaticReconnect()
            .Build();

        await hub.StartAsync();

        MainThread.BeginInvokeOnMainThread(() =>
        {
            hub.On<TurnoDTO>("TurnoNuevo", x =>
            {
                MainThread.BeginInvokeOnMainThread(() =>
                {
                    NumTurno.Text = x.IdTurno.ToString();
                    prueba.Text = x.EstadoTurno;
                });
            });
        });

    }



    private async void generar_Clicked(object sender, EventArgs e)
    {
        //dto.IdCaja = 1;
        await hub.InvokeAsync("AddTurno", 2);
    }

}