using System;

namespace User.API.Services.Interfaces
{
    public interface IAuthService
    {
        string Authenticate(string email, string password);

        void ChangePassword(Guid id, string newPassword);
    }
}
