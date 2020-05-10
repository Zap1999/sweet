USE sweet_life
GO

CREATE VIEW SweetExpanseData AS

SELECT mo.creation_date AS moCreationDate,
       mo.status_id     AS moStatusId,
       moi.count        AS moiCount,
       s.id             AS sId,
       s.name           AS sName,
       s.price          AS sPrice,
       s.description    AS sDescription,
       c.id             AS cId,
       c.name           AS cName,
       f.id             AS fId,
       f.address        AS fAddress

FROM manufacturing_order mo
         JOIN manufacturing_order_status mos ON mo.status_id = mos.id
         JOIN manufacturing_order_item moi ON mo.id = moi.manufacturing_order_id
         JOIN sweet s ON moi.sweet_id = s.id
         JOIN factory_unit fu on mo.factory_unit_id = fu.id
         JOIN factory f on fu.factory_id = f.id
         JOIN category c on fu.category_id = c.id
