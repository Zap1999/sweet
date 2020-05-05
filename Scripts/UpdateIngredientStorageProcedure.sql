USE [sweet_life]
GO

CREATE PROCEDURE UpdateIngredientStorage @IngredientId BIGINT,
                                         @FactoryID BIGINT,
                                         @Count NUMERIC(18, 3)
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE [dbo].[ingredient_storage]
    SET count = @Count
    WHERE ingredient_id = @IngredientId
      AND factory_id = @FactoryID;
END
GO
