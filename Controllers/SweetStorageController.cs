using Microsoft.AspNetCore.Mvc;
using SweetLife.Models;
using Sweets.Services;

namespace Sweets.Controllers
{
    [Route("api/sweet_storage")]
    public class SweetStorageController : ControllerBase
    {
        private readonly SweetStorageService _service;


        public SweetStorageController()
        {
            _service = new SweetStorageService(new SweetLifeDbContext());
        }
        
        [HttpPut("{sweetId}/{factoryId}")]
        public void UpdateSweetStorage([FromRoute] int sweetId, [FromRoute] int factoryId, [FromBody] int count)
        {
            _service.UpdateSweetStorage(sweetId, factoryId, count);
        }
    }
}