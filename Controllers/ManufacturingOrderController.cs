using Microsoft.AspNetCore.Mvc;
using SweetLife.Models;
using Sweets.ApiModels;
using Sweets.Services;

namespace Sweets.Controllers
{
    [Route("api/manufacturing")]
    [ApiController]
    public class ManufacturingOrderController : ControllerBase
    {
        private readonly ManufacturingOrderService _service;

        
        public ManufacturingOrderController()
        {
            _service = new ManufacturingOrderService(new SweetLifeDbContext());
        }

        [HttpPost]
        public void Post([FromBody] ManufacturingOrderPostDto manufacturingOrderPostDto)
        {
            _service.Post(manufacturingOrderPostDto);
        }
    }
}