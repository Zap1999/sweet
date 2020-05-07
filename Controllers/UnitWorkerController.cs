using Microsoft.AspNetCore.Mvc;
using SweetLife.Models;
using Sweets.Services;

namespace Sweets.Controllers
{
    [Route("api/worker")]
    [ApiController]
    public class UnitWorkerController : ControllerBase
    {
        private readonly UnitWorkerService _service;


        public UnitWorkerController()
        {
            _service = new UnitWorkerService(new SweetLifeDbContext());
        }
        
        [HttpPost("{userId}/{unitId}")]
        public void saveUnitWorker([FromRoute] long userId, [FromRoute] long unitId)
        {
            _service.SaveUnitWorker(userId, unitId);
        }
    }
}