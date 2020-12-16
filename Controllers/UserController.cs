using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using User.API.Models;
using User.API.Services.Interfaces;

namespace User.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private readonly IMapper _mapper;
        private readonly IUserService _userService;

        public UserController(ILogger<UserController> logger, IMapper mapper, IUserService userService)
        {
            _logger = logger;
            _mapper = mapper;
            _userService = userService;
        }

        /// <summary>
        /// Get a list of users.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IEnumerable<UserViewModel> Get()
        {
            try
            {
                Response.StatusCode = 200;
                var users = _userService.List();
                return _mapper.Map<IEnumerable<UserViewModel>>(users);
            }
            catch (Exception ex)
            {
                Response.StatusCode = 500;
                _logger.LogError(ex, "UserController/Get");
                return null;
            }
        }

        /// <summary>
        /// Gets a specific user based on its ID
        /// </summary>
        [HttpGet("{id}")]
        public UserViewModel Get(Guid id)
        {
            try
            {
                Response.StatusCode = 200;

                var user = _userService.Get(id);
                return _mapper.Map<UserViewModel>(user);
            }
            catch (Exception ex)
            {
                Response.StatusCode = 500;
                _logger.LogError(ex, $"UserController/Get/{id}");
                return null;
            }
        }

        /// <summary>
        /// Creates a new user
        /// </summary>
        [HttpPost()]
        public void Post([FromBody] UserViewModel userView)
        {
            try
            {
                Response.StatusCode = 200;
                var user = _mapper.Map<Entities.User>(userView);
                _userService.Add(user);
            }
            catch (Exception ex)
            {
                Response.StatusCode = 500;
                _logger.LogError(ex, "UserController/Post");
            }
        }

        /// <summary>
        /// Updates a user's name
        /// </summary>
        [HttpPatch("{id}")]
        public void Patch(Guid id, [FromBody] UserViewModel userView)
        {
            try
            {
                Response.StatusCode = 200;
                var user = _mapper.Map<Entities.User>(userView);
                _userService.Update(id, user);
            }
            catch (Exception ex)
            {
                Response.StatusCode = 500;
                _logger.LogError(ex, "UserController/Patch");
            }
        }

        /// <summary>
        /// Deletes a user
        /// </summary>
        [HttpDelete("{id}")]
        public void Delete(Guid id)
        {
            try
            {
                Response.StatusCode = 200;
                _userService.Delete(id);
            }
            catch (Exception ex)
            {
                Response.StatusCode = 500;
                _logger.LogError(ex, "UserController/Delete");
            }
        }
    }
}
