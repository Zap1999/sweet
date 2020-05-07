USE sweet_life
GO

CREATE PROCEDURE DeleteCategory @CategoryId NVARCHAR(100)
AS
BEGIN
    SET NOCOUNT ON;

    DELETE
    FROM [dbo].[category]
    WHERE category.id = @CategoryId
END
GO
