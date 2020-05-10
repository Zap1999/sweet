USE sweet_life
GO

CREATE PROCEDURE GetAllIngredientsForFactoryAndPeriod @FactoryId BIGINT,
                                                      @PeriodStart DATE,
                                                      @PeriodEnd DATE
AS
BEGIN

    SET NOCOUNT ON;

    SELECT iId, iName, iPrice, muId, muName, SUM(moiCount) iCount

    FROM IngredientsExpanseData

    WHERE moStatusId =
          (SELECT id FROM manufacturing_order_status WHERE manufacturing_order_status.name = N'Виконано')
      AND moCreationDate BETWEEN @PeriodStart AND @PeriodEnd
      AND fId = @FactoryId

    GROUP BY iId, iName, iPrice, muId, muName

END