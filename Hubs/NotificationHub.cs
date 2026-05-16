using GymBro.Models;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

namespace GymBroAspBackend.Hubs
{
    public class NotificationHub : Hub
    {
        private GymBroDbContext _dbContext;

        public NotificationHub(GymBroDbContext context)
        {
            _dbContext = context;
        }

        public async Task SendFriendRequest(int reciverId)
        {
            try
            {
                var userId = Context.UserIdentifier;
                await Clients.User(reciverId.ToString()).SendAsync("FriendRequest", new { userId, message = $"user {userId} wants to connect with you;" });

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}