using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using SweetLife.Models;
using Sweets.ApiModels;

namespace Sweets.Services
{
    public class FactoryService
    {
        private readonly SweetLifeDbContext _context;
        private readonly FactoryUnitService _factoryUnitService;


        public FactoryService(SweetLifeDbContext context)
        {
            _context = context;
            _factoryUnitService = new FactoryUnitService(context);
        }

        public IEnumerable<FullFactoryDto> GetAll()
        {
            var factory = DbProviderFactories.GetFactory(_context.Database.GetDbConnection());

            using var cmd = factory.CreateCommand();
            if (cmd == null) return null;

            cmd.CommandText = $"SELECT * FROM FullFactory";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = _context.Database.GetDbConnection();
            using var adapter = factory.CreateDataAdapter();
            if (adapter == null) return null;

            adapter.SelectCommand = cmd;
            var dataTable = new DataTable();
            adapter.Fill(dataTable);

            var rows = dataTable.Rows;
            if (rows.Count == 0) return null;

            var factoryList = new List<Factory>();
            var factoryUnitList = new List<FactoryUnit>();
            for (var i = 0; i < rows.Count; i++)
            {
                var fId = (long) rows[i]["fId"];
                var fAddress = (string) rows[i]["fAddress"];
                factoryList.Add(new Factory
                {
                    Id = fId,
                    Address = fAddress
                });
                var fuId = (long) rows[i]["fuId"];
                var cId = (long) rows[i]["cId"];
                var cName = (string) rows[i]["cName"];
                var uId = (long) rows[i]["uId"];
                var uFirstName = (string) rows[i]["uFirstName"];
                var uLastName = (string) rows[i]["uLastName"];
                var uEmail = (string) rows[i]["uEmail"];
                var uPassword = (string) rows[i]["uPassword"];
                var uRoleId = (long) rows[i]["uRoleId"];
                factoryUnitList.Add(new FactoryUnit
                {
                    Id = fuId,
                    FactoryId = fId,
                    CategoryId = cId,
                    ControllerId = uId,
                    Factory = null,
                    ManufacturingOrder = null,
                    Category = new Category
                    {
                        Id = cId,
                        Name = cName,
                        FactoryUnit = null,
                        Sweet = null
                    },
                    Controller = new User
                    {
                        Id = uId,
                        Email = uEmail,
                        FactoryUnit = null,
                        FirstName = uFirstName,
                        LastName = uLastName,
                        Password = uPassword,
                        RoleId = uRoleId,
                        Role = null
                    }
                });
            }
            var factories = factoryList.GroupBy(f => f.Id).Select(g => g.First()).ToList();
            factories.ForEach(f => f.FactoryUnit = factoryUnitList.FindAll(fu => fu.FactoryId == f.Id));

            var unitWorkers = new List<UnitWorker>();
            foreach (var unit in factoryUnitList)
            {
                unitWorkers.AddRange(_factoryUnitService.GetUnitWorkers(unit.Id));
            }

            var fullFactoryList = new List<FullFactoryDto>();
            foreach (var fact in factories)
            {
                var dto = new FullFactoryDto {Factory = fact};
                var workers = new List<UnitWorker>();
                foreach (var unit in fact.FactoryUnit)
                {
                    workers.AddRange(unitWorkers.FindAll(w => w.UnitId == unit.Id));
                }

                dto.UnitWorkers = workers;
                fullFactoryList.Add(dto);
            }

            return fullFactoryList;
        }
    }
}