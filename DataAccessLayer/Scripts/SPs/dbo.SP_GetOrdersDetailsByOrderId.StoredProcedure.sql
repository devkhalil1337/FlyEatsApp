USE [Flyeats]
GO
/****** Object:  StoredProcedure [dbo].[SP_GetOrdersDetailsByOrderId]    Script Date: 11/25/2023 12:48:19 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[SP_GetOrdersDetailsByOrderId]
(
	@OrderId  nvarchar(255)
)
AS  
BEGIN  

  select * from [OrderDetails] where OrderId=@OrderId;

END  

GO
