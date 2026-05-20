using GymBro.Dto;
using GymBro.Models;
using Microsoft.EntityFrameworkCore;

namespace GymBro.Services
{
    public class NotificationService(GymBroDbContext _dbContext) : INotificationService
    {
        //= Sends the user a friend request in real time
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

        //= Allows the user to retrive friends request 
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

        //= The user accepts the friend request 
        public async Task AcceptFriendRequestAsync(int userId, int friendId)
        {
            try
            {
                var friend = new Friend
                {
                    UserId = userId,
                    FriendId = friendId
                };
                await _dbContext.Friends.AddAsync(friend);
                var linesWritten = await _dbContext.SaveChangesAsync();
                if(linesWritten == 1) /// That means the user successfuly added the the request sender
                {
                    var friendReqeustToRemove =await  _dbContext.Notifications.FirstAsync(n => n.UserId == userId && n.TargetUser == friendId);
                    _dbContext.Notifications.Remove(friendReqeustToRemove);
                    await _dbContext.SaveChangesAsync();
                }
            }
            catch(Exception ex)
            {
                throw new Exception("Cant add to friends list " + ex.Message);
            }
        }
    }
}