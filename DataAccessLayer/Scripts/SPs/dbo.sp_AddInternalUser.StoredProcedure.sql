USE [Flyeats]
GO
/****** Object:  StoredProcedure [dbo].[sp_AddInternalUser]    Script Date: 11/25/2023 12:48:19 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Alter PROCEDURE for adding a new user
CREATE PROCEDURE [dbo].[sp_AddInternalUser]
    @BusinessId INT,
    @FullName NVARCHAR(50),
    @Username NVARCHAR(50),
    @Email NVARCHAR(60),
    @Password NVARCHAR(500),
    @MobileNumber NVARCHAR(20),
    @AccountType INT,
    @Role INT,
    @CreationDate DATETIME2(7),
    @UpdateDate DATETIME2(7),
    @IsDeleted BIT,
    @Active BIT
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO [dbo].[InternalUsers] (
        [BusinessId],
        [FullName],
        [Username],
        [Email],
        [Password],
        [MobileNumber],
        [AccountType],
        [Role],
        [CreationDate],
        [UpdateDate],
        [IsDeleted],
        [Active]
    ) VALUES (
        @BusinessId,
        @FullName,
        @Username,
        @Email,
        @Password,
        @MobileNumber,
        @AccountType,
        @Role,
        @CreationDate,
        @UpdateDate,
        @IsDeleted,
        @Active
    )
	 SELECT SCOPE_IDENTITY();
END

GO
