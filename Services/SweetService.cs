using Microsoft.EntityFrameworkCore;
using SweetLife.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SweetLife.Services
{
    public class SweetService
    {
        public readonly SweetLifeDbContext _context;

        public SweetService(SweetLifeDbContext context)
        {
            _context = context;
        }

        public List<Sweet> GetSweet()
        {
            var sweetList = _context.Sweet
                .FromSqlRaw("select * from sweet")
                .ToList();
/*            var categoryList = _context.Category
                .FromSqlRaw("select * from category")
                .ToList();
            foreach (var sweet in sweetList)
            {
                sweet.Category = categoryList.Find(c => c.Id == sweet.CategoryId);
            }*/

            return sweetList;
        }
    }
}
