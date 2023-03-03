ALTER PROCEDURE SP_GetGrossSales
    @StartDate DATE,
    @EndDate DATE,
    @BusinessId INT
AS
BEGIN
    SELECT SUM(TotalAmount) AS GrossSales, CONVERT(VARCHAR(10), CreatedDate, 120) AS CreatedDate
    FROM Orders
    WHERE BusinessId = @BusinessId
        AND OrderStatus IN ('completed', 'Delivered')
        AND CreatedDate >= @StartDate
        AND CreatedDate <= @EndDate
    GROUP BY CONVERT(VARCHAR(10), CreatedDate, 120)
END
