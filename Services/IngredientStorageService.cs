using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
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

        public IEnumerable<IngredientStorage> GetByFactoryId(long factoryId)
        {
            var factory = DbProviderFactories.GetFactory(_context.Database.GetDbConnection());

            using var cmd = factory.CreateCommand();
            if (cmd == null) return null;
            
            cmd.CommandText = $"SELECT * FROM dbo.IngredientStorageFull WHERE fId = {factoryId}";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = _context.Database.GetDbConnection();
            using var adapter = factory.CreateDataAdapter();
            if (adapter == null) return null;

            adapter.SelectCommand = cmd;
            var dataTable = new DataTable();
            adapter.Fill(dataTable);

            var rows = dataTable.Rows;
            if (rows.Count == 0) return null;

            return (from DataRow row in rows
                select new IngredientStorage
                {
                    Count = (decimal) row["isCount"],
                    FactoryId = (long) row["fId"],
                    IngredientId = (long) row["iId"],
                    Factory = new Factory
                        {Id = (long) row["fId"], Address = (string) row["fAddress"], FactoryUnit = null},
                    Ingredient = new Ingredient
                    {
                        Id = (long) row["iId"],
                        Name = (string) row["iName"],
                        Price = (decimal) row["iPrice"],
                        MeasurementUnitId = (long) row["muId"],
                        MeasurementUnit = new MeasurementUnit
                            {Id = (long) row["muId"], Name = (string) row["muName"], Ingredient = null}
                    }
                }).ToList();
        }
    }
}