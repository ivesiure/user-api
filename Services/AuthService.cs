using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using User.API.Services.Interfaces;

namespace User.API.Services
{
    public class AuthService : IAuthService
    {
        private readonly string _secretKey;
        private readonly IUserService _userService;

        public AuthService(IConfiguration configuration, IUserService userService)
        {
            _secretKey = configuration.GetValue<string>("SecretKey");
            _userService = userService;
        }

        public string Authenticate(string email, string password)
        {
            var user = _userService.GetByEmail(email);

            if (user == null)
                return null;

            password = password.SaltPassword(user.Salt);

            if (!user.Password.Equals(password))
                return null;

            //Create token
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_secretKey);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim("Id", user.Id.ToString()),
                    new Claim("Name", user.Name),
                    new Claim("Email", user.Email),
                    new Claim("RgDate", user.RegistrationDate.ToString("yyyy-MM-ddTHH:mm:ss")),
                }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public void ChangePassword(Guid id, string newPassword)
        {
            (string password, string salt) = newPassword.GenerateSaltAndHashPassword();

            _userService.UpdatePassword(id, password, salt);
        }
    }
}
