using System;
using System.Collections.Generic;
using System.Linq;
using User.API.Entities;
using User.API.Services.Interfaces;

namespace User.API.Services
{
    public class UserService : IUserService
    {
        private readonly ApplicationContext _applicationContext;

        public UserService(ApplicationContext applicationContext)
        {
            _applicationContext = applicationContext;
        }

        public Entities.User Get(Guid id) => _applicationContext.User.FirstOrDefault(i => i.Id == id);

        public Entities.User GetByEmail(string email) => _applicationContext.User.FirstOrDefault(i => i.Email == email);

        public IEnumerable<Entities.User> List() => _applicationContext.User.AsEnumerable();

        public void Add(Entities.User user)
        {
            (string password, string salt) = user.Password.GenerateSaltAndHashPassword();
            user.Password = password;
            user.Salt = salt;
            user.Id = Guid.NewGuid();

            _applicationContext.Add(user);
            _applicationContext.SaveChanges();
        }

        public void Delete(Guid id)
        {
            var user = _applicationContext.User.FirstOrDefault(i => i.Id == id);
            _applicationContext.Remove(user);
            _applicationContext.SaveChanges();
        }

        public void Update(Guid id, Entities.User user)
        {
            var usr = _applicationContext.User.FirstOrDefault(i => i.Id == id);

            usr.Name = user.Name;

            _applicationContext.Update(usr);
            _applicationContext.SaveChanges();
        }
    }
}
