USE [Flyeats]
GO
/****** Object:  StoredProcedure [dbo].[SP_DeleteInternalUser]    Script Date: 11/25/2023 12:48:19 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[SP_DeleteInternalUser]
(
			@Id int
)
AS  
BEGIN  

   delete from InternalUsers where Id = @Id 
END  

GO
