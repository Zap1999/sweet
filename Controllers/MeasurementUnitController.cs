using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using SweetLife.Models;
using Sweets.Services;

namespace Sweets.Controllers
{
    [Route("api/measurement")]
    [ApiController]
    public class MeasurementUnitController : ControllerBase
    {
        private readonly MeasurementUnitService _service;


        public MeasurementUnitController()
        {
            _service = new MeasurementUnitService(new SweetLifeDbContext());
        }

        [HttpGet]
        public IEnumerable<MeasurementUnit> Get()
        {
            return _service.GetMeasurementUnit();
        }
    }
}