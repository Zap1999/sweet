using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using SweetLife.Models;
using Sweets.ApiModels;

namespace Sweets.Services
{
    public class SweetService
    {
        private readonly SweetLifeDbContext _context;

        
        public SweetService(SweetLifeDbContext context)
        {
            _context = context;
        }

        public IEnumerable<SweetDto> GetSweet()
        {
            var sweetList = _context.Sweet.FromSqlRaw("SELECT * FROM sweet").ToList();
            var categoryList = _context.Category.FromSqlRaw("SELECT * FROM category").ToList();

            return (from sweet in sweetList
                let category = categoryList.Find(c => c.Id == sweet.CategoryId)
                select new SweetDto()
                {
                    Id = sweet.Id,
                    Description = sweet.Description,
                    Name = sweet.Name,
                    Price = sweet.Price,
                    Category = category.Name
                }).ToList();
        }
    }
}
