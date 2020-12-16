namespace User.API.Services.Interfaces
{
    public interface IAuthService
    {
        string Authenticate(string email, string password);

        string ChangePassword();
    }
}
