namespace WebAPIServer.Services
{
    public class AuthenticateResponse
    {
        public string Jwt { get; set; }
        public string RefreshToken { get; set; }
    }
}