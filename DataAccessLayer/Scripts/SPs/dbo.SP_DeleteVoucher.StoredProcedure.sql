USE [Flyeats]
GO
/****** Object:  StoredProcedure [dbo].[SP_DeleteVoucher]    Script Date: 11/25/2023 12:48:19 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_DeleteVoucher]
    @VoucherId int
AS
BEGIN
    DELETE FROM Voucher WHERE VoucherId = @VoucherId;
END;


GO
