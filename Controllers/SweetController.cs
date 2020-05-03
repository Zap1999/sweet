using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using SweetLife.Models;
using Sweets.Services;

namespace Sweets.Controllers
{
    [Route("api/sweet")]
    [ApiController]
    public class SweetController : ControllerBase
    {
        private readonly SweetService _service;


        public SweetController()
        {
            _service = new SweetService(new SweetLifeDbContext());
        }

        [HttpGet]
        public IEnumerable<Sweet> Get()
        {
            return _service.GetSweet();
        }

        [HttpPost]
        public void Post([FromBody] Sweet sweet)
        {
            _service.Save(sweet);
        }
    }
}
