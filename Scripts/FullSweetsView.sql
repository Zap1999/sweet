CREATE VIEW FullSweets AS

SELECT s.id          AS sId,
       s.name        AS sName,
       s.description AS sDescription,
       s.price       AS sPrice,
       c.id          AS cId,
       c.name        AS cName,
       si.count      AS siCount,
       i.id          AS iId,
       i.name        AS iName,
       i.price       AS iPrice,
       mu.id         AS muId,
       mu.name       AS muName

FROM sweet AS s
         INNER JOIN category c ON s.category_id = c.id
         INNER JOIN sweet_ingredient si ON s.id = si.sweet_id
         INNER JOIN ingredient i ON si.ingredient_id = i.id
         INNER JOIN measurement_unit mu ON i.measurement_unit_id = mu.id;