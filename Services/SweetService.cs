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
            var sweetList = _context.Sweet.FromSqlRaw("SELECT * FROM sweet").ToList();
            var categoryList = _context.Category.FromSqlRaw("SELECT * FROM category").ToList();

            foreach (var sweet in sweetList)
            {
                var category = categoryList.Find(c => c.Id.Equals(sweet.CategoryId));
                if (category == null) continue;
                category.FactoryUnit = null;
                category.Sweet = null;
                sweet.Category = category;
            }

            return sweetList;
        }

        public void Save(Sweet sweet)
        {
            _context.Database.ExecuteSqlRaw(
                $"dbo.SaveSweet {sweet.Name}, {sweet.Description}, {sweet.Price}, {sweet.CategoryId}");
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

            var sweets = new HashSet<Sweet>(sweetList);
            return sweets.Select(
                sweet => new SweetFullDto
                {
                    Sweet = sweet,
                    SweetIngredients = new List<SweetIngredient>(
                        sweetIngredients.FindAll(si => si.SweetId == sweet.Id)
                    )
                }).ToList();
        }
    }
}