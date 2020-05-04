using System.Data;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using SweetLife.Models;
using Sweets.ApiModels;

namespace Sweets.Services
{
    public class ManufacturingOrderService
    {
        private const string ManufacturingOrderUpdateSqlCommand =
            "UPDATE [dbo].[manufacturing_order] SET [status_id] = @status_id, [factory_unit_id] = @factory_unit_id, [deadline_date] = @deadline_date WHERE id = @id";
        
        private readonly SweetLifeDbContext _context;


        public ManufacturingOrderService(SweetLifeDbContext context)
        {
            _context = context;
        }

        public void Update(int manufacturingOrderId, ManufacturingOrder manufacturingOrder)
        {
            var id = new SqlParameter("id", manufacturingOrderId);
            var statusId = new SqlParameter("status_id", manufacturingOrder.StatusId);
            var factoryUnitId = new SqlParameter("factory_unit_id", manufacturingOrder.FactoryUnitId);
            var deadlineDate = new SqlParameter("deadline_date", manufacturingOrder.Date);

            _context.Database.ExecuteSqlCommand(ManufacturingOrderUpdateSqlCommand, id, statusId, factoryUnitId,
                deadlineDate);
            _context.SaveChanges();
        }

        public void Post(ManufacturingOrderPostDto manufacturingOrderPostDto)
        {
            var dateTable = new DataTable();
            dateTable.Columns.Add(new DataColumn("sweet_id", typeof(int)));
            dateTable.Columns.Add(new DataColumn("count", typeof(int)));
            foreach (var orderItem in manufacturingOrderPostDto.ManufacturingOrderItems)
            {
                dateTable.Rows.Add(orderItem.SweetId, orderItem.Count);
            }
            
            var parameters = new[] {
                new SqlParameter
                {
                    SqlDbType = SqlDbType.Structured,
                    Direction = ParameterDirection.Input,
                    ParameterName = "ManufacturingOrderItems",
                    TypeName = "[dbo].[ManufacturingOrderList]",
                    Value = dateTable
                },
                new SqlParameter
                {
                    SqlDbType = SqlDbType.Date,
                    Direction = ParameterDirection.Input,
                    ParameterName = "DeadLineDate",
                    Value = manufacturingOrderPostDto.Date
                },
                new SqlParameter
                {
                    SqlDbType = SqlDbType.BigInt,
                    Direction = ParameterDirection.Input, 
                    ParameterName = "FactoryUnitId",
                    Value = manufacturingOrderPostDto.FactoryUnitId
                },
                new SqlParameter
                {
                    SqlDbType = SqlDbType.BigInt,
                    Direction = ParameterDirection.Input,
                    ParameterName = "StatusId",
                    Value = manufacturingOrderPostDto.StatusId
                }
            };

            _context.Database.ExecuteSqlCommand(
                "dbo.SaveManufacturingOrder @DeadLineDate, @FactoryUnitId, @StatusId, @ManufacturingOrderItems", parameters);
            _context.SaveChanges();
        }
    }
}