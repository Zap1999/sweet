using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
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

        [HttpGet]
        public IEnumerable<User> GetAll()
        {
            return _service.GetAll();
        }

        [HttpGet("{roleId}")]
        public IEnumerable<User> GetUsersByRoleId([FromRoute] int roleId)
        {
            return _service.GetUsersByRoleId(roleId);
        }

        [HttpGet("role")]
        public IEnumerable<Role> GetAllRoles()
        {
            return _service.GetAllRoles();
        }
        
        [HttpGet("expanse/{startDate}/{endDate}")]
        public UserExpanseDataDto GetAllExpanseDataForPeriod([FromRoute] string startDate, [FromRoute] string endDate)
        {
            return _service.GetAllExpanseDataForPeriod(
                DateTime.ParseExact(startDate, "yyyyMMdd",
                    CultureInfo.InvariantCulture),
                DateTime.ParseExact(endDate, "yyyyMMdd",
                    CultureInfo.InvariantCulture)
            );
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
