using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using SweetLife.Models;

namespace Sweets.Services
{
    public class IngredientService
    {
        private const string InsertIngredientSqlCommand = "INSERT INTO [dbo].[ingredient] ([name], [price], [measurement_unit_id]) VALUES (@name, @price, @measurement_unit_id)";
        private const string UpdateIngredientStorageSqlCommand =
            "UPDATE [dbo].[ingredient_storage] SET [count] = @count WHERE ingredient_id = @ingredient_id AND factory_id = @factory_id";

        private readonly SweetLifeDbContext _context;


        public IngredientService(SweetLifeDbContext context)
        {
            _context = context;
        }

        public void Save(Ingredient ingredient)
        {
            var name = new SqlParameter("name", ingredient.Name);
            var price = new SqlParameter("price", ingredient.Price);
            var measurementUnitId = new SqlParameter("measurement_unit_id", ingredient.MeasurementUnitId);
            
            _context.Database.ExecuteSqlCommand(InsertIngredientSqlCommand, name, price, measurementUnitId);
            _context.SaveChanges();
        }

        public void UpdateIngredientStorage(int ingredientId, int factoryId, decimal count)
        {
            var ingredientIdParam = new SqlParameter("ingredient_id", ingredientId);
            var factoryIdParam = new SqlParameter("factory_id", factoryId);
            var countParam = new SqlParameter("count", count);
            _context.Database.ExecuteSqlCommand(UpdateIngredientStorageSqlCommand, ingredientIdParam, factoryIdParam,
                countParam);
            _context.SaveChanges();
        }

        public IEnumerable<Ingredient> GetAll()
        {
            return _context.Ingredient.FromSqlRaw("SELECT * FROM ingredient").ToList();
        }
    }
}