CREATE PROCEDURE SP_GetAddressesForUser (@UserId INT)
AS
BEGIN
    SET NOCOUNT ON;

    SELECT addressId, userId, addressLine1, addressLine2, city, stateProvince, zipPostalCode, country, latitude, longitude, phoneNumber, addressType, active, timestamp
    FROM Addresses
    WHERE UserId = @UserId;
END
