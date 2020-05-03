using Microsoft.AspNetCore.Mvc;
using SweetLife.Models;
using Sweets.ApiModels;
using Sweets.Services;


namespace Sweets.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserService _service;


        public UserController()
        {
            _service = new UserService(new SweetLifeDbContext());
        }

        [HttpPost("register")]
        public void Post([FromBody] User user)
        {
            _service.Register(user);
        }

        [HttpPost("login")]
        public User Post([FromBody] LogInForm form)
        {
            return _service.LogIn(form);
        }
    }
}
