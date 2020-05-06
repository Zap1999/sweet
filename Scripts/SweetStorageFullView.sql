USE sweet_life
GO

CREATE VIEW SweetStorageFull AS
SELECT sweet.id            AS SweetId,
       sweet.name          AS SweetName,
       sweet.description   AS SweetDescription,
       sweet.price         AS SweetPrice,
       category.id         AS CategoryId,
       category.name       AS CategoryName,
       sweet_storage.count AS SweetCount,
       factory.id          AS FactoryId,
       factory.address     AS FactoryAddress
FROM sweet_storage
         JOIN sweet ON sweet_storage.sweet_id = sweet.id
         JOIN category ON sweet.category_id = category.id
         JOIN factory ON sweet_storage.factory_id = factory.id

GO


