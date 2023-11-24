USE [Flyeats]
GO
/****** Object:  StoredProcedure [dbo].[SP_GetOrderById]    Script Date: 11/25/2023 12:48:19 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[SP_GetOrderById]
(
	@OrderInvoiceNumber nvarchar(255)
)
AS  
BEGIN  

   select * FROM [Flyeats].[dbo].[Orders] where OrderInvoiceNumber = @OrderInvoiceNumber;
END  

GO
