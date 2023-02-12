USE [Flyeats]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

Create PROCEDURE [dbo].[SP_GetOrdersByCustomerId]
(
	@CustomerId INT
)
AS  
BEGIN  

   select * FROM [Flyeats].[dbo].[Order] where CustomerId = @CustomerId;
END  