USE [Flyeats]
GO
/****** Object:  StoredProcedure [dbo].[SP_GetAllOrdersByBusinessId]    Script Date: 11/25/2023 12:48:19 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[SP_GetAllOrdersByBusinessId]
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


GO
