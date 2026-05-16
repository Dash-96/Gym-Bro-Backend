using GymBro.Dto;
using GymBro.Models;
using Microsoft.EntityFrameworkCore;

namespace GymBro.Services
{
    public class NotificationService(GymBroDbContext _dbContext) : INotificationService
    {
        public async Task SendFriendRequest(int senderId, int reciverId)
        {
            try
            {
                var sender = await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == senderId); 
                var notification = new NotificationModel
                {
                    UserId = senderId,
                    TargetUser = reciverId,
                    Message = "Wants to be your bro"
                };
                await _dbContext.Notifications.AddAsync(notification);
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

         public async Task<IEnumerable<NotificationDto>> GetFriendRequestsAsync(int userId)
        {
           var requests = await _dbContext.Notifications.Where(n => n.TargetUser == userId).Join(_dbContext.Users ,notification => notification.UserId , user=>user.Id , (notification , user) => new NotificationDto
           {
               SenderName = user.DisplayName,
               SenderId = user.Id,
               Message = notification.Message
           }).ToListAsync();

           return requests;
        }
    }
}