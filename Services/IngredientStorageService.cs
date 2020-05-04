using Microsoft.EntityFrameworkCore;
using SweetLife.Models;

namespace Sweets.Services
{
    public class IngredientStorageService
    {
        private readonly SweetLifeDbContext _context;


        public IngredientStorageService(SweetLifeDbContext context)
        {
            _context = context;
        }
        
        public void UpdateIngredientStorage(int ingredientId, int factoryId, decimal count)
        {
            _context.Database.ExecuteSqlRaw($"dbo.UpdateIngredientStorage {ingredientId}, {factoryId}, {count}");
            _context.SaveChanges();
        }
    }
}