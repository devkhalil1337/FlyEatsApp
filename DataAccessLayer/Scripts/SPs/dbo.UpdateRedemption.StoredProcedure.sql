USE [Flyeats]
GO
/****** Object:  StoredProcedure [dbo].[UpdateRedemption]    Script Date: 11/25/2023 12:48:19 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[UpdateRedemption]
    @RedeemId INT,
    @VoucherId INT,
    @UserId INT,
    @RedeemAmount DECIMAL(18,2),
    @RedeemCount INT
AS
BEGIN
    UPDATE VoucherRedemption
    SET VoucherId = @VoucherId, UserId = @UserId, RedeemAmount = @RedeemAmount, RedeemCount = @RedeemCount
    WHERE RedeemId = @RedeemId
END


GO
