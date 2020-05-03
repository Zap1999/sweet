using Microsoft.EntityFrameworkCore;
using SweetLife.Models;
using Sweets.ApiModels;
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

        public List<SweetDto> GetSweet()
        {
            var sweetList = _context.Sweet.FromSqlRaw("SELECT * FROM sweet").ToList();
            var categoryList = _context.Category.FromSqlRaw("SELECT * FROM category").ToList();
            var sweetDtoList = new List<SweetDto>();

            foreach (var sweet in sweetList)
            {
                var category = categoryList.Find(c => c.Id == sweet.CategoryId);
                var sweetDto = new SweetDto() { 
                    Id = sweet.Id, 
                    Description = sweet.Description, 
                    Name = sweet.Name, 
                    Price = sweet.Price, 
                    Category = category.Name 
                };
                sweetDtoList.Add(sweetDto);
            }

            return sweetDtoList;
        }
    }
}
