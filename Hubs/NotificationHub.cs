using Microsoft.AspNetCore.SignalR;

namespace GymBroAspBackend.Hubs
{
    public class NotificationHub: Hub
    {
        public async Task SendMessage(string user , string message)
        {
            await Clients.All.SendAsync("ReciveMessage" , user , message);
        }

        public async Task RTest(string message)
        {
            Console.Write("Messaged recived: " + message);
        }
    }
}