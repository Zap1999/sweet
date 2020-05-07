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
        public void Save([FromRoute] long userId, [FromRoute] long unitId)
        {
            _service.SaveUnitWorker(userId, unitId);
        }

        [HttpDelete("{unitId}/{userId}")]
        public void Delete([FromRoute] long userId, [FromRoute] long unitId)
        {
            _service.Delete(unitId, userId);
        }
    }
}