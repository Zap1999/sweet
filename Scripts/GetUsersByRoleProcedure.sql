USE [sweet_life]
GO

CREATE PROCEDURE GetUsersByRoleId @RoleId AS BIGINT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT id, first_name, last_name, email, role_id
    FROM [dbo].[user]
    WHERE role_id = @RoleId
END
GO