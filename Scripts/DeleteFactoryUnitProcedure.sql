USE sweet_life
GO

CREATE PROCEDURE DeleteFactoryUnit @UnitId BIGINT
AS
BEGIN
    SET NOCOUNT ON;

    DELETE
    FROM [dbo].[factory_unit]
    WHERE id = @UnitId
END
GO
