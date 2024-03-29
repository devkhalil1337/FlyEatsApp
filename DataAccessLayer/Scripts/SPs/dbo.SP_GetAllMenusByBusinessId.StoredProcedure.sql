USE [Flyeats]
GO
/****** Object:  StoredProcedure [dbo].[SP_GetAllMenusByBusinessId]    Script Date: 11/25/2023 12:48:19 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_GetAllMenusByBusinessId]
    @BusinessId bigint
AS
BEGIN
    SET NOCOUNT ON;

    SELECT [Id], [BusinessId], [MenuName], [MenuUrl], [OrderBy], [isActive]
    FROM [dbo].[Menus]
    WHERE [BusinessId] = @BusinessId;
END;


GO
