USE [Flyeats]
GO
/****** Object:  StoredProcedure [dbo].[SP_UpdateBusinesInfo]    Script Date: 11/25/2023 12:48:19 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[SP_UpdateBusinesInfo]
(
	@BusinessId int,
	@Localization nvarchar(50) ,
	@BusinessLogo nvarchar(100) ,
	@BusinessName nvarchar(100) ,
	@BusinessContact nvarchar(50) ,
	@BusinessEmail nvarchar(50) ,
	@BusinessAddress nvarchar(255) ,
	@BusinessPostcode nvarchar(10) ,
	@BusinessCity nvarchar(30) ,
	@BusinessCountry nvarchar(30) ,
	@BusinessDetails nvarchar(255) ,
	@BusinessLatitude nvarchar(25) ,
	@BusinessLongitude nvarchar(25) ,
	@BusinessCurrency nvarchar(25) ,
	@BusinessWebsiteUrl nvarchar(50) ,
	@BusinessTempClose bit ,
	@ClosetillDate datetime2(7) ,
	@BusinessExpiryDate datetime2(7) ,
	@UpdateDate datetime2(7) ,
	@Deleted bit ,
	@Active bit 
)
AS  
BEGIN  
Declare @status int 

   update  BusinessInfo
		SET Localization = @Localization 
      ,BusinessLogo = @BusinessLogo 
      ,BusinessName = @BusinessName 
      ,BusinessContact = @BusinessContact
      ,BusinessEmail = @BusinessEmail
      ,BusinessAddress = @BusinessAddress
      ,BusinessPostcode = @BusinessPostcode
      ,BusinessCity = @BusinessCity
      ,BusinessCountry = @BusinessCountry
      ,BusinessDetails = @BusinessDetails
      ,BusinessLatitude = @BusinessLatitude 
      ,BusinessLongitude = @BusinessLongitude
      ,BusinessCurrency = @BusinessCurrency
      ,BusinessWebsiteUrl = @BusinessWebsiteUrl
      ,BusinessTempClose = @BusinessTempClose
      ,ClosetillDate = @ClosetillDate
      ,BusinessExpiryDate = @BusinessExpiryDate
      ,UpdateDate = @UpdateDate
      ,Deleted = @Deleted
      ,Active = @Active
	  where BusinessId=@BusinessId;

END  

Begin
   SET @status = 1;
    return @status;
 End  

GO
