USE sweet_life
GO

CREATE PROCEDURE DeleteUnitWorker @UnitId BIGINT,
                                  @UserId BIGINT
AS
BEGIN
    SET NOCOUNT ON;

    DELETE
    FROM [dbo].[unit_worker]
    WHERE unit_id = @UnitId
      AND worker_id = @UserId
END
GO
