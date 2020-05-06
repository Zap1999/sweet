using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using SweetLife.Models;
using Sweets.ApiModels;

namespace Sweets.Services
{
    public class SweetStorageService
    {
        private readonly SweetLifeDbContext _context;


        public SweetStorageService(SweetLifeDbContext context)
        {
            _context = context;
        }
        
        public void UpdateSweetStorage(int sweetId, int factoryId, int count)
        {
            _context.Database.ExecuteSqlRaw($"dbo.UpdateSweetStorage {sweetId}, {factoryId}, {count}");
            _context.SaveChanges();
        }

        public IEnumerable<SweetStorage> GetSweetStorageForFactory(long factoryIdArg)
        {
            var factory = DbProviderFactories.GetFactory(_context.Database.GetDbConnection());

            using var cmd = factory.CreateCommand();
            if (cmd == null) return null;
            
            cmd.CommandText = $"SELECT * FROM SweetStorageFull WHERE FactoryId = {factoryIdArg}";
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
                let sweetId = (long) row["SweetId"]
                let sweetName = (string) row["SweetName"]
                let sweetDescription = (string) row["SweetDescription"]
                let sweetPrice = (decimal) row["SweetPrice"]
                let categoryId = (long) row["CategoryId"]
                let categoryName = (string) row["CategoryName"]
                let sweetCount = (long) row["SweetCount"]
                let factoryId = (long) row["FactoryId"]
                let factoryAddress = (string) row["FactoryAddress"]
                select new SweetStorage
                {
                    SweetId = sweetId,
                    FactoryId = factoryId,
                    Count = sweetCount,
                    Sweet = new Sweet
                    {
                        Id = sweetId,
                        Name = sweetName,
                        Description = sweetDescription,
                        Price = sweetPrice,
                        CategoryId = categoryId,
                        Category = new Category {Id = categoryId, Name = categoryName, FactoryUnit = null, Sweet = null}
                    },
                    Factory = new Factory {Id = factoryId, Address = factoryAddress, FactoryUnit = null}
                }).ToList();
        }
    }
}