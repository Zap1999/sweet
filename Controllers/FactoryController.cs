using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using SweetLife.Models;
using Sweets.ApiModels;
using Sweets.Services;

namespace Sweets.Controllers
{
    [Route("api/factory")]
    [ApiController]
    public class FactoryController : ControllerBase
    {
        private readonly FactoryService _service;

        public FactoryController()
        {
            _service = new FactoryService(new SweetLifeDbContext());
        }
        
        [HttpGet]
        public IEnumerable<FullFactoryDto> GetAll()
        {
            return _service.GetAll();
        }
    }
}