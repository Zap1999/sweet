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
        
        [HttpPost]
        public void Post([FromBody] FactoryUnit factoryUnit)
        {
             _service.Save(factoryUnit);
        }
    }
}