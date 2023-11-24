USE [Flyeats]
GO
/****** Object:  StoredProcedure [dbo].[SP_GetOrderSelectionsById]    Script Date: 11/25/2023 12:48:19 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[SP_GetOrderSelectionsById]
(
	@OrderDetailsId bigint
)
AS  
BEGIN  

   select * FROM [Flyeats].[dbo].[OrderDetailSelectionRelation] where OrderDetailsId = @OrderDetailsId;
END  

GO
