CREATE TRIGGER [dbo].[UpdateManufacturingOrderStatusTrigger]
    ON [dbo].[manufacturing_order]
    FOR UPDATE
    AS

BEGIN
    DECLARE @SweetStorage TABLE
                          (
                              SweetId   BIGINT,
                              FactoryId BIGINT,
                              Count     BIGINT,
                              Processed INT
                          );

    INSERT INTO @SweetStorage
    SELECT moi.sweet_id,
           f.id,
           moi.count,
           0
    FROM inserted,
         manufacturing_order mo
             JOIN factory_unit fu on mo.factory_unit_id = fu.id
             JOIN manufacturing_order_item moi on mo.id = moi.manufacturing_order_id
             JOIN factory f on fu.factory_id = f.id
    WHERE mo.id = inserted.id
      AND mo.status_id = (
        SELECT id
        FROM manufacturing_order_status
        WHERE name = N'Виконано'
    );

    DECLARE @SweetId BIGINT;
    DECLARE @FactoryId BIGINT;
    DECLARE @Count BIGINT;

    WHILE (
              SELECT COUNT(*)
              FROM @SweetStorage
              WHERE Processed = 0
          ) > 0
        BEGIN
            SELECT TOP 1 @SweetId = SweetId,
                         @FactoryId = FactoryId,
                         @Count = Count
            FROM @SweetStorage
            WHERE Processed = 0


            IF EXISTS(
                    SELECT *
                    FROM sweet_storage ss
                    WHERE ss.sweet_id = @SweetId
                      AND ss.factory_id = @FactoryId
                )
                BEGIN
                    UPDATE sweet_storage
                    SET sweet_storage.count = sweet_storage.count + @Count
                    WHERE sweet_storage.sweet_id = @SweetId
                      AND sweet_storage.factory_id = @FactoryId;
                END
            ELSE
                BEGIN
                    INSERT INTO sweet_storage (sweet_id, factory_id, [count])
                    VALUES (@SweetId, @FactoryId, @Count);
                END


            UPDATE @SweetStorage
            SET Processed = 1
            WHERE SweetId = @SweetId
              AND FactoryId = @FactoryId
              AND Count = @Count

        END
END