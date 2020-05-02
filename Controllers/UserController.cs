using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
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

        public UserController()
        {
            _service = new UserService(new SweetLifeDbContext());
        }

        // GET: api/User
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
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
