USE [Flyeats]
GO
/****** Object:  StoredProcedure [dbo].[SP_AddNewBusiness]    Script Date: 11/25/2023 12:48:19 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[SP_AddNewBusiness]
(
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
	@CreationDate datetime2(7) ,
	@UpdateDate datetime2(7) ,
	@Deleted bit ,
	@Active bit 
)
AS  
BEGIN   
   Insert BusinessInfo
		( 
		    Localization
		   ,BusinessLogo
           ,BusinessName
           ,BusinessContact
           ,BusinessEmail
           ,BusinessAddress
           ,BusinessPostcode
           ,BusinessCity
           ,BusinessCountry
           ,BusinessDetails
           ,BusinessLatitude
           ,BusinessLongitude
           ,BusinessCurrency
           ,BusinessWebsiteUrl
           ,BusinessTempClose
           ,ClosetillDate
           ,BusinessExpiryDate
           ,CreationDate
           ,UpdateDate
           ,Deleted
           ,Active
		 )
   Values
   (
		    @Localization
		   ,@BusinessLogo
           ,@BusinessName
           ,@BusinessContact
           ,@BusinessEmail
           ,@BusinessAddress
           ,@BusinessPostcode
           ,@BusinessCity
           ,@BusinessCountry
           ,@BusinessDetails
           ,@BusinessLatitude
           ,@BusinessLongitude
           ,@BusinessCurrency
           ,@BusinessWebsiteUrl
           ,@BusinessTempClose
           ,@ClosetillDate
           ,@BusinessExpiryDate
           ,@CreationDate
           ,@UpdateDate
           ,@Deleted
           ,@Active
   );
   SELECT SCOPE_IDENTITY();
END  


GO
