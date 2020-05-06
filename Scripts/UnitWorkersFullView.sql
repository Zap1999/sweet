USE sweet_life
GO

CREATE VIEW UnitWorkersFull AS
SELECT [user].id                  AS UserId,
       [user].first_name          AS UserFirstName,
       [user].last_name           AS UserLastName,
       [user].email               AS UserEmail,
       [user].[password]          AS UserPassword,
       [role].id                  AS RoleId,
       [role].[name]              AS RoleName,
       [role].salary              AS RoleSalary,
       factory_unit.id            AS FactoryUnitId,
       factory_unit.controller_id AS ControllerId,
       factory.id                 AS FactoryId,
       factory.[address]          AS FactoryAddress,
       category.id                AS CategoryId,
       category.[name]            AS CategoryName
FROM unit_worker
         JOIN factory_unit ON unit_worker.unit_id = factory_unit.id
         JOIN factory ON factory_unit.factory_id = factory.id
         JOIN category ON factory_unit.category_id = category.id
         JOIN [user] ON unit_worker.worker_id = [user].id
         JOIN [role] ON [user].role_id = [role].id
