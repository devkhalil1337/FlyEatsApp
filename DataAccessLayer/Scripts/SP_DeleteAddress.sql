CREATE PROCEDURE SP_DeleteAddress (
  @AddressId INT,  
  @Active BIT = NULL
) AS BEGIN 
SET 
  NOCOUNT ON;
UPDATE 
  Addresses 
SET 
  Active = @Active
WHERE 
  AddressId = @AddressId;
END
