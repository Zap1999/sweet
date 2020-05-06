USE sweet_life
GO

CREATE VIEW ManufacturingOrderFull AS
SELECT manufacturing_order.id            AS ManufacturingOrderId,
       manufacturing_order.creation_date AS ManufacturingOrderCreationDate,
       manufacturing_order.deadline_date AS ManufacturingOrderDeadlineDate,
       manufacturing_order_status.id     AS ManufacturingOrderStatusId,
       manufacturing_order_status.name   AS ManufacturingOrderStatusName,
       factory_unit.id                   AS FactoryUnitId,
       factory.id                        AS FactoryId,
       factory.address                   AS FactoryAddress,
       category.id                       AS CategoryId,
       category.name                     AS CategoryName,
       [dbo].[user].id                   AS UserId,
       [dbo].[user].first_name           AS UserFirstName,
       [dbo].[user].last_name            AS UserLastName,
       [dbo].[user].email                AS UserEmail,
       [dbo].[user].password             AS UserPassword,
       role.id                           AS RoleId,
       role.name                         AS RoleName,
       role.salary                       AS RoleSalary,
       manufacturing_order_item.count    AS ManufacturingOrderItemCount,
       sweet.id                          AS SweetId,
       sweet.name                        AS SweetName,
       sweet.description                 AS SweetDescription,
       sweet.price                       AS SweetPrice,
       sweet_ingredient.count            AS SweetIngredientCount,
       ingredient.id                     AS IngredientId,
       ingredient.name                   AS IngredientName,
       ingredient.price                  AS IngredientPrice,
       measurement_unit.id               AS MeasurementUnitId,
       measurement_unit.name             AS MeasurementUnitName
FROM manufacturing_order
         JOIN manufacturing_order_status ON (manufacturing_order.status_id = manufacturing_order_status.id)
         JOIN factory_unit ON (manufacturing_order.factory_unit_id = factory_unit.id)
         JOIN factory ON (factory_unit.factory_id = factory.id)
         JOIN category ON (factory_unit.category_id = category.id)
         JOIN [dbo].[user] ON (factory_unit.controller_id = [dbo].[user].id)
         JOIN role ON ([dbo].[user].role_id = role.id)
         JOIN manufacturing_order_item ON (manufacturing_order.id = manufacturing_order_item.manufacturing_order_id)
         JOIN sweet ON (manufacturing_order_item.sweet_id = sweet.id)
         JOIN sweet_ingredient ON (sweet.id = sweet_ingredient.sweet_id)
         JOIN ingredient ON (sweet_ingredient.ingredient_id = ingredient.id)
         JOIN measurement_unit ON (ingredient.measurement_unit_id = measurement_unit.id)

GO
