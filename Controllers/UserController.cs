using Microsoft.AspNetCore.Mvc;
using SweetLife.Models;
using Sweets.HelperModels;
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


        // Endpoints are not currenltly in use
        // GET: api/User
        [HttpGet]
        public string Get()
        {
            return "someVal";
        }

        // GET: api/User/5
        [HttpGet("{id}", Name = "GetUser")]
        public string Get(int id)
        {
            return "value";
        }

        // PUT: api/User/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
