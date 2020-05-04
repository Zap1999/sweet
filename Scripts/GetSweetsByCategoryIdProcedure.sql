USE [sweet_life]
GO

CREATE PROCEDURE GetSweetsByCategoryId @CategoryId AS BIGINT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT *
    FROM [dbo].[sweet]
    WHERE category_id = @CategoryId
END
GO
