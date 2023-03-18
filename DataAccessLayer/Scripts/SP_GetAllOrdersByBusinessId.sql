USE [Flyeats]
GO
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
        select * from [dbo].[Orders] where BusinessId=@BusinessId and CreatedDate >= @StartDate and CreatedDate <= DATEADD(day, 1, @EndDate);
    end
    else
    begin
        select * from [dbo].[Orders] where BusinessId=@BusinessId and CreatedDate >= @StartDate and CreatedDate <= DATEADD(day, 1, @EndDate) and OrderStatus=@OrderStatus;
    end

END
