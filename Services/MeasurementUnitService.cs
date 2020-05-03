using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using SweetLife.Models;
using Sweets.ApiModels;

namespace Sweets.Services
{
    public class MeasurementUnitService
    {
        private readonly SweetLifeDbContext _context;


        public MeasurementUnitService(SweetLifeDbContext context)
        {
            _context = context;
        }

        public IEnumerable<MeasurementUnitDto> GetMeasurementUnit()
        {
            var measurementUnitList = _context.MeasurementUnit.FromSqlRaw("SELECT * FROM measurement_unit").ToList();

            return measurementUnitList.Select(measurementUnit => new MeasurementUnitDto() {Id = measurementUnit.Id, Name = measurementUnit.Name}).ToList();
        }
    }
}