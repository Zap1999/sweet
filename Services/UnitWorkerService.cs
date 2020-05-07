using Microsoft.EntityFrameworkCore;
using SweetLife.Models;

namespace Sweets.Services
{
    public class UnitWorkerService
    {
        private readonly SweetLifeDbContext _context;
        
        
        public UnitWorkerService(SweetLifeDbContext sweetLifeDbContext)
        {
            _context = sweetLifeDbContext;
        }

        public void SaveUnitWorker(long userId, long unitId)
        {
            _context.Database.ExecuteSqlRaw($"dbo.SaveUnitWorker {userId}, {unitId}");
            _context.SaveChanges();
        }
    }
}