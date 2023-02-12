CREATE PROCEDURE SP_GetAddressByAddressId (@AddressId INT)
AS
BEGIN
  SELECT *
  FROM Addresses
  WHERE AddressId = @AddressId AND Active = 1
END
