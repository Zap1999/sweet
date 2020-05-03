using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using SweetLife.Models;

namespace Sweets.Services
{
    public class IngredientService
    {
        private const string InsertIngredientSqlCommand = "INSERT INTO [dbo].[ingredient] ([name], [price], [measurement_unit_id]) VALUES (@name, @price, @measurement_unit_id)";

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
            
            #pragma warning disable CS0618 // Type or member is obsolete
            _context.Database.ExecuteSqlCommand(InsertIngredientSqlCommand, name, price, measurementUnitId);
            #pragma warning restore CS0618 // Type or member is obsolete
            _context.SaveChanges();
        }
    }
}