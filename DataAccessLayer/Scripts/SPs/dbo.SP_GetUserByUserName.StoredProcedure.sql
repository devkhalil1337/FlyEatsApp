USE [Flyeats]
GO
/****** Object:  StoredProcedure [dbo].[SP_GetUserByUserName]    Script Date: 11/25/2023 12:48:19 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[SP_GetUserByUserName]
(
	@Username nvarchar(50)
)
AS  
BEGIN  
 
   select * from InternalUsers where Username = @Username;
END  

GO
