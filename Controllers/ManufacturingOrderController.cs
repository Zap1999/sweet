using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Security;
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

        [HttpGet("{id}")]
        public ManufacturingOrderFullDto GetFullManufacturingOrder([FromRoute] int id)
        {
            return _service.GetFullManufacturingOrder(id);
        }

        [HttpGet("date/{deadLineDate}")]
        public List<ManufacturingOrderFullDto> GetFullManufacturingOrderForDate([FromRoute] string deadlineDate)
        {
            var date = DateTime.ParseExact(deadlineDate, "yyyyMMdd",
                CultureInfo.InvariantCulture);
            return _service.GetFullManufacturingOrderForDate(date);
        }
        
        [HttpGet("unit/{unitId}")]
        public List<ManufacturingOrderFullDto> GetFullManufacturingOrderForDate([FromRoute] long unitId)
        {
            return _service.GetFullManufacturingOrderForUnit(unitId);
        }

        [HttpPut("{manufacturingOrderId}")]
        public void UpdateManufacturingOrder([FromRoute] int manufacturingOrderId,
            [FromBody] ManufacturingOrder manufacturingOrder)
        {
            _service.Update(manufacturingOrderId, manufacturingOrder);
        }

        [HttpPost]
        public void Post([FromBody] ManufacturingOrderPostDto manufacturingOrderPostDto)
        {
            _service.Post(manufacturingOrderPostDto);
        }

        [HttpDelete("{manufacturingOrderId}")]
        public void Delete([FromRoute] long manufacturingOrderId)
        {
            _service.Delete(manufacturingOrderId);
        }
        
        [HttpDelete("{manufacturingOrderId}/{sweetId}")]
        public void DeleteItem([FromRoute] long manufacturingOrderId, [FromRoute] long sweetId)
        {
            _service.Delete(manufacturingOrderId, sweetId);
        }
    }
}