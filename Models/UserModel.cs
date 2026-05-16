using System.ComponentModel.DataAnnotations.Schema;

namespace GymBro.Models
{
    [Table("users")]
    public class User
    {
        [Column("id")]
        public int Id {get;set;}
        [Column("phone")]
        public string Phone{get;set;} = null!;
        [Column("display_name")]
        public string DisplayName {get;set;} = null!;
        [Column("password")]
        public string HashedPassword {get;set;} =String.Empty;
        [Column("refresh_token")]
        public string RefreshToken {get;set;} = String.Empty;
        [Column("refresh_token_expiery")]
        public DateTime RefreshTokenExpiery {get;set;} 
    }
}