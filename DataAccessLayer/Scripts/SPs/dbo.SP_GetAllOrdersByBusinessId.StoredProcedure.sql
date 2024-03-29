USE [Flyeats]
GO
/****** Object:  StoredProcedure [dbo].[SP_GetAllOrdersByBusinessId]    Script Date: 12/9/2023 4:40:59 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [dbo].[SP_GetAllOrdersByBusinessId]
(
    @BusinessId int,
    @StartDate datetime,
    @EndDate datetime,
    @OrderStatus varchar(50) = NULL
)
AS
BEGIN

    if (@OrderStatus IS NULL OR @OrderStatus = '')
    begin
        select * from [dbo].[Orders] where BusinessId=@BusinessId and CreatedDate >= @StartDate and CreatedDate <= DATEADD(day, 1, @EndDate)  order by OrderId DESC;
    end
    else
    begin
        select * from [dbo].[Orders] where BusinessId=@BusinessId and CreatedDate >= @StartDate and CreatedDate <= DATEADD(day, 1, @EndDate) and OrderStatus=@OrderStatus order by OrderId DESC;
    end

END

