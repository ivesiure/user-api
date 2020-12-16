using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using User.API.Models;
using User.API.Services.Interfaces;

namespace User.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly ILogger<AuthController> _logger;
        private readonly IAuthService _authService;

        public AuthController(ILogger<AuthController> logger, IAuthService authService)
        {
            _logger = logger;
            _authService = authService;
        }

        [HttpPost(), AllowAnonymous]
        public object Post([FromBody] AuthenticationViewModel authView)
        {
            try
            {
                Response.StatusCode = 200;
                string token = _authService.Authenticate(authView.Email, authView.Password);
                string msg = token != null ? "OK" : "Invalid credentials";
                return new { msg, token };
            }
            catch (Exception ex)
            {
                Response.StatusCode = 500;
                _logger.LogError(ex, "AuthController/Post");
                return null;
            }
        }

        [HttpPatch()]
        public void Patch([FromBody] AuthenticationViewModel authView)
        {
            try
            {
                Response.StatusCode = 200;
                _authService.Authenticate(authView.Email, authView.Password);
            }
            catch (Exception ex)
            {
                Response.StatusCode = 500;
                _logger.LogError(ex, "AuthController/Patch");
            }
        }
    }
}
