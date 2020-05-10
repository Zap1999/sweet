USE sweet_life
GO

CREATE VIEW UsersExpanseData AS

SELECT u.id         AS uId,
       u.first_name AS uFirstName,
       u.last_name  AS uLastName,
       u.email      AS uEmail,
       u.password   AS uPassword,
       r.id         AS rId,
       r.name       AS rName,
       r.salary     AS rSalary

FROM [user] u
         JOIN role r on u.role_id = r.id