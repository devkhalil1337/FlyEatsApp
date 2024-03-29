USE [Flyeats]
GO
/****** Object:  StoredProcedure [dbo].[sp_GetInternalUserByCredentials]    Script Date: 11/25/2023 12:48:19 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Alter PROCEDURE for retrieving a user by username and password
CREATE PROCEDURE [dbo].[sp_GetInternalUserByCredentials]
    @Username NVARCHAR(50),
    @Password NVARCHAR(500)
AS
BEGIN
    SET NOCOUNT ON;

    SELECT *
    FROM [dbo].[InternalUsers]
    WHERE [Username] = @Username
        AND [Password] = @Password
        AND [IsDeleted] = 0
        AND [Active] = 1
END

GO
