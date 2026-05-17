using System.ComponentModel.DataAnnotations.Schema;

namespace GymBro.Models
{

    [Table("friends")]
    public class Friend
    {
        public int Id {get;set;}
        public int UserId {get;set;}
        public int FriendId {get;set;}

        //Navigation Property
        public User? User {get;set;}
    }
}