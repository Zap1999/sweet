USE sweet_life
GO

CREATE PROCEDURE GetAllSweetsForFactoryAndPeriod @FactoryId BIGINT,
                                                 @PeriodStart DATE,
                                                 @PeriodEnd DATE
AS
BEGIN

    SET NOCOUNT ON;

    SELECT sId, sName, sPrice, sDescription, cId, cName, SUM(moiCount) iCount

    FROM SweetExpanseData

    WHERE moStatusId =
          (SELECT id FROM manufacturing_order_status WHERE manufacturing_order_status.name = N'Виконано')
      AND moCreationDate BETWEEN @PeriodStart AND @PeriodEnd
      AND fId = @FactoryId

    GROUP BY sId, sName, sPrice, sDescription, cId, cName

END