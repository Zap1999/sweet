USE [sweet_life]
GO

CREATE PROCEDURE DeleteManufacturingOrder @ManufacturingOrderId AS BIGINT
AS
BEGIN
    SET NOCOUNT ON;

    DELETE
    FROM [dbo].[manufacturing_order]
    WHERE manufacturing_order.id = @ManufacturingOrderId
END
GO
 