USE sweet_life
GO

CREATE PROCEDURE SaveFactoryUnit @ControllerId BIGINT,
                                 @CategoryId BIGINT,
                                 @FactoryId BIGINT
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO [dbo].[factory_unit] ([controller_id], [category_id], [factory_id])
    VALUES (@ControllerId, @CategoryId, @FactoryId);
END
GO
