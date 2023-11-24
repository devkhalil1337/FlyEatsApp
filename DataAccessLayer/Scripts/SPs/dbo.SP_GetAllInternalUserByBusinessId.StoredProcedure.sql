USE [Flyeats]
GO
/****** Object:  StoredProcedure [dbo].[SP_GetAllInternalUserByBusinessId]    Script Date: 11/25/2023 12:48:19 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_GetAllInternalUserByBusinessId]
    @BusinessId int
AS
BEGIN
    SELECT *
    FROM [dbo].[InternalUsers]
    WHERE [BusinessId] = @BusinessId
END


GO
