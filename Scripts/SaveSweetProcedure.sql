USE sweet_life
GO

CREATE TYPE dbo.SweetIngredients
AS TABLE
(
    ingredient_id BIGINT,
    count    NUMERIC(18,3)
);
GO

CREATE PROCEDURE SaveSweet @Name NVARCHAR(100),
                           @Description NVARCHAR(500),
                           @Price NUMERIC(18, 2),
                           @CategoryId BIGINT,
                           @SweetIngredientsList SweetIngredients READONLY
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @SweetId TABLE
                                  (
                                      ID BIGINT
                                  );
    
    INSERT INTO [dbo].[sweet] ([name], [description], [price], [category_id])
    OUTPUT INSERTED.ID INTO @SweetId
    VALUES (@Name, @Description, @Price, @CategoryId);

    INSERT INTO [dbo].sweet_ingredient ([sweet_id], [ingredient_id], [count])
    SELECT ID, ingredient_id, count
    FROM @SweetId, @SweetIngredientsList;
END
