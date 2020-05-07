USE sweet_life
GO

CREATE PROCEDURE SaveCategory @CategoryName NVARCHAR(100)
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO [dbo].[category] (name)
    VALUES (@CategoryName);
END
GO
