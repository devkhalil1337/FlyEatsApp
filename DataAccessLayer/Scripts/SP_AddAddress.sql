CREATE PROCEDURE SP_AddAddress (
  @UserId INT, 
  @AddressLine1 VARCHAR(255) = NULL, 
  @AddressLine2 VARCHAR(255) = NULL, 
  @City VARCHAR(255) = NULL, 
  @StateProvince VARCHAR(255) = NULL, 
  @ZipPostalCode VARCHAR(255) = NULL, 
  @Country VARCHAR(255) = NULL, 
  @Latitude DECIMAL(9, 6) = NULL, 
  @Longitude DECIMAL(9, 6) = NULL, 
  @PhoneNumber VARCHAR(255) = NULL, 
  @AddressType VARCHAR(255) = NULL,
  @Active bit = Null
) AS BEGIN 
SET 
  NOCOUNT ON;
INSERT INTO Addresses (
  UserId, AddressLine1, AddressLine2, 
  City, StateProvince, ZipPostalCode, 
  Country, Latitude, Longitude, PhoneNumber, 
  AddressType, Active
) 
VALUES 
  (
    @UserId, @AddressLine1, @AddressLine2, 
    @City, @StateProvince, @ZipPostalCode, 
    @Country, @Latitude, @Longitude, 
    @PhoneNumber, @AddressType,@Active
  );
   SELECT SCOPE_IDENTITY();
END
