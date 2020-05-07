USE [sweet_life]
GO

CREATE PROCEDURE DeleteManufacturingOrderItem @ManufacturingOrderId AS BIGINT,
                                              @SweetId AS BIGINT
AS
BEGIN
    SET NOCOUNT ON;

    DELETE
    FROM [dbo].[manufacturing_order_item]
    WHERE manufacturing_order_id = @ManufacturingOrderId
      AND sweet_id = @SweetId
END
GO
 