using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
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
            var deadlineDate = new SqlParameter("deadline_date", manufacturingOrder.DeadlineDate);

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

            var parameters = new[]
            {
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
                    Value = manufacturingOrderPostDto.DeadLineDate
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
                "dbo.SaveManufacturingOrder @DeadLineDate, @FactoryUnitId, @StatusId, @ManufacturingOrderItems",
                parameters);
            _context.SaveChanges();
        }

        public ManufacturingOrderFullDto GetFullManufacturingOrder(int id)
        {
            var factory = DbProviderFactories.GetFactory(_context.Database.GetDbConnection());

            using var cmd = factory.CreateCommand();
            if (cmd == null) return null;
            
            cmd.CommandText = $"SELECT * FROM ManufacturingOrderFull WHERE ManufacturingOrderId = {id}";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = _context.Database.GetDbConnection();
            using var adapter = factory.CreateDataAdapter();
            if (adapter == null) return null;
            
            adapter.SelectCommand = cmd;
            var dataTable = new DataTable();
            adapter.Fill(dataTable);

            var rows = dataTable.Rows;
            if (rows.Count == 0) return null;

            var manufacturingOrderId = (long) rows[0]["ManufacturingOrderId"];
            var manufacturingOrderCreationDate = (DateTime) rows[0]["ManufacturingOrderCreationDate"];
            var manufacturingOrderDeadlineDate = (DateTime) rows[0]["ManufacturingOrderDeadlineDate"];
            var manufacturingOrderStatusId = (long) rows[0]["ManufacturingOrderStatusId"];
            var manufacturingOrderStatusName = (string) rows[0]["ManufacturingOrderStatusName"];
            var factoryUnitId = (long) rows[0]["FactoryUnitId"];
            var factoryId = (long) rows[0]["FactoryId"];
            var factoryAddress = (string) rows[0]["FactoryAddress"];
            var categoryId = (long) rows[0]["CategoryId"];
            var categoryName = (string) rows[0]["CategoryName"];
            var userId = (long) rows[0]["UserId"];
            var userFirstName = (string) rows[0]["UserFirstName"];
            var userLastName = (string) rows[0]["UserLastName"];
            var userEmail = (string) rows[0]["UserEmail"];
            var userPassword = (string) rows[0]["UserPassword"];
            var roleId = (long) rows[0]["RoleId"];
            var roleName = (string) rows[0]["RoleName"];
            var roleSalary = (long) rows[0]["RoleSalary"];


            var sweetIngredientList = new List<SweetIngredient>();
            var manufacturingOrderList = new List<ManufacturingOrderItem>();
            for (var i = 0; i < rows.Count; i++)
            {
                var sweetIngredientCount = (decimal) rows[i]["SweetIngredientCount"];
                var ingredientId = (long) rows[i]["IngredientId"];
                var ingredientName = (string) rows[i]["IngredientName"];
                var ingredientPrice = (decimal) rows[i]["IngredientPrice"];
                var measurementUnitId = (long) rows[i]["MeasurementUnitId"];
                var measurementUnitName = (string) rows[i]["MeasurementUnitName"];
                sweetIngredientList.Add(new SweetIngredient
                {
                    Count = sweetIngredientCount,
                    Ingredient = new Ingredient
                    {
                        Id = ingredientId,
                        Name = ingredientName,
                        Price = ingredientPrice,
                        MeasurementUnitId = measurementUnitId,
                        MeasurementUnit = new MeasurementUnit
                        {
                            Id = measurementUnitId,
                            Ingredient = null,
                            Name = measurementUnitName
                        }
                    }
                });

                var sweetId = (long) rows[i]["SweetId"];
                var manufacturingOrderItemCount = (int) rows[i]["ManufacturingOrderItemCount"];
                var sweetName = (string) rows[i]["SweetName"];
                var sweetDescription = (string) rows[i]["SweetDescription"];
                var sweetPrice = (decimal) rows[i]["SweetPrice"];
                manufacturingOrderList.Add(new ManufacturingOrderItem
                {
                    Count = manufacturingOrderItemCount,
                    ManufacturingOrder = null,
                    ManufacturingOrderId = manufacturingOrderId,
                    Sweet = new Sweet
                    {
                        Category = null,
                        CategoryId = categoryId,
                        Description = sweetDescription,
                        Id = sweetId,
                        Name = sweetName,
                        Price = sweetPrice
                    },
                    SweetId = sweetId
                });
            }

            var manufacturingOrderFullDto = new ManufacturingOrderFullDto
            {
                ManufacturingOrder = new ManufacturingOrder
                {
                    Id = manufacturingOrderId,
                    CreationDate = manufacturingOrderCreationDate,
                    StatusId = manufacturingOrderStatusId,
                    Status = new ManufacturingOrderStatus
                    {
                        Id = manufacturingOrderStatusId,
                        ManufacturingOrder = null,
                        Name = manufacturingOrderStatusName
                    },
                    FactoryUnitId = factoryUnitId,
                    FactoryUnit = new FactoryUnit
                    {
                        CategoryId = categoryId,
                        Category = new Category
                        {
                            FactoryUnit = null,
                            Id = categoryId,
                            Name = categoryName,
                            Sweet = null
                        },
                        ControllerId = userId,
                        Controller = new User
                        {
                            Email = userEmail,
                            FactoryUnit = null,
                            FirstName = userFirstName,
                            Id = userId,
                            LastName = userLastName,
                            Password = userPassword,
                            RoleId = roleId,
                            Role = new Role
                            {
                                Id = roleId,
                                Name = roleName,
                                Salary = roleSalary,
                                User = null
                            }
                        },
                        Factory = new Factory
                        {
                            Address = factoryAddress,
                            FactoryUnit = null,
                            Id = factoryId
                        },
                        FactoryId = factoryId,
                        Id = factoryUnitId,
                        ManufacturingOrder = null
                    },
                    DeadlineDate = manufacturingOrderDeadlineDate
                },
                ManufacturingOrderItems = manufacturingOrderList,
                SweetsIngredients = sweetIngredientList
            };

            return manufacturingOrderFullDto;
        }
    }
}