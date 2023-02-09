USE [Flyeats]
GO
/****** Object:  StoredProcedure [dbo].[SP_GetAllProductsByBusinessId]    Script Date: 2/9/2023 4:34:37 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

Create PROCEDURE [dbo].[SP_GetAllOrdersByBusinessId]
(
	@BusinessId int
)
AS  
BEGIN  

  select * from [Order] where BusinessId=@BusinessId and IsDeleted=0;

END  