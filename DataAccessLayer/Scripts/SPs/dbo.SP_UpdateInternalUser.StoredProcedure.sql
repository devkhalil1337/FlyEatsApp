USE [Flyeats]
GO
/****** Object:  StoredProcedure [dbo].[SP_UpdateInternalUser]    Script Date: 11/25/2023 12:48:19 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[SP_UpdateInternalUser]
(
    @Id int,
	@BusinessId int,
    @FullName nvarchar(50),
    @Username nvarchar(50),
    @Email nvarchar(60),
    @Password nvarchar(500),
    @MobileNumber nvarchar(20),
    @AccountType int,
    @Role int,
	@CreationDate datetime2(7),
    @UpdateDate datetime2(7),
    @IsDeleted bit,
    @Active bit
)
AS  
BEGIN  
   UPDATE [dbo].[InternalUsers]
   SET
		[BusinessId] = @BusinessId,	
       [FullName] = @FullName,
       [Username] = @Username,
       [Email] = @Email,
       [Password] = @Password,
       [MobileNumber] = @MobileNumber,
       [AccountType] = @AccountType,
       [Role] = @Role,
	   [CreationDate] = @CreationDate,
       [UpdateDate] = @UpdateDate,
       [IsDeleted] = @IsDeleted,
       [Active] = @Active
   WHERE [Id] = @Id;
END  

GO
