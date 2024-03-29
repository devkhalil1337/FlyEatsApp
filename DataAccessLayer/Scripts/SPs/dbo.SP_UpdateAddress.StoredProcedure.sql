USE [Flyeats]
GO
/****** Object:  StoredProcedure [dbo].[SP_UpdateAddress]    Script Date: 11/25/2023 12:48:19 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_UpdateAddress] (
  @AddressId INT, 
  @AddressLine1 VARCHAR(255), 
  @AddressLine2 VARCHAR(255) = NULL, 
  @City VARCHAR(255), 
  @StateProvince VARCHAR(255), 
  @ZipPostalCode VARCHAR(255), 
  @Country VARCHAR(255), 
  @Latitude DECIMAL(9, 6) = NULL, 
  @Longitude DECIMAL(9, 6) = NULL, 
  @PhoneNumber VARCHAR(255) = NULL, 
  @AddressType VARCHAR(255) = NULL, 
  @Active BIT = NULL
) AS BEGIN 
SET 
  NOCOUNT ON;
UPDATE 
  Addresses 
SET 
  AddressLine1 = @AddressLine1, 
  AddressLine2 = @AddressLine2, 
  City = @City, 
  StateProvince = @StateProvince, 
  ZipPostalCode = @ZipPostalCode, 
  Country = @Country, 
  Latitude = @Latitude, 
  Longitude = @Longitude, 
  PhoneNumber = @PhoneNumber, 
  AddressType = @AddressType, 
  Active = @Active
WHERE 
  AddressId = @AddressId;
END


GO
