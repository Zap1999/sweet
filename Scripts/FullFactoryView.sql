CREATE VIEW FullFactory AS

SELECT f.id         AS fId,
       f.address    AS fAddress,
       fu.id        AS fuId,
       c.id         AS cId,
       c.name       AS cName,
       u.id         AS uId,
       u.first_name AS uFirstName,
       u.last_name  AS uLastName,
       u.email      AS uEmail,
       u.password   AS uPassword,
       u.role_id    AS uRoleId

FROM factory f
         INNER JOIN factory_unit fu ON f.id = fu.factory_id
         INNER JOIN category c ON fu.category_id = c.id
         INNER JOIN [user] u ON fu.controller_id = u.id