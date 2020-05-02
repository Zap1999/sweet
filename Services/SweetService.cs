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

        public List<Sweet> getSweet()
        {
            return _context.Sweet.FromSqlRaw("SELECT * from sweet").ToList();
        }
    }
}
