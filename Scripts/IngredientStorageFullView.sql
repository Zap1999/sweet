USE sweet_life
GO

CREATE VIEW IngredientStorageFull AS
    
SELECT i.id       AS iId,
       i.name     AS iName,
       i.price    AS iPrice,
       mu.id      AS muId,
       mu.name    AS muName,
       [is].count AS isCount,
       f.id       AS fId,
       f.address  AS fAddress

FROM ingredient_storage [is]
         INNER JOIN ingredient i on [is].ingredient_id = i.id
         INNER JOIN measurement_unit mu on i.measurement_unit_id = mu.id
         INNER JOIN factory f on [is].factory_id = f.id

GO


