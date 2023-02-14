USE [Flyeats]
GO
/****** Object:  StoredProcedure [dbo].[SP_GetOrdersByCustomerId]    Script Date: 2/15/2023 1:43:46 AM ******/
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

   select * FROM [Flyeats].[dbo].[Order] where CustomerId = @CustomerId ORDER BY OrderId DESC;;
END  