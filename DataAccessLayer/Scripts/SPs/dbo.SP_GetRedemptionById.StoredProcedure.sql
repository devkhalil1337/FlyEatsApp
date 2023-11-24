USE [Flyeats]
GO
/****** Object:  StoredProcedure [dbo].[SP_GetRedemptionById]    Script Date: 11/25/2023 12:48:19 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_GetRedemptionById]
    @RedeemId INT
AS
BEGIN
    SELECT RedeemId, VoucherId, UserId, RedeemAmount, RedeemCount
    FROM VoucherRedemption
    WHERE RedeemId = @RedeemId
END


GO
