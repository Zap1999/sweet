USE sweet_life
GO

CREATE PROCEDURE DeleteSweetStorageRecord @FactoryId BIGINT,
                                          @SweetId BIGINT
AS
BEGIN
    SET NOCOUNT ON;

    DELETE
    FROM [dbo].[sweet_storage]
    WHERE sweet_storage.factory_id = @FactoryId
      AND sweet_storage.sweet_id = @SweetId
END
GO
