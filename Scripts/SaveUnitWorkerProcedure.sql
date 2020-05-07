CREATE PROCEDURE SaveUnitWorker @WorkerId BIGINT,
                                @UnitId BIGINT
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO [dbo].[unit_worker] (worker_id, unit_id)
    VALUES (@WorkerId, @UnitId);
END
GO
