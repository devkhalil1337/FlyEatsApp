USE [Flyeats]
GO
/****** Object:  StoredProcedure [dbo].[SP_GetVoucherByCode]    Script Date: 2/22/2023 11:03:59 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


ALTER PROCEDURE [dbo].[SP_GetVoucherByCode]
    @VoucherCode NVARCHAR(150)
AS
BEGIN
    SET NOCOUNT ON;

    SELECT *
    FROM [dbo].[Voucher]
    WHERE VoucherCode = @VoucherCode;
END
