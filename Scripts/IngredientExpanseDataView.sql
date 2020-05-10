USE sweet_life
GO

CREATE VIEW IngredientsExpanseData AS

SELECT mo.creation_date AS moCreationDate,
       mo.status_id     AS moStatusId,
       moi.count        AS moiCount,
       i.id             AS iId,
       i.name           AS iName,
       i.price          AS iPrice,
       mu.id            AS muId,
       mu.name          AS muName,
       f.id             AS fId,
       f.address        AS fAddress


FROM manufacturing_order mo
         JOIN manufacturing_order_status mos ON mo.status_id = mos.id
         JOIN manufacturing_order_item moi ON mo.id = moi.manufacturing_order_id
         JOIN sweet s ON moi.sweet_id = s.id
         JOIN sweet_ingredient si ON s.id = si.sweet_id
         JOIN ingredient i ON si.ingredient_id = i.id
         JOIN measurement_unit mu on i.measurement_unit_id = mu.id
         JOIN factory_unit fu on mo.factory_unit_id = fu.id
         JOIN factory f on fu.factory_id = f.id