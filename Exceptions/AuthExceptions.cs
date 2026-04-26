namespace GymBroAspBackend.Exceptions
{
    public class InvalidCredentialsException : Exception
    {
        public InvalidCredentialsException() : base("Invalid credentials"){}
    }

    public class RefreshTokenExpiredException : Exception
    {
        public RefreshTokenExpiredException() : base("Refresh token expired"){}
    }
}