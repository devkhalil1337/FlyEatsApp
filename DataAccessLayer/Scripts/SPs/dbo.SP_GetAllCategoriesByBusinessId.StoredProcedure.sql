USE [Flyeats]
GO
/****** Object:  StoredProcedure [dbo].[SP_GetAllCategoriesByBusinessId]    Script Date: 11/25/2023 12:48:19 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[SP_GetAllCategoriesByBusinessId]
(
	@BusinessId int
)
AS  
BEGIN   
   select * from Categories where BusinessId=@BusinessId;
END  

GO
