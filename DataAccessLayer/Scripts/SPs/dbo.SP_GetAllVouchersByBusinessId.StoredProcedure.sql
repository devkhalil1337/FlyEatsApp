USE [Flyeats]
GO
/****** Object:  StoredProcedure [dbo].[SP_GetAllVouchersByBusinessId]    Script Date: 11/25/2023 12:48:19 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_GetAllVouchersByBusinessId]
    @BusinessId INT
AS
BEGIN
    SELECT * FROM Voucher WHERE BusinessId = @BusinessId
END


GO
