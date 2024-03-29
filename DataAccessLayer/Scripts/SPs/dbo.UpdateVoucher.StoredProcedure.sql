USE [Flyeats]
GO
/****** Object:  StoredProcedure [dbo].[UpdateVoucher]    Script Date: 11/25/2023 12:48:19 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[UpdateVoucher]
    @VoucherId INT,
    @VoucherCode NVARCHAR(50),
    @MinValue DECIMAL(18,2),
    @MaxValue DECIMAL(18,2),
    @StartDate DATETIME,
    @EndDate DATETIME,
    @IsActive BIT,
    @RedeemCount INT
AS
BEGIN
    UPDATE Voucher SET 
        VoucherCode = @VoucherCode,
        MinValue = @MinValue,
        MaxValue = @MaxValue,
        StartDate = @StartDate,
        EndDate = @EndDate,
        IsActive = @IsActive,
        RedeemCount = @RedeemCount
    WHERE VoucherId = @VoucherId
END


GO
