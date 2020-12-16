using System;

namespace User.API.Entities
{
    public class User
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string Salt { get; set; }

        public DateTime RegistrationDate { get; set; } = DateTime.Now;
    }
}
