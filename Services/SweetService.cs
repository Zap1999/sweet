using System.Collections.Generic;
using System.Linq;
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
    }
}
