using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppCliente.Helpers
{
    public class SignalRHelper
    {
        private readonly HubConnection hub;
        public SignalRHelper()
        {
            hub = new HubConnectionBuilder()
            .WithUrl("https://localhost:44385/turnos")
            .WithAutomaticReconnect()
            .Build();
        }

        public async Task StartConnectionAsync()
        {
            await hub.StartAsync();
        }

        public HubConnection GetHubConnection()
        {
            return hub;
        }
    }
}
