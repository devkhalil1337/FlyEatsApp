USE [Flyeats]
GO
/****** Object:  StoredProcedure [dbo].[SP_CreateVoucher]    Script Date: 11/25/2023 12:48:19 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_CreateVoucher]
    @VoucherCode nvarchar(50),
    @MinValue decimal(18, 2),
    @MaxValue decimal(18, 2),
    @StartDate datetime,
    @EndDate datetime,
    @CreatedBy int,
    @BusinessId int,
    @IsActive bit,
	@RedeemCount int
AS
BEGIN
    INSERT INTO Voucher (VoucherCode, MinValue, MaxValue, StartDate, EndDate, CreatedOn, CreatedBy, BusinessId, IsActive,RedeemCount)
    VALUES (@VoucherCode, @MinValue, @MaxValue, @StartDate, @EndDate, GETDATE(), @CreatedBy, @BusinessId, @IsActive,@RedeemCount);
    SELECT SCOPE_IDENTITY() AS VoucherId;
END;


GO
