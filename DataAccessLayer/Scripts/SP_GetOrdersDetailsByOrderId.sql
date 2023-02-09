USE [Flyeats]
GO
/****** Object:  StoredProcedure [dbo].[SP_GetAllOrdersByBusinessId]    Script Date: 2/9/2023 10:05:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

Create PROCEDURE [dbo].[SP_GetOrdersDetailsByOrderId]
(
	@OrderId int
)
AS  
BEGIN  

  select * from [OrderDetails] where OrderId=@OrderId;

END  