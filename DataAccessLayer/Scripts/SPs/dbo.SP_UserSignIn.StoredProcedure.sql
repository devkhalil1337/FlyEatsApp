USE [Flyeats]
GO
/****** Object:  StoredProcedure [dbo].[SP_UserSignIn]    Script Date: 11/25/2023 12:48:19 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[SP_UserSignIn]
(
	@email nvarchar(60) ,
	@password nvarchar(500)
)
AS  
BEGIN  
   select * from InternalSignup where Email = @email and [Password] = @password
END 

GO
