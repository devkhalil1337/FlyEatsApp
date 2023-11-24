USE [Flyeats]
GO
/****** Object:  StoredProcedure [dbo].[SP_GetAddressByAddressId]    Script Date: 11/25/2023 12:48:19 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_GetAddressByAddressId] (@AddressId INT)
AS
BEGIN
  SELECT *
  FROM Addresses
  WHERE AddressId = @AddressId AND Active = 1
END


GO
