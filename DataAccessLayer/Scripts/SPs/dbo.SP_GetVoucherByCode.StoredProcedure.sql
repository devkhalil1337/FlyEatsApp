USE [Flyeats]
GO
/****** Object:  StoredProcedure [dbo].[SP_GetVoucherByCode]    Script Date: 11/25/2023 12:48:19 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[SP_GetVoucherByCode]
    @VoucherCode NVARCHAR(150)
AS
BEGIN
    SET NOCOUNT ON;

    SELECT *
    FROM [dbo].[Voucher]
    WHERE VoucherCode = @VoucherCode;
END


GO
