CREATE TRIGGER [dbo].[UpdateManufacturingOrderStatusTriggerInProgress]
    ON [dbo].[manufacturing_order]
    FOR UPDATE
    AS

BEGIN

    UPDATE ingredient_storage

    SET ingredient_storage.count = ingredient_storage.count - si.count * moi.count

    FROM inserted,
         manufacturing_order mo
             JOIN factory_unit fu on mo.factory_unit_id = fu.id
             JOIN manufacturing_order_item moi on mo.id = moi.manufacturing_order_id
             JOIN factory f on fu.factory_id = f.id
             JOIN sweet s on moi.sweet_id = s.id
             JOIN sweet_ingredient si on s.id = si.sweet_id

    WHERE mo.id = inserted.ID
      AND ingredient_storage.ingredient_id = si.ingredient_id
      AND ingredient_storage.factory_id = f.id
      AND mo.status_id = (
        SELECT id
        FROM manufacturing_order_status
        WHERE name = N'В процесі'
    )

END