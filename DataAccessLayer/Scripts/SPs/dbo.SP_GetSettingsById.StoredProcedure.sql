USE [Flyeats]
GO
/****** Object:  StoredProcedure [dbo].[SP_GetSettingsById]    Script Date: 11/25/2023 12:48:19 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[SP_GetSettingsById]
(
	@BusinessId int

 
)
AS  
BEGIN  
   select * FROM Settings  where BusinessId = @BusinessId
END  

GO
