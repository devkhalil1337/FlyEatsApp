USE [Flyeats]
GO
/****** Object:  StoredProcedure [dbo].[SP_UpdateOrderTypes]    Script Date: 11/25/2023 12:48:19 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[SP_UpdateOrderTypes] (@OrderTypesDetailsId INT,
                                             @OrderTypesId        INT,
                                             @BusinessId          INT,
                                             @Active              BIT)
AS
  BEGIN
      DECLARE @status INT

      IF NOT EXISTS(SELECT *
                    FROM   ordertypesdetails
                    WHERE  ordertypesdetailsid = @OrderTypesDetailsId)
        INSERT INTO ordertypesdetails
                    (businessid,
                     ordertypesid,
                     active)
        VALUES     (@BusinessId,
                    @OrderTypesId,
                    @Active);
      ELSE IF EXISTS(SELECT *
                FROM   ordertypesdetails
                WHERE  ordertypesdetailsid = @OrderTypesDetailsId)
        UPDATE ordertypesdetails
        SET    active = @Active
        WHERE  ordertypesdetailsid = @OrderTypesDetailsId;
  END

BEGIN
    SET @status = 1;

    RETURN @status;
END 

GO
