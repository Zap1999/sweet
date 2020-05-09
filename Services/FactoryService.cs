using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using SweetLife.Models;

namespace Sweets.Services
{
    public class FactoryService
    {
        private readonly SweetLifeDbContext _context;

        
        public FactoryService(SweetLifeDbContext context)
        {
            _context = context;
        }
        
        public IEnumerable<Factory> GetAll()
        {
            var list = _context.Factory.FromSqlRaw("SELECT * FROM factory").ToList();
            list.ForEach(f => f.FactoryUnit = null);

            return list;
        }
    }
}