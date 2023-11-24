USE [Flyeats]
GO
/****** Object:  StoredProcedure [dbo].[SP_GetOrdersByCustomerId]    Script Date: 11/25/2023 12:48:19 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[SP_GetOrdersByCustomerId]
(
	@CustomerId INT
)
AS  
BEGIN  

   select * FROM [Flyeats].[dbo].[Orders] where CustomerId = @CustomerId ORDER BY OrderId DESC;;
END  

GO
