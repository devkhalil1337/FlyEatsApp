USE [Flyeats]
GO
/****** Object:  StoredProcedure [dbo].[SP_GetCustomerByEmail]    Script Date: 11/25/2023 12:48:19 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_GetCustomerByEmail]
(
@Email VARCHAR(255),
@BusinessId int

)
AS
BEGIN
SET NOCOUNT ON;

SELECT [UserId]
  ,[FirstName]
  ,[LastName]
  ,[Email]
  ,[PhoneNumber]
  ,[BusinessId]
  ,[PasswordHash]
  ,[Salt]
  ,[isGuest]
  ,[CreatedAt]
  ,[UpdatedAt]
FROM [dbo].[Users]
WHERE [Email] = @Email and BusinessId = @BusinessId
END

GO
