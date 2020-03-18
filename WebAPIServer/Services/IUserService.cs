namespace WebAPIServer.Services
{
    public interface IUserService
    {
        AuthenticateResponse Authenticate(string username, string password);
        AuthenticateResponse RenewToken(string username, string refreshToken);
    }
}