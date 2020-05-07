using Microsoft.EntityFrameworkCore;
using SweetLife.Models;

namespace Sweets.Services
{
    public class CategoryService
    {
        private readonly SweetLifeDbContext _context;

        
        public CategoryService(SweetLifeDbContext context)
        {
            _context = context;
        }

        public void Save(Category category)
        {
            _context.Database.ExecuteSqlRaw($"dbo.SaveCategory {category.Name}");
            _context.SaveChanges();
        }
    }
}