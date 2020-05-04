USE [sweet_life]
GO

CREATE PROCEDURE UpdateSweetStorage @SweetId BIGINT,
                                    @FactoryID BIGINT,
                                    @Count BIGINT
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE [dbo].[sweet_storage]
    SET count = @Count
    WHERE sweet_id = @SweetId AND factory_id = @FactoryID;
END
GO
