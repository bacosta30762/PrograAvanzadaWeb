using Microsoft.AspNetCore.SignalR;

namespace ProyectoPrograAvanzadaWeb.Alertas
{
    public class AlertasHub : Hub
    {
        
            public async Task AFlightAlert(string message)
            {
                await Clients.All.SendAsync("ReceiveMessage", message);
            }
        
        
    }
}
