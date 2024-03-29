USE [Flyeats]
GO
/****** Object:  StoredProcedure [dbo].[SP_GetOrdersByTypeAndDateRange]    Script Date: 11/25/2023 12:48:19 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_GetOrdersByTypeAndDateRange]
	@BusinessId int,
    @FromDate datetime,
    @ToDate datetime,
    @OrderStatus nvarchar(20)
AS
BEGIN
    SELECT COUNT(*) AS NumberOfOrders
    FROM [Flyeats].[dbo].[Orders]
    WHERE [OrderStatus] = @OrderStatus
        AND [BusinessId] = @BusinessId
        AND [CreatedDate] BETWEEN CONVERT(DATE, @FromDate) AND CONVERT(DATE, @ToDate)
END
GO
