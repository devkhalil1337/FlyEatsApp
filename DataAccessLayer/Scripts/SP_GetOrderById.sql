USE [Flyeats]
GO
/****** Object:  StoredProcedure [dbo].[SP_GetProductDetailById]    Script Date: 2/9/2023 8:58:50 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

Create PROCEDURE [dbo].[SP_GetOrderById]
(
	@OrderInvoiceNumber nvarchar(255)
)
AS  
BEGIN  

   select * FROM [Flyeats].[dbo].[Order] where OrderInvoiceNumber = @OrderInvoiceNumber;
END  