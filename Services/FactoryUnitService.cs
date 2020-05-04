using Microsoft.EntityFrameworkCore;
using SweetLife.Models;

namespace Sweets.Services
{
    public class FactoryUnitService
    {
        private readonly SweetLifeDbContext _context;

        public FactoryUnitService(SweetLifeDbContext context)
        {
            _context = context;
        }

        public void Save(FactoryUnit factoryUnit)
        {
            _context.Database.ExecuteSqlRaw($"dbo.SaveFactoryUnit {factoryUnit.ControllerId}, {factoryUnit.CategoryId}, {factoryUnit.FactoryId}");
            _context.SaveChanges();
        }
    }
}