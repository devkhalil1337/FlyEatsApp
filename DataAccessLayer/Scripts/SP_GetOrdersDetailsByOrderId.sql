USE [Flyeats]
GO
/****** Object:  StoredProcedure [dbo].[SP_GetOrdersDetailsByOrderId]    Script Date: 2/11/2023 4:30:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [dbo].[SP_GetOrdersDetailsByOrderId]
(
	@OrderId  nvarchar(255)
)
AS  
BEGIN  

  select * from [OrderDetails] where OrderId=@OrderId;

END  