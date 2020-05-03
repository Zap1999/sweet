using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using SweetLife.Models;

namespace Sweets.Services
{
    public class MeasurementUnitService
    {
        private readonly SweetLifeDbContext _context;


        public MeasurementUnitService(SweetLifeDbContext context)
        {
            _context = context;
        }

        public IEnumerable<MeasurementUnit> GetMeasurementUnit()
        {
            var measurementUnitList = _context.MeasurementUnit.FromSqlRaw("SELECT * FROM measurement_unit").ToList();
            foreach (var measurementUnit in measurementUnitList)
            {
                measurementUnit.Ingredient = null;
            }

            return measurementUnitList;
        }
    }
}