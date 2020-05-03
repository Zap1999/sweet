USE [sweet_life]
GO

CREATE TYPE dbo.ManufacturingOrderList
AS TABLE
(
    sweet_id BIGINT,
    count BIGINT
);
GO

CREATE PROCEDURE SaveManufacturingOrder @DeadLineDate DATE,
                                        @FactoryUnitId BIGINT,
                                        @StatusId BIGINT,
                                        @ManufacturingOrderItems AS dbo.ManufacturingOrderList READONLY
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @ManufacturingOrderId TABLE (ID BIGINT);

    INSERT INTO [dbo].[manufacturing_order] ([creation_date], [status_id], [factory_unit_id], [deadline_date])
    OUTPUT INSERTED.ID INTO @ManufacturingOrderId
    VALUES (CAST(GETDATE() AS DATE), @StatusId, @FactoryUnitId, @DeadLineDate);

    INSERT INTO [dbo].manufacturing_order_item ([manufacturing_order_id], [sweet_id], [count])
    SELECT ID, sweet_id, count FROM @ManufacturingOrderId, @ManufacturingOrderItems;
END
GO
