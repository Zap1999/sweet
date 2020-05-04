using System.Collections.Generic;
using System.Linq;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using SweetLife.Models;

namespace Sweets.Services
{
    public class SweetService
    {
        private readonly SweetLifeDbContext _context;

        
        public SweetService(SweetLifeDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Sweet> GetSweet()
        {
            var sweetList = _context.Sweet.FromSqlRaw("SELECT * FROM sweet").ToList();
            var categoryList = _context.Category.FromSqlRaw("SELECT * FROM category").ToList();

            foreach (var sweet in sweetList)
            {
                var category = categoryList.Find(c => c.Id.Equals(sweet.CategoryId));
                if (category == null) continue;
                category.FactoryUnit = null;
                category.Sweet = null;
                sweet.Category = category;
            }

            return sweetList;
        }

        public void Save(Sweet sweet)
        {
            _context.Database.ExecuteSqlRaw($"dbo.SaveSweet {sweet.Name}, {sweet.Description}, {sweet.Price}, {sweet.CategoryId}");
            _context.SaveChanges();
        }

        public IEnumerable<Sweet> GetSweetsByCategoryId(int categoryId)
        {
            var sweets = _context.Sweet.FromSqlRaw($"dbo.GetSweetsByCategoryId {categoryId}").ToList();
            sweets.ForEach(s => s.Category = null);

            return sweets;
        }
    }
}
