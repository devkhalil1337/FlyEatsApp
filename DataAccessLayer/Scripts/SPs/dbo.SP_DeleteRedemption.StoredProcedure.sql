USE [Flyeats]
GO
/****** Object:  StoredProcedure [dbo].[SP_DeleteRedemption]    Script Date: 11/25/2023 12:48:19 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_DeleteRedemption]
    @RedeemId INT
AS
BEGIN
    DELETE FROM VoucherRedemption WHERE RedeemId = @RedeemId
END


GO
