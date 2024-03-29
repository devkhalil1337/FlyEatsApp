USE [Flyeats]
GO
/****** Object:  StoredProcedure [dbo].[SP_GetGrossSales]    Script Date: 11/25/2023 12:48:19 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_GetGrossSales]
    @StartDate DATE,
    @EndDate DATE,
    @BusinessId INT
AS
BEGIN
    SELECT SUM(TotalAmount) AS GrossSales, CONVERT(VARCHAR(10), CreatedDate, 120) AS CreatedDate
    FROM Orders
    WHERE BusinessId = @BusinessId
        AND OrderStatus IN ('completed', 'Delivered')
        AND CreatedDate >= CONVERT(date, @StartDate) 
        AND CreatedDate <= CONVERT(date, @EndDate) 
    GROUP BY CONVERT(VARCHAR(10), CreatedDate, 120)
END


GO
