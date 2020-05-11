using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using SweetLife.Models;
using Sweets.ApiModels;

namespace Sweets.Services
{
    public class IngredientService
    {
        private const string InsertIngredientSqlCommand =
            "INSERT INTO [dbo].[ingredient] ([name], [price], [measurement_unit_id]) VALUES (@name, @price, @measurement_unit_id)";

        private const string UpdateIngredientStorageSqlCommand =
            "UPDATE [dbo].[ingredient_storage] SET [count] = @count WHERE ingredient_id = @ingredient_id AND factory_id = @factory_id";

        private readonly SweetLifeDbContext _context;
        private readonly FactoryService _factoryService;


        public IngredientService(SweetLifeDbContext context)
        {
            _context = context;
            _factoryService = new FactoryService(context);
        }

        public void Save(Ingredient ingredient)
        {
            var name = new SqlParameter("name", ingredient.Name);
            var price = new SqlParameter("price", ingredient.Price);
            var measurementUnitId = new SqlParameter("measurement_unit_id", ingredient.MeasurementUnitId);

            _context.Database.ExecuteSqlCommand(InsertIngredientSqlCommand, name, price, measurementUnitId);
            _context.SaveChanges();
            
            _context.Ingredient.Load();
            var id = _context.Ingredient.FromSqlRaw("SELECT * FROM ingredient WHERE ingredient.id = (SELECT MAX(id) FROM ingredient)").First().Id;
            _context.Database.ExecuteSqlRaw($"INSERT INTO ingredient_storage (ingredient_id, factory_id, [count]) SELECT {id}, factory.id, 0 FROM factory");
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
            var ingredients = _context.Ingredient.FromSqlRaw("SELECT * FROM ingredient").ToList();
            var measurementUnits = _context.MeasurementUnit.FromSqlRaw("SELECT * FROM measurement_unit").ToList();
            foreach (var ingredient in ingredients)
            {
                ingredient.MeasurementUnit = measurementUnits.Find(mu => mu.Id == ingredient.MeasurementUnitId);
                ingredient.MeasurementUnit.Ingredient = null;
            }

            return ingredients;
        }

        public IngredientsExpanseDataDto GetAllIngredientsExpanseDataForPeriod(DateTime startDate,
            DateTime endDate)
        {
            var factory = DbProviderFactories.GetFactory(_context.Database.GetDbConnection());

            using var cmd = factory.CreateCommand();
            if (cmd == null) return null;

            cmd.CommandText = $"EXEC dbo.GetAllIngredientsForPeriod '{startDate:yyyy-MM-dd}', '{endDate:yyyy-MM-dd}'";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = _context.Database.GetDbConnection();
            using var adapter = factory.CreateDataAdapter();
            if (adapter == null) return null;

            adapter.SelectCommand = cmd;
            var dataTable = new DataTable();
            adapter.Fill(dataTable);

            var rows = dataTable.Rows;
            if (rows.Count == 0) return null;

            var ingredients = new List<Ingredient>();
            var idToCount = new Dictionary<string, decimal>();
            foreach (DataRow row in rows)
            {
                ingredients.Add(new Ingredient
                {
                    Id = (long) row["iId"],
                    Name = (string) row["iName"],
                    Price = (decimal) row["iPrice"],
                    MeasurementUnitId = (long) row["muId"],
                    MeasurementUnit = new MeasurementUnit
                    {
                        Id = (long) row["muId"],
                        Name = (string) row["muName"],
                        Ingredient = null
                    }
                });
                idToCount.Add(
                    ((long) row["iId"]).ToString(),
                    (decimal) row["iCount"]
                    );
            }

            return new IngredientsExpanseDataDto
            {
                Ingredients = ingredients,
                Counts = idToCount
            };
        }

        public IngredientsExpanseDataDto GetAllIngredientsExpanseDataForFactoryAndPeriod(long factoryId, DateTime startDate, DateTime endDate)
        {
            var factory = DbProviderFactories.GetFactory(_context.Database.GetDbConnection());

            using var cmd = factory.CreateCommand();
            if (cmd == null) return null;

            cmd.CommandText = $"EXEC dbo.GetAllIngredientsForFactoryAndPeriod {factoryId}, '{startDate:yyyy-MM-dd}', '{endDate:yyyy-MM-dd}'";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = _context.Database.GetDbConnection();
            using var adapter = factory.CreateDataAdapter();
            if (adapter == null) return null;

            adapter.SelectCommand = cmd;
            var dataTable = new DataTable();
            adapter.Fill(dataTable);

            var rows = dataTable.Rows;
            if (rows.Count == 0) return null;

            var ingredients = new List<Ingredient>();
            var idToCount = new Dictionary<string, decimal>();
            foreach (DataRow row in rows)
            {
                ingredients.Add(new Ingredient
                {
                    Id = (long) row["iId"],
                    Name = (string) row["iName"],
                    Price = (decimal) row["iPrice"],
                    MeasurementUnitId = (long) row["muId"],
                    MeasurementUnit = new MeasurementUnit
                    {
                        Id = (long) row["muId"],
                        Name = (string) row["muName"],
                        Ingredient = null
                    }
                });
                idToCount.Add(
                    ((long) row["iId"]).ToString(),
                    (decimal) row["iCount"]
                );
            }

            return new IngredientsExpanseDataDto
            {
                Ingredients = ingredients,
                Counts = idToCount
            };
        }
    }
}