USE sweet_life
GO

CREATE PROCEDURE SaveSweet @Name NVARCHAR(100),
                           @Description NVARCHAR(500),
                           @Price NUMERIC(18, 2),
                           @CategoryId BIGINT
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO [dbo].[sweet] ([name], [description], [price], [category_id])
    VALUES (@Name, @Description, @Price, @CategoryId);
END
GO
