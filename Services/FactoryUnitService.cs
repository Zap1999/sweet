using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using SweetLife.Models;

namespace Sweets.Services
{
    public class FactoryUnitService
    {
        private readonly SweetLifeDbContext _context;

        public FactoryUnitService(SweetLifeDbContext context)
        {
            _context = context;
        }

        public void Save(FactoryUnit factoryUnit)
        {
            _context.Database.ExecuteSqlRaw(
                $"dbo.SaveFactoryUnit {factoryUnit.ControllerId}, {factoryUnit.CategoryId}, {factoryUnit.FactoryId}");
            _context.SaveChanges();
        }

        public IEnumerable<UnitWorker> GetUnitWorkers(long factoryUnitIdArg)
        {
            var factory = DbProviderFactories.GetFactory(_context.Database.GetDbConnection());

            using var cmd = factory.CreateCommand();
            if (cmd == null) return null;

            cmd.CommandText = $"SELECT * FROM UnitWorkersFull WHERE FactoryUnitId = {factoryUnitIdArg}";
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
                let userId = (long) row["UserId"]
                let userFirstName = (string) row["UserFirstName"]
                let userLastName = (string) row["UserLastName"]
                let userEmail = (string) row["UserEmail"]
                let userPassword = (string) row["UserPassword"]
                let roleId = (long) row["RoleId"]
                let roleName = (string) row["RoleName"]
                let roleSalary = (long) row["RoleSalary"]
                let factoryUnitId = (long) row["FactoryUnitId"]
                let controllerId = (long) row["ControllerId"]
                let factoryId = (long) row["FactoryId"]
                let factoryAddress = (string) row["FactoryAddress"]
                let categoryId = (long) row["CategoryId"]
                let categoryName = (string) row["CategoryName"]
                select new UnitWorker
                {
                    UnitId = factoryUnitId,
                    WorkerId = userId,
                    Unit = new FactoryUnit
                    {
                        Id = factoryUnitId,
                        ControllerId = controllerId,
                        CategoryId = categoryId,
                        FactoryId = factoryId,
                        Controller = null,
                        ManufacturingOrder = null,
                        Category = new Category {Id = categoryId, Name = categoryName, FactoryUnit = null, Sweet = null},
                        Factory = new Factory {Id = factoryId, Address = factoryAddress, FactoryUnit = null}
                    },
                    Worker = new User
                    {
                        Id = userId,
                        FirstName = userFirstName,
                        LastName = userLastName,
                        Email = userEmail,
                        Password = userPassword,
                        RoleId = roleId,
                        Role = new Role {Id = roleId, Name = roleName, Salary = roleSalary, User = null},
                        FactoryUnit = null
                    }
                }).ToList();
        }

        public void Delete(long unitId)
        {
            _context.Database.ExecuteSqlRaw($"dbo.DeleteFactoryUnit {unitId}");
            _context.SaveChanges();
        }
    }
}