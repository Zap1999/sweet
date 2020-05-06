using System.Collections;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using SweetLife.Models;
using Sweets.Services;

namespace Sweets.Controllers
{
    [Route("api/factory_unit")]
    [ApiController]
    public class FactoryUnitController : ControllerBase
    {
        private readonly FactoryUnitService _service;

        
        public FactoryUnitController()
        {
            _service = new FactoryUnitService(new SweetLifeDbContext());
        }

        [HttpGet("{factoryUnitId}")]
        public IEnumerable<UnitWorker> GetUnitWorkers([FromRoute] long factoryUnitId)
        {
            return _service.GetUnitWorkers(factoryUnitId);
        }
        
        [HttpPost]
        public void Post([FromBody] FactoryUnit factoryUnit)
        {
             _service.Save(factoryUnit);
        }
    }
}