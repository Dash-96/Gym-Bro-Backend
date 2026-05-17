using GymBro.Dto;

namespace GymBro.Services
{
    public interface INotificationService
    {
         Task SendFriendRequest(int senderId , int reciverId);
         Task<IEnumerable<NotificationDto>> GetFriendRequestsAsync(int userId);
         Task AcceptFriendRequestAsync(int userId , int friendId);
    }
}