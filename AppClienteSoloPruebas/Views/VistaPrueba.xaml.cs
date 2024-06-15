using AppClienteSoloPruebas.Models.DTOs;
using Microsoft.AspNetCore.SignalR.Client;

namespace AppClienteSoloPruebas.Views;

public partial class VistaPrueba : ContentPage
{
	HubConnection hub;
    TurnoDTO dto = new();
    public VistaPrueba()
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


        hub.On<TurnoDTO>("TurnoNuevo", x =>
        {
            MainThread.BeginInvokeOnMainThread(() =>
            {
                NumTicket.Text = x.IdTurno.ToString();
                EstadoTurno.Text = x.EstadoTurno;
            });
          
        });

        hub.On<TurnoDTO>("LlamadoCliente", x =>
        {
            MainThread.BeginInvokeOnMainThread(() =>
            {
                NumTicket.Text = x.IdTurno.ToString();
                EstadoTurno.Text = x.EstadoTurno;
            });
        });

    }

    private async void Button_Clicked(object sender, EventArgs e)
    {
        await hub.InvokeAsync("AddTurno", 2);
    }

}