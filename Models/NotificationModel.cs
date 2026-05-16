using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace GymBro.Models
{
    [Table("notifications")]
    public class NotificationModel
    {
        [Key]
        [Column("id")]

        public int Id{get;set;}
        [Column("user_id")]
        public int UserId{get;set;}
        [Column("message")]
        [Required]
        public required string Message{get;set;}
        [Column("target_user")]

        public int? TargetUser {get;set;} 
        public bool IsDelivered{get;set;} = true;
        public DateTime Created {get;set;} = DateTime.UtcNow;
    }
}