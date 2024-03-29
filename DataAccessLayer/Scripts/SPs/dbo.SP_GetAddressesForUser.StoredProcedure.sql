USE [Flyeats]
GO
/****** Object:  StoredProcedure [dbo].[SP_GetAddressesForUser]    Script Date: 11/25/2023 12:48:19 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_GetAddressesForUser] (@UserId INT)
AS
BEGIN
    SET NOCOUNT ON;

    SELECT addressId, userId, addressLine1, addressLine2, city, stateProvince, zipPostalCode, country, latitude, longitude, phoneNumber, addressType, active, timestamp
    FROM Addresses
    WHERE UserId = @UserId;
END


GO
