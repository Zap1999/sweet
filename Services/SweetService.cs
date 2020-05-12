using System;
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
    public class SweetService
    {
        private readonly SweetLifeDbContext _context;


        public SweetService(SweetLifeDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Sweet> GetSweet()
        {
            var sweetList = _context.Sweet.FromSqlRaw("SELECT * FROM sweet").Include(s => s.Category).ToList();
            sweetList.ForEach(s =>
            {
                s.Category.FactoryUnit = null;
                s.Category.Sweet = null;
            });

            return sweetList;
        }

        public void Save(Sweet sweet, IEnumerable<SweetIngredient> ingredients)
        {
            var dateTable = new DataTable();
            dateTable.Columns.Add(new DataColumn("ingredient_id", typeof(long)));
            dateTable.Columns.Add(new DataColumn("count", typeof(decimal)));
            foreach (var ingredient in ingredients)
            {
                dateTable.Rows.Add(ingredient.IngredientId, ingredient.Count);
            }

            var parameters = new[]
            {
                new SqlParameter("Name", sweet.Name),
                new SqlParameter("Description", sweet.Description),
                new SqlParameter("Price", sweet.Price),
                new SqlParameter("CategoryId", sweet.CategoryId),
                new SqlParameter
                {
                    SqlDbType = SqlDbType.Structured,
                    Direction = ParameterDirection.Input,
                    ParameterName = "SweetIngredientsList",
                    TypeName = "[dbo].[SweetIngredients]",
                    Value = dateTable
                }
            };
            
            _context.Database.ExecuteSqlCommand("dbo.SaveSweet @Name, @Description, @Price, @CategoryId, @SweetIngredientsList",
                parameters);
            _context.SaveChanges();
        }

        public IEnumerable<Sweet> GetSweetsByCategoryId(int categoryId)
        {
            var sweets = _context.Sweet.FromSqlRaw($"dbo.GetSweetsByCategoryId {categoryId}").ToList();
            sweets.ForEach(s => s.Category = null);

            return sweets;
        }

        public void Delete(long sweetId)
        {
            _context.Database.ExecuteSqlRaw($"dbo.DeleteSweet {sweetId}");
        }

        public IEnumerable<SweetFullDto> GetFullAll()
        {
            var factory = DbProviderFactories.GetFactory(_context.Database.GetDbConnection());

            using var cmd = factory.CreateCommand();
            if (cmd == null) return null;

            cmd.CommandText = $"SELECT * FROM FullSweets";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = _context.Database.GetDbConnection();
            using var adapter = factory.CreateDataAdapter();
            if (adapter == null) return null;

            adapter.SelectCommand = cmd;
            var dataTable = new DataTable();
            adapter.Fill(dataTable);

            var rows = dataTable.Rows;
            if (rows.Count == 0) return null;

            var sweetList = new List<Sweet>();
            var sweetIngredients = new List<SweetIngredient>();
            for (var i = 0; i < rows.Count; i++)
            {
                var sId = (long) rows[i]["sId"];
                var sName = (string) rows[i]["sName"];
                var sDescription = (string) rows[i]["sDescription"];
                var sPrice = (decimal) rows[i]["sPrice"];
                var cId = (long) rows[i]["cId"];
                var cName = (string) rows[i]["cName"];
                sweetList.Add(new Sweet
                {
                    Id = sId,
                    CategoryId = cId,
                    Description = sDescription,
                    Name = sName,
                    Price = sPrice,
                    Category = new Category
                    {
                        Id = cId,
                        Name = cName,
                        FactoryUnit = null,
                        Sweet = null
                    }
                });

                var siCount = (decimal) rows[i]["siCount"];
                var iId = (long) rows[i]["iId"];
                var iName = (string) rows[i]["iName"];
                var iPrice = (decimal) rows[i]["iPrice"];
                var muId = (long) rows[i]["muId"];
                var muName = (string) rows[i]["muName"];
                sweetIngredients.Add(new SweetIngredient
                {
                    IngredientId = iId,
                    SweetId = sId,
                    Count = siCount,
                    Sweet = null,
                    Ingredient = new Ingredient
                    {
                        Id = iId,
                        MeasurementUnitId = muId,
                        Name = iName,
                        Price = iPrice,
                        MeasurementUnit = new MeasurementUnit
                        {
                            Id = muId,
                            Name = muName,
                            Ingredient = null
                        }
                    }
                });    
            }

            var sweets = sweetList.GroupBy(s => s.Id).Select(g => g.First()).ToList();
            return sweets.Select(
                sweet => new SweetFullDto
                {
                    Sweet = sweet,
                    SweetIngredients = new List<SweetIngredient>(
                        sweetIngredients.FindAll(si => si.SweetId == sweet.Id)
                    )
                }).ToList();
        }

        public SweetExpanseDataDto GetAllExpanseDataForPeriod(DateTime startDate, DateTime endDate)
        {
            var factory = DbProviderFactories.GetFactory(_context.Database.GetDbConnection());

            using var cmd = factory.CreateCommand();
            if (cmd == null) return null;

            cmd.CommandText = $"EXEC dbo.GetAllSweetsForPeriod '{startDate:yyyy-MM-dd}', '{endDate:yyyy-MM-dd}'";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = _context.Database.GetDbConnection();
            using var adapter = factory.CreateDataAdapter();
            if (adapter == null) return null;

            adapter.SelectCommand = cmd;
            var dataTable = new DataTable();
            adapter.Fill(dataTable);

            var rows = dataTable.Rows;
            if (rows.Count == 0) return null;

            var sweets = new List<Sweet>();
            var idToCount = new Dictionary<string, int>();
            foreach (DataRow row in rows)
            {
                var sweet = new Sweet
                {
                    Id = (long) row["sId"],
                    Name = (string) row["sName"],
                    Price = (decimal) row["sPrice"],
                    Description = (string) row["sDescription"],
                    CategoryId = (long) row["cId"],
                    Category = new Category
                    {
                        Id = (long) row["cId"],
                        Name = (string) row["cName"],
                        FactoryUnit = null,
                        Sweet = null
                    }
                };
                if (sweets.Find(s => s.Id == sweet.Id) == null)
                {
                    sweets.Add(sweet);
                }

                var sId = ((long) row["sId"]).ToString();
                var iCount = (int) row["iCount"];
                if (!idToCount.ContainsKey(sId))
                {
                    idToCount.Add(sId, iCount);
                }
                else
                {
                    var val = idToCount.First(x => x.Key == sId).Value;
                    idToCount.Remove(sId);
                    idToCount.Add(sId, iCount + val);
                }
            }

            sweets.Sort((x, y) => x.Id == y.Id ? 0 : x.Id > y.Id ? 1 : -1);

            return new SweetExpanseDataDto
            {
                Sweets = sweets,
                Counts = idToCount
            };
        }

        public SweetExpanseDataDto GetAllExpanseDataForFactoryAndPeriod(long factoryId, DateTime startDate, DateTime endDate)
        {
            var factory = DbProviderFactories.GetFactory(_context.Database.GetDbConnection());

            using var cmd = factory.CreateCommand();
            if (cmd == null) return null;

            cmd.CommandText = $"EXEC dbo.GetAllSweetsForFactoryAndPeriod {factoryId}, '{startDate:yyyy-MM-dd}', '{endDate:yyyy-MM-dd}'";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = _context.Database.GetDbConnection();
            using var adapter = factory.CreateDataAdapter();
            if (adapter == null) return null;

            adapter.SelectCommand = cmd;
            var dataTable = new DataTable();
            adapter.Fill(dataTable);

            var rows = dataTable.Rows;
            if (rows.Count == 0) return null;

            var sweets = new List<Sweet>();
            var idToCount = new Dictionary<string, int>();
            foreach (DataRow row in rows)
            {
                sweets.Add(new Sweet
                {
                    Id = (long) row["sId"],
                    Name = (string) row["sName"],
                    Price = (decimal) row["sPrice"],
                    Description = (string) row["sDescription"],
                    CategoryId = (long) row["cId"],
                    Category =  new Category
                    {
                        Id = (long) row["cId"],
                        Name = (string) row["cName"],
                        FactoryUnit = null,
                        Sweet = null
                    }
                });
                idToCount.Add(
                    ((long) row["sId"]).ToString(),
                    (int) row["iCount"]
                );
            }

            return new SweetExpanseDataDto
            {
                Sweets = sweets,
                Counts = idToCount
            };
        }
    }
}