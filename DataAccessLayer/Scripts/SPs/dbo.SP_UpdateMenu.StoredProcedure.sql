USE [Flyeats]
GO
/****** Object:  StoredProcedure [dbo].[SP_UpdateMenu]    Script Date: 11/25/2023 12:48:19 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_UpdateMenu]
    @Id int,
    @BusinessId bigint,
    @MenuName nvarchar(255),
    @MenuUrl nvarchar(255),
    @OrderBy int,
    @isActive bit
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE [dbo].[Menus]
    SET
        [BusinessId] = @BusinessId,
        [MenuName] = @MenuName,
        [MenuUrl] = @MenuUrl,
        [OrderBy] = @OrderBy,
        [isActive] = @isActive
    WHERE [Id] = @Id;

    SELECT 'Menu updated successfully.' AS 'Message';
END;


GO
