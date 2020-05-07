USE [sweet_life]
GO

CREATE PROCEDURE DeleteSweet @SweetId AS BIGINT
AS
BEGIN
    SET NOCOUNT ON;

    DELETE
    FROM [dbo].[sweet]
    WHERE sweet.id = @SweetId
END
GO
 