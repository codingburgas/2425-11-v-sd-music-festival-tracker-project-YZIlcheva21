using Microsoft.AspNetCore.SignalR;

namespace MusicFestivalManagementSystem.Services
{
    public class SignalRService : Hub
    {
        public async Task SendUpdate(string message)
        {
            await Clients.All.SendAsync("ReceiveUpdate", message);
        }
    }
}
