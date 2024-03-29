USE [Flyeats]
GO
/****** Object:  StoredProcedure [dbo].[SP_DeleteAddress]    Script Date: 11/25/2023 12:48:19 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_DeleteAddress] (
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


GO
