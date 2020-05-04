using Microsoft.EntityFrameworkCore;
using SweetLife.Models;

namespace Sweets.Services
{
    public class SweetStorageService
    {
        private readonly SweetLifeDbContext _context;


        public SweetStorageService(SweetLifeDbContext context)
        {
            _context = context;
        }
        
        public void UpdateSweetStorage(int sweetId, int factoryId, int count)
        {
            _context.Database.ExecuteSqlRaw($"dbo.UpdateSweetStorage {sweetId}, {factoryId}, {count}");
            _context.SaveChanges();
        }
    }
}