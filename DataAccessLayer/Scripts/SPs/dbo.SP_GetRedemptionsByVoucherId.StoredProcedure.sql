USE [Flyeats]
GO
/****** Object:  StoredProcedure [dbo].[SP_GetRedemptionsByVoucherId]    Script Date: 11/25/2023 12:48:19 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_GetRedemptionsByVoucherId]
    @VoucherId INT
AS
BEGIN
    SELECT RedeemId, VoucherId, UserId, RedeemAmount, RedeemCount
    FROM VoucherRedemption
    WHERE VoucherId = @VoucherId
END


GO
