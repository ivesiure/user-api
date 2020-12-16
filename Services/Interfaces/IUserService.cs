using System;
using System.Collections.Generic;

namespace User.API.Services.Interfaces
{
    public interface IUserService
    {
        IEnumerable<Entities.User> List();

        Entities.User Get(Guid id);

        Entities.User GetByEmail(string email);

        void Add(Entities.User user);

        void Update(Guid id, Entities.User user);

        void UpdatePassword(Guid id, string password, string salt);

        void Delete(Guid id);
    }
}
