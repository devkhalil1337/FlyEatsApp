USE [Flyeats]
GO
/****** Object:  StoredProcedure [dbo].[SP_InsertRedemption]    Script Date: 11/25/2023 12:48:19 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_InsertRedemption]
    @VoucherId INT,
    @UserId INT,
    @RedeemAmount DECIMAL(18,2),
    @RedeemCount INT
AS
BEGIN
    INSERT INTO VoucherRedemption (VoucherId, UserId, RedeemAmount, RedeemCount)
    VALUES (@VoucherId, @UserId, @RedeemAmount, @RedeemCount)
END


GO
