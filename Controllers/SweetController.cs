using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SweetLife.Models;
using SweetLife.Services;

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

        // GET: api/Sweet
        [HttpGet]
        public IEnumerable<Sweet> Get()
        {
            return _service.GetSweet();
        }

        // GET: api/Sweet/5
        [HttpGet("{id}", Name = "GetSweet")]
        public Sweet Get(int id)
        {
            return null;
        }

        // POST: api/Sweet
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/Sweet/5
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
