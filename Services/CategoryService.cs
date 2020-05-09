using System.Collections.Generic;
using System.Linq;
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

        public void Delete(long categoryId)
        {
            _context.Database.ExecuteSqlRaw($"dbo.DeleteCategory {categoryId}");
            _context.SaveChanges();
        }

        public IEnumerable<Category> GetAll()
        {
            var list = _context.Category.FromSqlRaw("SELECT * FROM category").ToList();
            list.ForEach(c =>
            {
                c.FactoryUnit = null;
                c.Sweet = null;
            });
            return list;
        }
    }
}