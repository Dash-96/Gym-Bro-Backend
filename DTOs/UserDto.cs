namespace GymBroAspBackend.DTOs
{
    public class UserDto
    {
        public string Phone{get;set;} = null!;
        
        public string DisplayName {get;set;} = null!;
    
        public string Password {get;set;} = null!;
    }

    public class McpUser
    {
        public int Id {get;set;}
        public string DisplayName {get;set;}
    }
}