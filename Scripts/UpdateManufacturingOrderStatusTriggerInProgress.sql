CREATE TRIGGER [dbo].[UpdateManufacturingOrderStatusTriggerInProgress]
    ON [dbo].[manufacturing_order]
    FOR UPDATE
    AS

BEGIN

    UPDATE sweet_storage

    SET sweet_storage.count = sweet_storage.count - moi.count

    FROM inserted
             JOIN manufacturing_order mo ON mo.id = inserted.ID
             JOIN factory_unit fu on mo.factory_unit_id = fu.id
             JOIN manufacturing_order_item moi on mo.id = moi.manufacturing_order_id
             JOIN factory f on fu.factory_id = f.id

    WHERE sweet_storage.sweet_id = moi.sweet_id
      AND sweet_storage.factory_id = f.id
      AND mo.status_id = (
        SELECT id
        FROM manufacturing_order_status
        WHERE name = N'В процесі'
    )

END