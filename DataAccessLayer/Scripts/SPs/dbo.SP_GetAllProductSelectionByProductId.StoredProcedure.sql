USE [Flyeats]
GO
/****** Object:  StoredProcedure [dbo].[SP_GetAllProductSelectionByProductId]    Script Date: 11/25/2023 12:48:19 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[SP_GetAllProductSelectionByProductId]
(
	@ProductId int
)
AS  
BEGIN  

   select * from ProductSelection where ProductId=@ProductId;
END  

GO
