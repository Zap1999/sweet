USE sweet_life
GO

CREATE PROCEDURE GetAllUsersForPeriod @PeriodStart DATE,
                                      @PeriodEnd DATE
AS
BEGIN

    SET NOCOUNT ON;

    SELECT uId,
           uFirstName,
           uLastName,
           uEmail,
           uPassword,
           rId,
           rName,
           rSalary,
           DATEDIFF(DAY, @PeriodStart, @PeriodEnd) * (rSalary / 30.42) AS totalSalary

    FROM UsersExpanseData

END
