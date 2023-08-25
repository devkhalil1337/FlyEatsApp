USE [master]
GO
/****** Object:  Database [Flyeats]    Script Date: 8/25/2023 11:01:33 PM ******/
CREATE DATABASE [Flyeats]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'Flyeats', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL11.MSSQLSERVER\MSSQL\DATA\Flyeats.mdf' , SIZE = 4096KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'Flyeats_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL11.MSSQLSERVER\MSSQL\DATA\Flyeats_log.ldf' , SIZE = 1024KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [Flyeats] SET COMPATIBILITY_LEVEL = 110
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [Flyeats].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [Flyeats] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [Flyeats] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [Flyeats] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [Flyeats] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [Flyeats] SET ARITHABORT OFF 
GO
ALTER DATABASE [Flyeats] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [Flyeats] SET AUTO_CREATE_STATISTICS ON 
GO
ALTER DATABASE [Flyeats] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [Flyeats] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [Flyeats] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [Flyeats] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [Flyeats] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [Flyeats] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [Flyeats] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [Flyeats] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [Flyeats] SET  DISABLE_BROKER 
GO
ALTER DATABASE [Flyeats] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [Flyeats] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [Flyeats] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [Flyeats] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [Flyeats] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [Flyeats] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [Flyeats] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [Flyeats] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [Flyeats] SET  MULTI_USER 
GO
ALTER DATABASE [Flyeats] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [Flyeats] SET DB_CHAINING OFF 
GO
ALTER DATABASE [Flyeats] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [Flyeats] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
USE [Flyeats]
GO
/****** Object:  StoredProcedure [dbo].[SP_AddAddress]    Script Date: 8/25/2023 11:01:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_AddAddress] (
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

GO
/****** Object:  StoredProcedure [dbo].[SP_AddBusinessDays]    Script Date: 8/25/2023 11:01:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[SP_AddBusinessDays]
(
		   @BusinessId int,
		   @WeekDayName nvarchar(100),
		   @Active bit
      )
AS  
BEGIN   
   Insert BusinessDays
		( 
		    BusinessId,
			WeekDayName,
			CreationDate,
			UpdateDate,
			Active
	 )
   Values
   (
		    @BusinessId,
			@WeekDayName,
			GETDATE(),
			GETDATE(),
			@Active

   );
END  

GO
/****** Object:  StoredProcedure [dbo].[SP_AddBusinessTimes]    Script Date: 8/25/2023 11:01:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[SP_AddBusinessTimes]
(
		   @BusinessDaysId int,
		   @StartDay varchar(30),
		   @EndDay varchar(30)
    )
AS  
BEGIN   
   Insert BusinessTimes
		( 
		    BusinessDaysId,
			StartDay,
			EndDay
	 )
   Values
   (
		    @BusinessDaysId,
			@StartDay,
			@EndDay

   );
END  

GO
/****** Object:  StoredProcedure [dbo].[sp_AddInternalUser]    Script Date: 8/25/2023 11:01:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- CREATE PROCEDURE for adding a new user
CREATE PROCEDURE [dbo].[sp_AddInternalUser]
    @BusinessId INT,
    @FullName NVARCHAR(50),
    @Username NVARCHAR(50),
    @Email NVARCHAR(60),
    @Password NVARCHAR(500),
    @MobileNumber NVARCHAR(20),
    @AccountType INT,
    @Role INT,
    @CreationDate DATETIME2(7),
    @UpdateDate DATETIME2(7),
    @IsDeleted BIT,
    @Active BIT
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO [dbo].[InternalUsers] (
        [BusinessId],
        [FullName],
        [Username],
        [Email],
        [Password],
        [MobileNumber],
        [AccountType],
        [Role],
        [CreationDate],
        [UpdateDate],
        [IsDeleted],
        [Active]
    ) VALUES (
        @BusinessId,
        @FullName,
        @Username,
        @Email,
        @Password,
        @MobileNumber,
        @AccountType,
        @Role,
        @CreationDate,
        @UpdateDate,
        @IsDeleted,
        @Active
    )
	 SELECT SCOPE_IDENTITY();
END
GO
/****** Object:  StoredProcedure [dbo].[SP_AddNewBusiness]    Script Date: 8/25/2023 11:01:33 PM ******/
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
/****** Object:  StoredProcedure [dbo].[SP_AddNewCategory]    Script Date: 8/25/2023 11:01:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[SP_AddNewCategory]
(
			@BusinessId int,
			@CategoryImage nvarchar(150),
			@CategoryName nvarchar(150),
			@CategoryDetails nvarchar(500),
			@CategorySortBy int,
			@CreationDate datetime2(7),
			@UpdateDate datetime2(7),
			@IsDeleted bit,
			@Active bit
)
AS  
BEGIN   
   Insert Categories
		( 
			BusinessId,
		    CategoryImage,
			CategoryName,
			CategoryDetails,
			CategorySortBy,
			CreationDate,
			UpdateDate,
			IsDeleted,
			Active

		 )
   Values
   (
		    @BusinessId,
			@CategoryImage,
			@CategoryName,
			@CategoryDetails,
			@CategorySortBy,
			@CreationDate,
			@UpdateDate,
			@IsDeleted,
			@Active

   );
   SELECT SCOPE_IDENTITY();
END  

GO
/****** Object:  StoredProcedure [dbo].[SP_AddNewCustomer]    Script Date: 8/25/2023 11:01:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create PROCEDURE [dbo].[SP_AddNewCustomer]
    @FirstName VARCHAR(50),
    @LastName VARCHAR(50),
    @Email VARCHAR(255),
    @PhoneNumber VARCHAR(20),
    @BusinessId INT,
    @PasswordHash  [varchar](255),
    @Salt [varchar](255)
AS
BEGIN
    INSERT INTO Users (FirstName, LastName, Email, PhoneNumber, BusinessId, PasswordHash, Salt, CreatedAt, UpdatedAt)
    VALUES (@FirstName, @LastName, @Email, @PhoneNumber, @BusinessId, @PasswordHash, @Salt, GETDATE(), GETDATE())
END
GO
/****** Object:  StoredProcedure [dbo].[SP_AddNewOrder]    Script Date: 8/25/2023 11:01:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_AddNewOrder]
(
    @BusinessId INT,
    @CustomerId INT,
    @OrderInvoiceNumber NVARCHAR(255),
    @OrderType NVARCHAR(20),
    @OrderTableId INT,
    @OrderStatus NVARCHAR(50),
    @ServiceChargeAmount DECIMAL(10,2),
    @DiscountAmount DECIMAL(10,2),
    @VoucherId INT,
    @VoucherDiscountAmount DECIMAL(10,2),
    @TotalAmount DECIMAL(10,2),
    @VatAmount DECIMAL(10,2),
    @VatPercentage DECIMAL(10,2),
    @VatType NVARCHAR(30),
    @PaymentStatus NVARCHAR(30),
    @PaymentMethod NVARCHAR(30),
    @AveragePreparationTime INT,
    @Comments NVARCHAR(255),
    @DeliveryTime INT,
    @CustomerDeliveryId INT,
    @CompletedBy NVARCHAR(50),
    @DeliveryCharges DECIMAL(10,2),
    @CardPaymentId NVARCHAR(50) = NULL,
    @CreatedDate NVARCHAR(50) = NULL,
    @ModifiedDate DATETIME = NULL,
    @IsDeleted BIT
)
AS
BEGIN
    SET NOCOUNT ON;
    INSERT INTO Orders
    (
        BusinessId,
        CustomerId,
        OrderInvoiceNumber,
        OrderType,
        OrderTableId,
        OrderStatus,
        ServiceChargeAmount,
        DiscountAmount,
        VoucherId,
        VoucherDiscountAmount,
        TotalAmount,
        VatAmount,
        VatPercentage,
        VatType,
        PaymentStatus,
        PaymentMethod,
        AveragePreparationTime,
        Comments,
        DeliveryTime,
        CustomerDeliveryId,
        CompletedBy,
        DeliveryCharges,
        CardPaymentId,
        CreatedDate,
        ModifiedDate,
        IsDeleted
    )
    VALUES
    (
        @BusinessId,
        @CustomerId,
        @OrderInvoiceNumber,
        @OrderType,
        @OrderTableId,
        @OrderStatus,
        @ServiceChargeAmount,
        @DiscountAmount,
        @VoucherId,
        @VoucherDiscountAmount,
        @TotalAmount,
        @VatAmount,
        @VatPercentage,
        @VatType,
        @PaymentStatus,
        @PaymentMethod,
        @AveragePreparationTime,
        @Comments,
        @DeliveryTime,
        @CustomerDeliveryId,
        @CompletedBy,
        @DeliveryCharges,
        @CardPaymentId,
        @CreatedDate,
        @ModifiedDate,
        @IsDeleted
    )
END

GO
/****** Object:  StoredProcedure [dbo].[SP_AddNewOrderDetails]    Script Date: 8/25/2023 11:01:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[SP_AddNewOrderDetails]
(
	@OrderId nvarchar(255),
    @BusinessId int,
    @CategoryId int,
    @ProductId int,
    @VariantId int,
    @ProductName nvarchar(255),
    @ProductQuantity int,
    @ProductPrice decimal(8,2),
    @ProductComments nvarchar(255),
    @ProductHaveSelection bit
)
AS  
BEGIN   
 INSERT INTO [OrderDetails]
           (
      [OrderId]
      ,[BusinessId]
      ,[CategoryId]
      ,[ProductId]
      ,[VariantId]
      ,[ProductName]
      ,[ProductQuantity]
      ,[ProductPrice]
      ,[ProductComments]
      ,[ProductHaveSelection])
     VALUES
           (    
		   @OrderId,
           @BusinessId,
           @CategoryId,
           @ProductId,
           @VariantId,
           @ProductName,
           @ProductQuantity,
           @ProductPrice,
           @ProductComments,
           @ProductHaveSelection)
		   SELECT SCOPE_IDENTITY();
END  

GO
/****** Object:  StoredProcedure [dbo].[SP_AddNewOrderDetailSelectionRelation]    Script Date: 8/25/2023 11:01:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

Create PROCEDURE [dbo].[SP_AddNewOrderDetailSelectionRelation]
(
	@OrderDetailsId bigint,
    @BusinessId int,
    @SelectionId int,
    @ChoicesId int,
    @ChoiceName nvarchar(255),
    @ChoicePrice decimal(10, 2)
)
AS  
BEGIN   
 INSERT INTO [OrderDetailSelectionRelation]
           (
		      [OrderDetailsId]
           ,[BusinessId]
           ,[SelectionId]
           ,[ChoicesId]
           ,[ChoiceName]
           ,[ChoicePrice])
     VALUES
           (    
		   @OrderDetailsId,
           @BusinessId,
           @SelectionId,
           @ChoicesId,
           @ChoiceName,
           @ChoicePrice)
END  

GO
/****** Object:  StoredProcedure [dbo].[SP_AddNewProductSelection]    Script Date: 8/25/2023 11:01:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[SP_AddNewProductSelection]
(
		@ProductId int,
		@SelectionId int,
		@BusinessId int,
		@CreationDate datetime2(7),
		@UpdateDate datetime2(7)
      )
AS  
BEGIN   
Declare @status int
   Insert ProductSelection
		( 
		   	ProductId,
			SelectionId,
			BusinessId,
			CreationDate,
			UpdateDate

	 )
   Values
   (
		    @ProductId,
			@SelectionId,
			@BusinessId,
			@CreationDate,
			@UpdateDate

   );
END  
Begin
   SET @status = 1;
    return @status;
 End  

GO
/****** Object:  StoredProcedure [dbo].[SP_AddNewProductVariant]    Script Date: 8/25/2023 11:01:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[SP_AddNewProductVariant]
(
		   @ProductId int,
		   @BusinessId int,
		   @VariationName nvarchar(80),
		   @VariationPrice decimal(18,2),
		   @CreationDate datetime2(7),
		   @UpdateDate datetime2(7),
		   @IsDeleted bit,
		   @Active bit
      )
AS  
BEGIN   
   Insert ProductVariants
		( 
		   ProductId,
		   BusinessId,
		   VariationName,
		   VariationPrice,
		   CreationDate,
		   UpdateDate,
		   IsDeleted,
		   Active
	 )
   Values
   (
		   @ProductId,
		   @BusinessId,
		   @VariationName,
		   @VariationPrice,
		   @CreationDate,
		   @UpdateDate,
		   @IsDeleted,
		   @Active

   );
   SELECT SCOPE_IDENTITY();
END  

GO
/****** Object:  StoredProcedure [dbo].[SP_AddNewSelection]    Script Date: 8/25/2023 11:01:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

Create PROCEDURE [dbo].[SP_AddNewSelection]
(
		   @BusinessId int,
		   @SelectionName nvarchar(100),
		   @MinimumSelection int,
		   @MaximumSelection int,
		   @CreationDate datetime2(7),
		   @UpdateDate datetime2(7),
		   @IsDeleted bit,
		   @Active bit
      )
AS  
BEGIN   
   Insert Selections
		( 
		    BusinessId,
			SelectionName,
			MinimumSelection,
			MaximumSelection,
			CreationDate,
			UpdateDate,
			IsDeleted,
			Active
	 )
   Values
   (
		    @BusinessId,
			@SelectionName,
			@MinimumSelection,
			@MaximumSelection,
			@CreationDate,
			@UpdateDate,
			@IsDeleted,
			@Active

   );
   SELECT SCOPE_IDENTITY();
END  

GO
/****** Object:  StoredProcedure [dbo].[SP_AddNewSelectionChoice]    Script Date: 8/25/2023 11:01:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

Create PROCEDURE [dbo].[SP_AddNewSelectionChoice]
(

	
		@SelectionId int,
		@BusinessId int,
		@ChoiceName nvarchar(100),
		@ChoicePrice decimal(18,2),
		@ChoiceSortedBy int,
		@CreationDate datetime2(7),
		@UpdateDate datetime2(7),
		@IsDeleted bit
  

      )
AS  
BEGIN   
   Insert SelectionChoices
		( 
		    SelectionId,
			BusinessId,
			ChoiceName,
			ChoicePrice,
			ChoiceSortedBy,
			CreationDate,
			UpdateDate,
			IsDeleted


	 )
   Values
   (
		    @SelectionId,
			@BusinessId,
			@ChoiceName,
			@ChoicePrice,
			@ChoiceSortedBy,
			@CreationDate,
			@UpdateDate,
			@IsDeleted

   );
   SELECT SCOPE_IDENTITY();
END  

GO
/****** Object:  StoredProcedure [dbo].[SP_AddNewSettings]    Script Date: 8/25/2023 11:01:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[SP_AddNewSettings]
(
           @BusinessId int,
           @RegisterNumber nvarchar(100),
           @Vat decimal(18,2),
           @VatType nvarchar(15),
           @ServiceCharges decimal(18,2),
		   @DeliveryCharges decimal(18,2),
           @MinimumOrder decimal(18,2),
           @DeliveryTime decimal(18,2),
		   @AveragePrepareTime decimal(18,2),
           @CreationDate datetime2(7),
           @UpdateDate datetime2(7),
		   @IsDeliveryOrderActive bit,
     	   @IsCollectionOrderActive bit,
		   @IsTableOrderActive bit,
           @IsGuestLoginActive bit
)
AS  
BEGIN   
   Insert Settings
		( 
           BusinessId,
           RegisterNumber,
           Vat,
           VatType,
           ServiceCharges,
		   DeliveryCharges,
           MinimumOrder,
           DeliveryTime,
		   AveragePrepareTime,
           CreationDate,
           UpdateDate,
		   IsDeliveryOrderActive,
		   IsCollectionOrderActive,
		   IsTableOrderActive,
           IsGuestLoginActive

		 )
   Values
   (
           @BusinessId,
		   @RegisterNumber,
		   @Vat,
		   @VatType,
		   @ServiceCharges,
		   @DeliveryCharges,
		   @MinimumOrder,
		   @DeliveryTime,
		   @AveragePrepareTime,
           @CreationDate,
           @UpdateDate,
		   @IsDeliveryOrderActive,
		   @IsTableOrderActive,
		   @IsCollectionOrderActive,
		   @IsGuestLoginActive

   );
   SELECT SCOPE_IDENTITY();
END  

GO
/****** Object:  StoredProcedure [dbo].[SP_AddProduct]    Script Date: 8/25/2023 11:01:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_AddProduct]
  @BusinessId int,
  @CategoryId int,
  @ProductName nvarchar(150),
  @ProductDescription nvarchar(250),
  @ProductImage nvarchar(150),
  @ProductSortOrder int,
  @ProductQuantity int,
  @IsTableProduct bit,
  @TablePrice decimal(18, 2),
  @TableVat decimal(18, 2),
  @IsPickupProduct bit,
  @PickupPrice decimal(18, 2),
  @PickupVat decimal(18, 2),
  @IsDeliveryProduct bit,
  @DeliveryPrice decimal(18, 2),
  @DeliveryVat decimal(18, 2),
  @HasVariations bit,
  @Featured bit,
  @CreationDate datetime2,
  @ModifiedDate datetime2,
  @IsDeleted bit,
  @Active bit,
  @IsPopular bit
AS
BEGIN
  SET NOCOUNT ON;

  INSERT INTO Products (BusinessId, CategoryId, ProductName, ProductDescription, ProductImage, ProductSortOrder, ProductQuantity, IsTableProduct, TablePrice, TableVat, IsPickupProduct, PickupPrice, PickupVat, IsDeliveryProduct, DeliveryPrice, DeliveryVat, HasVariations, Featured, CreationDate, ModifiedDate, IsDeleted, Active,IsPopular)
  VALUES (@BusinessId, @CategoryId, @ProductName, @ProductDescription, @ProductImage, @ProductSortOrder, @ProductQuantity, @IsTableProduct, @TablePrice, @TableVat, @IsPickupProduct, @PickupPrice, @PickupVat, @IsDeliveryProduct, @DeliveryPrice, @DeliveryVat, @HasVariations, @Featured, @CreationDate, @ModifiedDate, @IsDeleted, @Active,@IsPopular)
END

GO
/****** Object:  StoredProcedure [dbo].[SP_CheckVoucherRedemptionEligibility]    Script Date: 8/25/2023 11:01:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_CheckVoucherRedemptionEligibility]
    @VoucherCode NVARCHAR(50),
    @UserId INT,
    @Amount DECIMAL(18, 2)
AS
BEGIN
    DECLARE @VoucherId INT
    DECLARE @RedemptionCount INT
    DECLARE @MaxRedemptionCount INT
    DECLARE @IsEligible INT

    -- Check if the voucher code exists in the table
    IF NOT EXISTS (SELECT 1 FROM Voucher WHERE VoucherCode = @VoucherCode)
    BEGIN
        SET @IsEligible = -1 -- Voucher code does not exist
        SELECT @IsEligible
        RETURN
    END

    -- Get the voucher ID for the given voucher code
    SELECT @VoucherId = VoucherId
    FROM Voucher
    WHERE VoucherCode = @VoucherCode

    -- Get the redemption count for the given user and voucher
    SELECT @RedemptionCount = COUNT(*)
    FROM VoucherRedemption
    WHERE VoucherId = @VoucherId AND UserId = @UserId

    -- Get the maximum redemption count for the voucher
    SELECT @MaxRedemptionCount = RedeemCount
    FROM Voucher
    WHERE VoucherId = @VoucherId

    -- Check if the user is eligible to redeem the voucher
    IF @RedemptionCount >= @MaxRedemptionCount
    BEGIN
        SET @IsEligible = 0 -- Not eligible
    END
    ELSE IF @Amount < (SELECT MinValue FROM Voucher WHERE VoucherId = @VoucherId)
    BEGIN
        SET @IsEligible = 1 -- Not eligible
    END
    ELSE
    BEGIN
        SET @IsEligible = 2 -- Eligible
    END
    
    SELECT @IsEligible
END

GO
/****** Object:  StoredProcedure [dbo].[SP_CreateVoucher]    Script Date: 8/25/2023 11:01:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_CreateVoucher]
    @VoucherCode nvarchar(50),
    @MinValue decimal(18, 2),
    @MaxValue decimal(18, 2),
    @StartDate datetime,
    @EndDate datetime,
    @CreatedBy int,
    @BusinessId int,
    @IsActive bit,
	@RedeemCount int
AS
BEGIN
    INSERT INTO Voucher (VoucherCode, MinValue, MaxValue, StartDate, EndDate, CreatedOn, CreatedBy, BusinessId, IsActive,RedeemCount)
    VALUES (@VoucherCode, @MinValue, @MaxValue, @StartDate, @EndDate, GETDATE(), @CreatedBy, @BusinessId, @IsActive,@RedeemCount);
    SELECT SCOPE_IDENTITY() AS VoucherId;
END;

GO
/****** Object:  StoredProcedure [dbo].[SP_DeleteAddress]    Script Date: 8/25/2023 11:01:33 PM ******/
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
/****** Object:  StoredProcedure [dbo].[SP_DeleteBusinessInfo]    Script Date: 8/25/2023 11:01:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[SP_DeleteBusinessInfo]
(
	@BusinessId int	
)
AS  
BEGIN   
Declare @status int  
   update  BusinessInfo
		SET Deleted=1 where BusinessId=@BusinessId 
   Begin
   SET @status = 1;
    return @status;
 End
END  

GO
/****** Object:  StoredProcedure [dbo].[SP_DeleteBusinessTimesById]    Script Date: 8/25/2023 11:01:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[SP_DeleteBusinessTimesById]
(
	@BusinessTimesId int
)
AS  
BEGIN  

  delete from BusinessTimes WHERE BusinessTimesId=@BusinessTimesId

END  
GO
/****** Object:  StoredProcedure [dbo].[SP_DeleteCategoryById]    Script Date: 8/25/2023 11:01:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[SP_DeleteCategoryById]
(
	@CategoryId
 int
)
AS  
BEGIN   
Declare @status int  
   update  Categories
		SET IsDeleted=1 where CategoryId=@CategoryId 
   Begin
   SET @status = 1;
    return @status;
 End
END  
 
GO
/****** Object:  StoredProcedure [dbo].[sp_DeleteCustomer]    Script Date: 8/25/2023 11:01:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create PROCEDURE [dbo].[sp_DeleteCustomer]
  @UserId INT
AS
BEGIN
  DELETE FROM Users WHERE UserId = @UserId;
END;
GO
/****** Object:  StoredProcedure [dbo].[sp_DeletePaymentGatewayConfig]    Script Date: 8/25/2023 11:01:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_DeletePaymentGatewayConfig]
(
    @Id INT
)
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE [dbo].[PaymentGatewayConfiguration]
    SET [IsDeleted] = 1,
        [UpdateDate] = GETUTCDATE()
    WHERE [Id] = @Id
    AND [IsDeleted] = 0;
END

GO
/****** Object:  StoredProcedure [dbo].[SP_DeleteProductById]    Script Date: 8/25/2023 11:01:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[SP_DeleteProductById]
(
	@ProductId int
	
)
AS  
BEGIN   
Declare @status int  
   update  Products
		SET IsDeleted=1  where ProductId=@ProductId;
   Begin
   SET @status = 1;
    return @status;
 End
END  
 
GO
/****** Object:  StoredProcedure [dbo].[SP_DeleteProductSelectionById]    Script Date: 8/25/2023 11:01:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[SP_DeleteProductSelectionById]
(
	@ProductId int
	
)
AS  
BEGIN   
Declare @status int  
DELETE FROM ProductSelection where ProductId=@ProductId;
  
   Begin
   SET @status = 1;
    return @status;
 End
END  
GO
/****** Object:  StoredProcedure [dbo].[SP_DeleteProductVariantById]    Script Date: 8/25/2023 11:01:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[SP_DeleteProductVariantById]
(
	@ProductId int
	
)
AS  
BEGIN   
Declare @status int  
If EXISTS(Select Count(*) from ProductVariants where ProductId = @ProductId)
   update  ProductVariants SET IsDeleted=1 where ProductId = @ProductId
   Begin
   SET @status = 1;
    return @status;
 End
END  
 
GO
/****** Object:  StoredProcedure [dbo].[SP_DeleteRedemption]    Script Date: 8/25/2023 11:01:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create PROCEDURE [dbo].[SP_DeleteRedemption]
    @RedeemId INT
AS
BEGIN
    DELETE FROM VoucherRedemption WHERE RedeemId = @RedeemId
END

GO
/****** Object:  StoredProcedure [dbo].[SP_DeleteSelectionById]    Script Date: 8/25/2023 11:01:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

Create PROCEDURE [dbo].[SP_DeleteSelectionById]
(
	@SelectionId int
	
)
AS  
BEGIN   
Declare @status int  
   update  Selections
		SET IsDeleted=1  where SelectionId=@SelectionId;
   Begin
   SET @status = 1;
    return @status;
 End
END  
GO
/****** Object:  StoredProcedure [dbo].[SP_DeleteSelectionChoicesById]    Script Date: 8/25/2023 11:01:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

Create PROCEDURE [dbo].[SP_DeleteSelectionChoicesById]
(
	@ChoicesId int
	
)
AS  
BEGIN   
Declare @status int  
   update  SelectionChoices
		SET IsDeleted=1  where ChoicesId=@ChoicesId;
   Begin
   SET @status = 1;
    return @status;
 End
END  
GO
/****** Object:  StoredProcedure [dbo].[SP_DeleteVoucher]    Script Date: 8/25/2023 11:01:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_DeleteVoucher]
    @VoucherId int
AS
BEGIN
    DELETE FROM Voucher WHERE VoucherId = @VoucherId;
END;

GO
/****** Object:  StoredProcedure [dbo].[SP_GetAddressByAddressId]    Script Date: 8/25/2023 11:01:33 PM ******/
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
/****** Object:  StoredProcedure [dbo].[SP_GetAddressesForUser]    Script Date: 8/25/2023 11:01:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_GetAddressesForUser] (@UserId INT)
AS
BEGIN
    SET NOCOUNT ON;

    SELECT addressId, userId, addressLine1, addressLine2, city, stateProvince, zipPostalCode, country, latitude, longitude, phoneNumber, addressType, active, timestamp
    FROM Addresses
    WHERE UserId = @UserId;
END

GO
/****** Object:  StoredProcedure [dbo].[SP_GetAllCategoriesByBusinessId]    Script Date: 8/25/2023 11:01:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[SP_GetAllCategoriesByBusinessId]
(
	@BusinessId int
)
AS  
BEGIN   
   select * from Categories where BusinessId=@BusinessId;
END  
GO
/****** Object:  StoredProcedure [dbo].[SP_GetAllInternalUserByBusinessId]    Script Date: 8/25/2023 11:01:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_GetAllInternalUserByBusinessId]
    @BusinessId int
AS
BEGIN
    SELECT *
    FROM [dbo].[InternalUsers]
    WHERE [BusinessId] = @BusinessId
END

GO
/****** Object:  StoredProcedure [dbo].[SP_GetAllOrdersByBusinessId]    Script Date: 8/25/2023 11:01:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[SP_GetAllOrdersByBusinessId]
(
    @BusinessId int,
    @StartDate datetime,
    @EndDate datetime,
    @OrderStatus varchar(50) = NULL
)
AS
BEGIN

    if (@OrderStatus IS NULL OR @OrderStatus = '')
    begin
        select * from [dbo].[Orders] where BusinessId=@BusinessId and CreatedDate >= @StartDate and CreatedDate <= DATEADD(day, 1, @EndDate);
    end
    else
    begin
        select * from [dbo].[Orders] where BusinessId=@BusinessId and CreatedDate >= @StartDate and CreatedDate <= DATEADD(day, 1, @EndDate) and OrderStatus=@OrderStatus;
    end

END

GO
/****** Object:  StoredProcedure [dbo].[SP_GetAllProductsByBusinessId]    Script Date: 8/25/2023 11:01:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[SP_GetAllProductsByBusinessId]
(
	@BusinessId int
)
AS  
BEGIN  

  select p.ProductId, c.CategoryName,p.CategoryId
      ,p.BusinessId
      ,p.ProductImage
      ,p.ProductName
      ,p.ProductDescription
	  ,p.IsTableProduct
      ,p.TablePrice
      ,p.TableVat
      ,p.IsPickupProduct
      ,p.PickupPrice
      ,p.PickupVat
	  ,p.IsDeliveryProduct
      ,p.DeliveryPrice
      ,p.DeliveryVat
      ,p.ProductSortOrder
      ,p.ProductQuantity
      ,p.HasVariations
      ,p.Featured
      ,p.CreationDate
      ,p.ModifiedDate
      ,p.IsDeleted
      ,p.Active,
	   p.IsPopular FROM Flyeats.dbo.Products p inner join Flyeats.dbo.Categories c on c.categoryId = p.CategoryId where p.BusinessId=@BusinessId and p.IsDeleted=0;

   /*select * from Products where BusinessId=@BusinessId and IsDeleted=0;*/
END  
GO
/****** Object:  StoredProcedure [dbo].[SP_GetAllProductSelectionByProductId]    Script Date: 8/25/2023 11:01:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

Create PROCEDURE [dbo].[SP_GetAllProductSelectionByProductId]
(
	@ProductId int
)
AS  
BEGIN  

   select * from ProductSelection where ProductId=@ProductId;
END  
GO
/****** Object:  StoredProcedure [dbo].[SP_GetAllProductsVariantsByProductId]    Script Date: 8/25/2023 11:01:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[SP_GetAllProductsVariantsByProductId]
(
	@ProductId int
)
AS  
BEGIN  
   select * from ProductVariants where ProductId=@ProductId and IsDeleted=0;
END  
GO
/****** Object:  StoredProcedure [dbo].[SP_GetAllSelectionByBusinessId]    Script Date: 8/25/2023 11:01:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

Create PROCEDURE [dbo].[SP_GetAllSelectionByBusinessId]
(
	@BusinessId int
)
AS  
BEGIN  

   select * from Selections where BusinessId=@BusinessId;
END  
GO
/****** Object:  StoredProcedure [dbo].[SP_GetAllSelectionChoicesBySelectionId]    Script Date: 8/25/2023 11:01:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[SP_GetAllSelectionChoicesBySelectionId]
(
	@SelectionId int
)
AS  
BEGIN  

   select * from SelectionChoices where SelectionId=@SelectionId AND IsDeleted = 0;
END  
GO
/****** Object:  StoredProcedure [dbo].[SP_GetAllVouchersByBusinessId]    Script Date: 8/25/2023 11:01:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create PROCEDURE [dbo].[SP_GetAllVouchersByBusinessId]
    @BusinessId INT
AS
BEGIN
    SELECT * FROM Voucher WHERE BusinessId = @BusinessId
END

GO
/****** Object:  StoredProcedure [dbo].[SP_GetBusinessDaysById]    Script Date: 8/25/2023 11:01:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[SP_GetBusinessDaysById]
(
	@BusinessId int
)
AS  
BEGIN  

   IF NOT EXISTS(Select 1 from BusinessDays WHERE BusinessId=@BusinessId)
    BEGIN
	execute [dbo].[SP_AddBusinessDays] @BusinessId,'Monday',1
	execute [dbo].[SP_AddBusinessDays] @BusinessId,'Tuesday',1
	execute [dbo].[SP_AddBusinessDays] @BusinessId,'Wednesday',1
	execute [dbo].[SP_AddBusinessDays] @BusinessId,'Thuresday',1
	execute [dbo].[SP_AddBusinessDays] @BusinessId,'Friday',1
	execute [dbo].[SP_AddBusinessDays] @BusinessId,'Saturday',1
	execute [dbo].[SP_AddBusinessDays] @BusinessId,'Sunday',1
	Select * from BusinessDays WHERE BusinessId=@BusinessId
   End
   ELSE
   BEGIN
	Select * from BusinessDays WHERE BusinessId=@BusinessId
   END
END  
GO
/****** Object:  StoredProcedure [dbo].[SP_GetBusinessInfo]    Script Date: 8/25/2023 11:01:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[SP_GetBusinessInfo]
(
	@BusinessId int	
)
AS  
BEGIN  

   Select * from BusinessInfo where BusinessId=@BusinessId 
   SELECT SCOPE_IDENTITY();
END  

GO
/****** Object:  StoredProcedure [dbo].[SP_GetBusinessTimesById]    Script Date: 8/25/2023 11:01:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[SP_GetBusinessTimesById]
(
	@BusinessDaysId int
)
AS  
BEGIN  
	if NOT EXISTS(Select * from BusinessTimes WHERE BusinessDaysId=@BusinessDaysId)
	Begin
/*		execute SP_AddBusinessTimes @BusinessDaysId,'2017-02-23 08:00','2017-02-23 19:00' */
        Select * from BusinessTimes WHERE BusinessDaysId=@BusinessDaysId
	END
	ELSE
	Begin
	 Select * from BusinessTimes WHERE BusinessDaysId=@BusinessDaysId
	END

END  
GO
/****** Object:  StoredProcedure [dbo].[SP_GetCategoryDetailById]    Script Date: 8/25/2023 11:01:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[SP_GetCategoryDetailById]
(
	@CategoryId
 int
)
AS  
BEGIN  

   select * from Categories where CategoryId=@CategoryId;
END  
GO
/****** Object:  StoredProcedure [dbo].[SP_GetCustomerByEmail]    Script Date: 8/25/2023 11:01:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_GetCustomerByEmail]
(
@Email VARCHAR(255)
)
AS
BEGIN
SET NOCOUNT ON;

SELECT [UserId]
  ,[FirstName]
  ,[LastName]
  ,[Email]
  ,[PhoneNumber]
  ,[BusinessId]
  ,[PasswordHash]
  ,[Salt]
  ,[isGuest]
  ,[CreatedAt]
  ,[UpdatedAt]
FROM [dbo].[Users]
WHERE [Email] = @Email
END
GO
/****** Object:  StoredProcedure [dbo].[sp_GetCustomerById]    Script Date: 8/25/2023 11:01:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create PROCEDURE [dbo].[sp_GetCustomerById]
  @UserId INT
AS
BEGIN
  SELECT * FROM Users WHERE UserId = @UserId;
END;
GO
/****** Object:  StoredProcedure [dbo].[SP_GetGrossSales]    Script Date: 8/25/2023 11:01:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_GetGrossSales]
    @StartDate DATE,
    @EndDate DATE,
    @BusinessId INT
AS
BEGIN
    SELECT SUM(TotalAmount) AS GrossSales, CONVERT(VARCHAR(10), CreatedDate, 120) AS CreatedDate
    FROM Orders
    WHERE BusinessId = @BusinessId
        AND OrderStatus IN ('completed', 'Delivered')
        AND CreatedDate >= @StartDate
        AND CreatedDate <= @EndDate
    GROUP BY CONVERT(VARCHAR(10), CreatedDate, 120)
END

GO
/****** Object:  StoredProcedure [dbo].[sp_GetInternalUserByCredentials]    Script Date: 8/25/2023 11:01:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- CREATE PROCEDURE for retrieving a user by username and password
CREATE PROCEDURE [dbo].[sp_GetInternalUserByCredentials]
    @Username NVARCHAR(50),
    @Password NVARCHAR(500)
AS
BEGIN
    SET NOCOUNT ON;

    SELECT *
    FROM [dbo].[InternalUsers]
    WHERE [Username] = @Username
        AND [Password] = @Password
        AND [IsDeleted] = 0
        AND [Active] = 1
END
GO
/****** Object:  StoredProcedure [dbo].[SP_GetOrderById]    Script Date: 8/25/2023 11:01:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[SP_GetOrderById]
(
	@OrderInvoiceNumber nvarchar(255)
)
AS  
BEGIN  

   select * FROM [Flyeats].[dbo].[Orders] where OrderInvoiceNumber = @OrderInvoiceNumber;
END  
GO
/****** Object:  StoredProcedure [dbo].[SP_GetOrdersByCustomerId]    Script Date: 8/25/2023 11:01:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[SP_GetOrdersByCustomerId]
(
	@CustomerId INT
)
AS  
BEGIN  

   select * FROM [Flyeats].[dbo].[Orders] where CustomerId = @CustomerId ORDER BY OrderId DESC;;
END  
GO
/****** Object:  StoredProcedure [dbo].[SP_GetOrdersByTypeAndDateRange]    Script Date: 8/25/2023 11:01:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_GetOrdersByTypeAndDateRange]
	@BusinessId int,
    @FromDate datetime,
    @ToDate datetime,
    @OrderStatus nvarchar(20)
AS
BEGIN
    SELECT count(*) as NumberOfOrders
    FROM [Flyeats].[dbo].[Orders]
    WHERE [OrderStatus] = @OrderStatus
	AND [BusinessId] = @BusinessId
    AND [CreatedDate] >= @FromDate
    AND [CreatedDate] <= @ToDate
END

GO
/****** Object:  StoredProcedure [dbo].[SP_GetOrdersDetailsByOrderId]    Script Date: 8/25/2023 11:01:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[SP_GetOrdersDetailsByOrderId]
(
	@OrderId  nvarchar(255)
)
AS  
BEGIN  

  select * from [OrderDetails] where OrderId=@OrderId;

END  
GO
/****** Object:  StoredProcedure [dbo].[SP_GetOrderSelectionsById]    Script Date: 8/25/2023 11:01:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

Create PROCEDURE [dbo].[SP_GetOrderSelectionsById]
(
	@OrderDetailsId bigint
)
AS  
BEGIN  

   select * FROM [Flyeats].[dbo].[OrderDetailSelectionRelation] where OrderDetailsId = @OrderDetailsId;
END  
GO
/****** Object:  StoredProcedure [dbo].[SP_GetPaymentGatewaysByBusinessId]    Script Date: 8/25/2023 11:01:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create PROCEDURE [dbo].[SP_GetPaymentGatewaysByBusinessId]
    @businessId INT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT *
    FROM [dbo].[PaymentGatewayKeys]
    WHERE [BusinessId] = @businessId
END

GO
/****** Object:  StoredProcedure [dbo].[SP_GetProductDetailById]    Script Date: 8/25/2023 11:01:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[SP_GetProductDetailById]
(
	@ProductId int

 
)
AS  
BEGIN  

   select p.ProductId, c.CategoryName,p.CategoryId
      ,p.BusinessId
      ,p.ProductImage
      ,p.ProductName
      ,p.ProductDescription
	  ,p.IsTableProduct
      ,p.TablePrice
      ,p.TableVat
      ,p.IsPickupProduct
      ,p.PickupPrice
      ,p.PickupVat
	  ,p.IsDeliveryProduct
      ,p.DeliveryPrice
      ,p.DeliveryVat
      ,p.ProductSortOrder
      ,p.ProductQuantity
      ,p.HasVariations
      ,p.Featured
      ,p.CreationDate
      ,p.ModifiedDate
      ,p.IsDeleted
      ,p.Active FROM Flyeats.dbo.Products p inner join Flyeats.dbo.Categories c on c.categoryId = p.CategoryId where ProductId = @ProductId
END  
GO
/****** Object:  StoredProcedure [dbo].[SP_GetRedemptionById]    Script Date: 8/25/2023 11:01:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create PROCEDURE [dbo].[SP_GetRedemptionById]
    @RedeemId INT
AS
BEGIN
    SELECT RedeemId, VoucherId, UserId, RedeemAmount, RedeemCount
    FROM VoucherRedemption
    WHERE RedeemId = @RedeemId
END

GO
/****** Object:  StoredProcedure [dbo].[SP_GetRedemptionsByVoucherId]    Script Date: 8/25/2023 11:01:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create PROCEDURE [dbo].[SP_GetRedemptionsByVoucherId]
    @VoucherId INT
AS
BEGIN
    SELECT RedeemId, VoucherId, UserId, RedeemAmount, RedeemCount
    FROM VoucherRedemption
    WHERE VoucherId = @VoucherId
END

GO
/****** Object:  StoredProcedure [dbo].[SP_GetSelectionById]    Script Date: 8/25/2023 11:01:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

Create PROCEDURE [dbo].[SP_GetSelectionById]
(
	@SelectionId int
)
AS  
BEGIN  

   select * from Selections where SelectionId=@SelectionId;
END  
GO
/****** Object:  StoredProcedure [dbo].[SP_GetSettingsById]    Script Date: 8/25/2023 11:01:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[SP_GetSettingsById]
(
	@BusinessId int

 
)
AS  
BEGIN  
   select * FROM Settings  where BusinessId = @BusinessId
END  
GO
/****** Object:  StoredProcedure [dbo].[SP_GetUserByEmail]    Script Date: 8/25/2023 11:01:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[SP_GetUserByEmail]
(
	@email nvarchar(50)
)
AS  
BEGIN  
 
   select * from InternalSignup where Email = @email;
END  
GO
/****** Object:  StoredProcedure [dbo].[SP_GetUserByUserName]    Script Date: 8/25/2023 11:01:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

Create PROCEDURE [dbo].[SP_GetUserByUserName]
(
	@Username nvarchar(50)
)
AS  
BEGIN  
 
   select * from InternalUsers where Username = @Username;
END  
GO
/****** Object:  StoredProcedure [dbo].[SP_GetVoucherByCode]    Script Date: 8/25/2023 11:01:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


Create PROCEDURE [dbo].[SP_GetVoucherByCode]
    @VoucherCode NVARCHAR(150)
AS
BEGIN
    SET NOCOUNT ON;

    SELECT *
    FROM [dbo].[Voucher]
    WHERE VoucherCode = @VoucherCode;
END

GO
/****** Object:  StoredProcedure [dbo].[SP_GetVoucherById]    Script Date: 8/25/2023 11:01:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_GetVoucherById]
    @VoucherId int
AS
BEGIN
    SELECT * FROM Voucher WHERE VoucherId = @VoucherId;
END;

GO
/****** Object:  StoredProcedure [dbo].[sp_InsertPaymentGatewayConfig]    Script Date: 8/25/2023 11:01:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_InsertPaymentGatewayConfig]
(
    @BusinessId int,
    @GatewayName VARCHAR(50),
    @ApiKey VARCHAR(100),
    @ApiSecret VARCHAR(100),
    @IsActive BIT,
    @PaymentMode INT
)
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO [dbo].[PaymentGatewayKeys]
    (
     	[BusinessId],
        [GatewayName],
        [ApiKey],
        [ApiSecret],
        [IsActive],
        [PaymentMode]
    )
    VALUES
    (
		@BusinessId,
        @GatewayName,
        @ApiKey,
        @ApiSecret,
        @IsActive,
        @PaymentMode
    );
END

GO
/****** Object:  StoredProcedure [dbo].[SP_InsertRedemption]    Script Date: 8/25/2023 11:01:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create PROCEDURE [dbo].[SP_InsertRedemption]
    @VoucherId INT,
    @UserId INT,
    @RedeemAmount DECIMAL(18,2),
    @RedeemCount INT
AS
BEGIN
    INSERT INTO VoucherRedemption (VoucherId, UserId, RedeemAmount, RedeemCount)
    VALUES (@VoucherId, @UserId, @RedeemAmount, @RedeemCount)
END

GO
/****** Object:  StoredProcedure [dbo].[SP_SignUpNewUser]    Script Date: 8/25/2023 11:01:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[SP_SignUpNewUser]
(
			@BusinessId int,
           @FullName nvarchar(50),
           @Username nvarchar(50),
           @Email nvarchar(60),
           @Password nvarchar(500),
           @MobileNumber nvarchar(20),
           @AccountType int,
           @Role int,
           @CreationDate datetime2(7),
           @UpdateDate datetime2(7),
           @IsDeleted bit,
           @Active bit
)
AS  
BEGIN  

   INSERT INTO [dbo].[InternalSignup]
           ([BusinessId]
           ,[FullName]
           ,[Username]
           ,[Email]
           ,[Password]
           ,[MobileNumber]
           ,[AccountType]
           ,[Role]
           ,[CreationDate]
           ,[UpdateDate]
           ,[IsDeleted]
           ,[Active])
     VALUES
   
   (		@BusinessId ,
           @FullName ,
           @Username ,
           @Email ,
           @Password,
           @MobileNumber ,
           @AccountType ,
           @Role ,
           @CreationDate ,
           @UpdateDate,
           @IsDeleted ,
           @Active 
   );
   SELECT SCOPE_IDENTITY();
END  
GO
/****** Object:  StoredProcedure [dbo].[SP_UpdateAddress]    Script Date: 8/25/2023 11:01:33 PM ******/
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
/****** Object:  StoredProcedure [dbo].[SP_UpdateBusinesInfo]    Script Date: 8/25/2023 11:01:33 PM ******/
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
   Begin
   SET @status = 1;
 End
    return @status;
END  

GO
/****** Object:  StoredProcedure [dbo].[SP_UpdateBusinessDays]    Script Date: 8/25/2023 11:01:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[SP_UpdateBusinessDays]
(
		   @BusinessDaysId int,
		   @BusinessId int,
		   @WeekDayName nvarchar(100),
		   @CreationDate datetime,
		   @UpdateDate datetime,
		   @Active bit
)
AS  
BEGIN  
Declare @status int  

   update  BusinessDays
		SET 
	       
			BusinessId=@BusinessId,
			WeekDayName=@WeekDayName,
			CreationDate=@CreationDate,
			UpdateDate=@UpdateDate,
			Active=@Active

	  where BusinessDaysId=@BusinessDaysId
   
END
Begin
   SET @status = 1;
    return @status;
 End  

GO
/****** Object:  StoredProcedure [dbo].[SP_UpdateBusinessTimes]    Script Date: 8/25/2023 11:01:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[SP_UpdateBusinessTimes]
(
           
		   @BusinessTimesId int,
           @StartDay varchar(30),
		   @EndDay varchar(30)
)
AS  
BEGIN  
Declare @status int  

   update  BusinessTimes
		SET 
	       
			StartDay=@StartDay,
			EndDay=@EndDay

	  where BusinessTimesId=@BusinessTimesId
   
END
Begin
   SET @status = 1;
    return @status;
 End  

GO
/****** Object:  StoredProcedure [dbo].[SP_UpdateCategory]    Script Date: 8/25/2023 11:01:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[SP_UpdateCategory]
(
			@CategoryId int,
			@BusinessId int,
			@CategoryImage nvarchar(150),
			@CategoryName nvarchar(150),
			@CategoryDetails nvarchar(500),
			@CategorySortBy int,
			@UpdateDate datetime2(7),
			@IsDeleted bit,
			@Active bit
)
AS  
BEGIN  
Declare @status int  
 
   update  Categories
		SET CategoryImage=@CategoryImage,
			CategoryName=@CategoryName,
			CategoryDetails=@CategoryDetails,
			CategorySortBy=@CategorySortBy,
			UpdateDate=@UpdateDate,
			IsDeleted=@IsDeleted,
			Active=@Active
	  where CategoryId=@CategoryId and BusinessId = @BusinessId;
   
END
Begin
   SET @status = 1;
    return @status;
 End  

GO
/****** Object:  StoredProcedure [dbo].[SP_UpdateCustomer]    Script Date: 8/25/2023 11:01:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create PROCEDURE [dbo].[SP_UpdateCustomer]
    @UserId INT,
    @FirstName VARCHAR(50),
    @LastName VARCHAR(50),
    @Email VARCHAR(255),
    @PhoneNumber VARCHAR(20),
    @BusinessId INT,
    @PasswordHash  [varchar](255),
    @Salt  [varchar](255)
AS
BEGIN
    UPDATE Users
    SET FirstName = @FirstName,
        LastName = @LastName,
        Email = @Email,
        PhoneNumber = @PhoneNumber,
        BusinessId = @BusinessId,
        PasswordHash = @PasswordHash,
        Salt = @Salt,
        UpdatedAt = GETDATE()
    WHERE UserId = @UserId
END
GO
/****** Object:  StoredProcedure [dbo].[SP_UpdateOrder]    Script Date: 8/25/2023 11:01:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

Create PROCEDURE [dbo].[SP_UpdateOrder]
(
    @OrderInvoiceNumber nvarchar(255),
    @OrderStatus nvarchar(50),
    @OrderDeliveryTime int,
    @OrderCompletedBy nvarchar(50),
    @IsDeleted bit
)
AS  
BEGIN  
Declare @status int  

   update  [Order]
		SET 
      OrderInvoiceNumber = @OrderInvoiceNumber,
      OrderStatus = @OrderStatus,
      OrderDeliveryTime = @OrderDeliveryTime,
      OrderCompletedBy = @OrderCompletedBy,
      IsDeleted = @IsDeleted 
	  where OrderInvoiceNumber=@OrderInvoiceNumber;
   Begin
   SET @status = 1;
 End
    return @status;
END  

GO
/****** Object:  StoredProcedure [dbo].[SP_UpdateOrderStatusByInvoiceNumber]    Script Date: 8/25/2023 11:01:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_UpdateOrderStatusByInvoiceNumber]
    @orderInvoiceNumber NVARCHAR(50),
    @newOrderStatus NVARCHAR(50)
AS
BEGIN
    DECLARE @rowsAffected INT;

    UPDATE [dbo].[Orders]
    SET [OrderStatus] = @newOrderStatus
    WHERE [OrderInvoiceNumber] = @orderInvoiceNumber;

    SELECT @rowsAffected = @@ROWCOUNT;

    IF @rowsAffected > 0
    BEGIN
        RETURN 1;
    END
    ELSE
    BEGIN
        RETURN 0;
    END
END

GO
/****** Object:  StoredProcedure [dbo].[SP_UpdateOrderTypes]    Script Date: 8/25/2023 11:01:33 PM ******/
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
/****** Object:  StoredProcedure [dbo].[sp_UpdatePaymentGatewayConfig]    Script Date: 8/25/2023 11:01:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_UpdatePaymentGatewayConfig]
(
    @Id INT,
    @GatewayName VARCHAR(50),
    @ApiKey VARCHAR(100),
    @ApiSecret VARCHAR(100),
    @IsActive BIT,
    @PaymentMode INT
)
AS
BEGIN
DECLARE @status INT
    SET NOCOUNT ON;

    UPDATE [dbo].[PaymentGatewayKeys]
    SET [GatewayName] = @GatewayName,
        [ApiKey] = @ApiKey,
        [ApiSecret] = @ApiSecret,
        [IsActive] = @IsActive,
        [PaymentMode] = @PaymentMode
    WHERE [Id] = @Id
END
BEGIN
    SET @status = 1;

    RETURN @status;
END 
GO
/****** Object:  StoredProcedure [dbo].[SP_UpdateProduct]    Script Date: 8/25/2023 11:01:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_UpdateProduct]
  @ProductId int,
  @BusinessId int,
  @CategoryId int,
  @ProductName nvarchar(250),
  @ProductDescription nvarchar(500),
  @ProductImage nvarchar(250),
  @ProductSortOrder int,
  @ProductQuantity int,
  @IsTableProduct bit,
  @TablePrice decimal(18, 2),
  @TableVat decimal(18, 2),
  @IsPickupProduct bit,
  @PickupPrice decimal(18, 2),
  @PickupVat decimal(18, 2),
  @IsDeliveryProduct bit,
  @DeliveryPrice decimal(18, 2),
  @DeliveryVat decimal(18, 2),
  @HasVariations bit,
  @Featured bit,
  @ModifiedDate datetime2,
  @IsDeleted bit,
  @Active bit
AS
BEGIN
  SET NOCOUNT ON;

  UPDATE Products
  SET 
    BusinessId = @BusinessId,
    CategoryId = @CategoryId,
    ProductName = @ProductName,
    ProductDescription = @ProductDescription,
    ProductImage = @ProductImage,
    ProductSortOrder = @ProductSortOrder,
    ProductQuantity = @ProductQuantity,
    IsTableProduct = @IsTableProduct,
    TablePrice = @TablePrice,
    TableVat = @TableVat,
    IsPickupProduct = @IsPickupProduct,
    PickupPrice = @PickupPrice,
    PickupVat = @PickupVat,
    IsDeliveryProduct = @IsDeliveryProduct,
    DeliveryPrice = @DeliveryPrice,
    DeliveryVat = @DeliveryVat,
    HasVariations = @HasVariations,
    Featured = @Featured,
    ModifiedDate = @ModifiedDate,
    IsDeleted = @IsDeleted,
    Active = @Active
  WHERE ProductId = @ProductId;
  IF @@ROWCOUNT = 1
	BEGIN
		RETURN 1;
	END
	ELSE
	BEGIN
		RETURN 0;
	END
END

GO
/****** Object:  StoredProcedure [dbo].[SP_UpdateProductSelection]    Script Date: 8/25/2023 11:01:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[SP_UpdateProductSelection]
(
		@ProductSelectionId int,
		@ProductId int,
		@SelectionId int,
		@BusinessId int,
		@UpdateDate datetime2(7)

		)
AS  
BEGIN  
   update  ProductSelection
		SET 
	       
			ProductId=@ProductId ,
			SelectionId=@SelectionId ,
			BusinessId=@BusinessId ,
			UpdateDate=@UpdateDate 


	  where ProductSelectionId=@ProductSelectionId
   SELECT SCOPE_IDENTITY();
END

GO
/****** Object:  StoredProcedure [dbo].[SP_UpdateProductVariant]    Script Date: 8/25/2023 11:01:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[SP_UpdateProductVariant]
(
		   @VariantId int,
		   @ProductId int,
		   @BusinessId int,
		   @VariationName nvarchar(80),
		   @VariationPrice decimal(18,2),
		   @CreationDate datetime2(7),
		   @UpdateDate datetime2(7),
		   @IsDeleted bit,
		   @Active bit
)
AS  
BEGIN  
Declare @status int  

   update  ProductVariants
		SET 
	       
			ProductId=@ProductId,
			BusinessId=@BusinessId,
			VariationName=@VariationName,
			VariationPrice=@VariationPrice,
			CreationDate=@CreationDate,
			UpdateDate=@UpdateDate,
			IsDeleted=@IsDeleted,
			Active=@Active

	  where VariantId=@VariantId
   
END
Begin
   SET @status = 1;
    return @status;
 End  

GO
/****** Object:  StoredProcedure [dbo].[SP_UpdateSelection]    Script Date: 8/25/2023 11:01:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

Create PROCEDURE [dbo].[SP_UpdateSelection]
(
		   @SelectionId int,
		   @BusinessId int,
		   @SelectionName nvarchar(100),
		   @MinimumSelection int,
		   @MaximumSelection int,
		   @CreationDate datetime2(7),
		   @UpdateDate datetime2(7),
		   @IsDeleted bit,
		   @Active bit
)
AS  
BEGIN  
Declare @status int  

   update  Selections
		SET 
	       
			BusinessId=@BusinessId,
			SelectionName=@SelectionName,
			MinimumSelection=@MinimumSelection,
			MaximumSelection=@MaximumSelection,
			CreationDate=@CreationDate,
			UpdateDate=@UpdateDate,
			IsDeleted=@IsDeleted,
			Active=@Active

	  where SelectionId=@SelectionId
   
END
Begin
   SET @status = 1;
    return @status;
 End  

GO
/****** Object:  StoredProcedure [dbo].[SP_UpdateSelectionChoice]    Script Date: 8/25/2023 11:01:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

create PROCEDURE [dbo].[SP_UpdateSelectionChoice]
(
		@ChoicesId int,
		@SelectionId int,
		@BusinessId int,
		@ChoiceName nvarchar(100),
		@ChoicePrice decimal(18,2),
		@ChoiceSortedBy int,
		@CreationDate datetime2(7),
		@UpdateDate datetime2(7),
		@IsDeleted bit
		)
AS  
BEGIN  
Declare @status int  

   update  SelectionChoices
		SET 
	       
			SelectionId= @SelectionId,
			BusinessId= @BusinessId,
			ChoiceName= @ChoiceName,
			ChoicePrice= @ChoicePrice,
			ChoiceSortedBy= @ChoiceSortedBy,
			CreationDate= @CreationDate,
			UpdateDate= @UpdateDate,
			IsDeleted= @IsDeleted

	  where ChoicesId=@ChoicesId
   
END
Begin
   SET @status = 1;
    return @status;
 End  

GO
/****** Object:  StoredProcedure [dbo].[SP_UpdateSettings]    Script Date: 8/25/2023 11:01:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[SP_UpdateSettings]
(
		   @SettingsId int,
	       @BusinessId int,
           @RegisterNumber nvarchar(100),
           @Vat decimal(18,2),
           @VatType nvarchar(15),
           @ServiceCharges decimal(18,2),
		   @DeliveryCharges decimal(18,2),
           @MinimumOrder decimal(18,2),
           @DeliveryTime decimal(18,2),
		   @AveragePrepareTime decimal(18,2),
           @CreationDate datetime2(7),
           @UpdateDate datetime2(7),
		   @IsDeliveryOrderActive bit,
     	   @IsCollectionOrderActive bit,
		   @IsTableOrderActive bit,
           @IsGuestLoginActive bit
)
AS  
BEGIN  
Declare @status int  

   update Settings
		SET 
           BusinessId=@BusinessId,
           RegisterNumber=@RegisterNumber,
           Vat=@Vat,
           VatType=@VatType,
           ServiceCharges=@ServiceCharges,
		   DeliveryCharges = @DeliveryCharges,
           MinimumOrder=@MinimumOrder,
           DeliveryTime=@DeliveryTime,
		   AveragePrepareTime=@AveragePrepareTime,
           UpdateDate=@UpdateDate,
           IsGuestLoginActive=@IsGuestLoginActive,
		   IsDeliveryOrderActive=@IsDeliveryOrderActive,
		   IsCollectionOrderActive=@IsCollectionOrderActive,
		   IsTableOrderActive=@IsTableOrderActive
	  where SettingsId=@SettingsId and BusinessId = @BusinessId;
   
END
Begin
   SET @status = 1;
    return @status;
 End  

GO
/****** Object:  StoredProcedure [dbo].[SP_UpdateVoucher]    Script Date: 8/25/2023 11:01:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create PROCEDURE [dbo].[SP_UpdateVoucher]
    @VoucherId INT,
    @VoucherCode NVARCHAR(50),
    @MinValue DECIMAL(18,2),
    @MaxValue DECIMAL(18,2),
    @StartDate DATETIME,
    @EndDate DATETIME,
    @IsActive BIT,
    @RedeemCount INT
AS
BEGIN
    UPDATE Voucher SET 
        VoucherCode = @VoucherCode,
        MinValue = @MinValue,
        MaxValue = @MaxValue,
        StartDate = @StartDate,
        EndDate = @EndDate,
        IsActive = @IsActive,
        RedeemCount = @RedeemCount
    WHERE VoucherId = @VoucherId
END

GO
/****** Object:  StoredProcedure [dbo].[SP_UserSignIn]    Script Date: 8/25/2023 11:01:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[SP_UserSignIn]
(
	@email nvarchar(60) ,
	@password nvarchar(500)
)
AS  
BEGIN  
   select * from InternalSignup where Email = @email and [Password] = @password
END 
GO
/****** Object:  StoredProcedure [dbo].[UpdateRedemption]    Script Date: 8/25/2023 11:01:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[UpdateRedemption]
    @RedeemId INT,
    @VoucherId INT,
    @UserId INT,
    @RedeemAmount DECIMAL(18,2),
    @RedeemCount INT
AS
BEGIN
    UPDATE VoucherRedemption
    SET VoucherId = @VoucherId, UserId = @UserId, RedeemAmount = @RedeemAmount, RedeemCount = @RedeemCount
    WHERE RedeemId = @RedeemId
END

GO
/****** Object:  StoredProcedure [dbo].[UpdateVoucher]    Script Date: 8/25/2023 11:01:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[UpdateVoucher]
    @VoucherId INT,
    @VoucherCode NVARCHAR(50),
    @MinValue DECIMAL(18,2),
    @MaxValue DECIMAL(18,2),
    @StartDate DATETIME,
    @EndDate DATETIME,
    @IsActive BIT,
    @RedeemCount INT
AS
BEGIN
    UPDATE Voucher SET 
        VoucherCode = @VoucherCode,
        MinValue = @MinValue,
        MaxValue = @MaxValue,
        StartDate = @StartDate,
        EndDate = @EndDate,
        IsActive = @IsActive,
        RedeemCount = @RedeemCount
    WHERE VoucherId = @VoucherId
END

GO
/****** Object:  Table [dbo].[Addresses]    Script Date: 8/25/2023 11:01:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Addresses](
	[AddressId] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NOT NULL,
	[AddressLine1] [varchar](255) NOT NULL,
	[AddressLine2] [varchar](255) NULL,
	[City] [varchar](255) NOT NULL,
	[StateProvince] [varchar](255) NOT NULL,
	[ZipPostalCode] [varchar](255) NOT NULL,
	[Country] [varchar](255) NOT NULL,
	[Latitude] [decimal](9, 6) NULL,
	[Longitude] [decimal](9, 6) NULL,
	[PhoneNumber] [varchar](255) NULL,
	[AddressType] [varchar](255) NULL,
	[Active] [bit] NOT NULL DEFAULT ((1)),
	[Timestamp] [datetime] NULL DEFAULT (getdate()),
PRIMARY KEY CLUSTERED 
(
	[AddressId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[BusinessDays]    Script Date: 8/25/2023 11:01:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BusinessDays](
	[BusinessDaysId] [bigint] IDENTITY(1,1) NOT NULL,
	[BusinessId] [bigint] NULL,
	[WeekDayName] [nchar](20) NULL,
	[CreationDate] [datetime2](7) NULL,
	[UpdateDate] [datetime2](7) NULL,
	[Active] [bit] NULL,
 CONSTRAINT [PK_dbo.BusinessDays] PRIMARY KEY CLUSTERED 
(
	[BusinessDaysId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[BusinessInfo]    Script Date: 8/25/2023 11:01:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BusinessInfo](
	[BusinessId] [int] IDENTITY(1,1) NOT NULL,
	[Localization] [nvarchar](50) NOT NULL,
	[BusinessLogo] [nvarchar](100) NOT NULL,
	[BusinessName] [nvarchar](100) NOT NULL,
	[BusinessContact] [nvarchar](50) NOT NULL,
	[BusinessEmail] [nvarchar](50) NOT NULL,
	[BusinessAddress] [nvarchar](255) NOT NULL,
	[BusinessPostcode] [nvarchar](10) NOT NULL,
	[BusinessCity] [nvarchar](30) NOT NULL,
	[BusinessCountry] [nvarchar](30) NOT NULL,
	[BusinessDetails] [nvarchar](255) NOT NULL,
	[BusinessLatitude] [nvarchar](25) NOT NULL,
	[BusinessLongitude] [nvarchar](25) NOT NULL,
	[BusinessCurrency] [nvarchar](25) NOT NULL,
	[BusinessWebsiteUrl] [nvarchar](50) NOT NULL,
	[BusinessTempClose] [bit] NOT NULL,
	[ClosetillDate] [datetime2](7) NOT NULL,
	[BusinessExpiryDate] [datetime2](7) NOT NULL,
	[CreationDate] [datetime2](7) NOT NULL,
	[UpdateDate] [datetime2](7) NOT NULL,
	[Deleted] [bit] NOT NULL,
	[Active] [bit] NOT NULL,
 CONSTRAINT [PK_BusinessInfo] PRIMARY KEY CLUSTERED 
(
	[BusinessId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [UQ_BusinessEmail] UNIQUE NONCLUSTERED 
(
	[BusinessEmail] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[BusinessTimes]    Script Date: 8/25/2023 11:01:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[BusinessTimes](
	[BusinessTimesId] [bigint] IDENTITY(1,1) NOT NULL,
	[BusinessDaysId] [bigint] NULL,
	[StartDay] [varchar](30) NULL,
	[EndDay] [varchar](30) NULL,
 CONSTRAINT [PK_BusinessTimes] PRIMARY KEY CLUSTERED 
(
	[BusinessTimesId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Categories]    Script Date: 8/25/2023 11:01:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Categories](
	[CategoryId] [int] IDENTITY(1,1) NOT NULL,
	[BusinessId] [int] NOT NULL,
	[CategoryImage] [nvarchar](150) NOT NULL,
	[CategoryName] [nvarchar](150) NOT NULL,
	[CategoryDetails] [nvarchar](500) NOT NULL,
	[CategorySortBy] [int] NOT NULL,
	[CreationDate] [datetime2](7) NOT NULL,
	[UpdateDate] [datetime2](7) NOT NULL,
	[IsDeleted] [bit] NOT NULL,
	[Active] [bit] NOT NULL,
 CONSTRAINT [PK_Categories] PRIMARY KEY CLUSTERED 
(
	[CategoryId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[InternalSignup]    Script Date: 8/25/2023 11:01:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[InternalSignup](
	[InternalSignupId] [int] IDENTITY(1,1) NOT NULL,
	[BusinessId] [int] NOT NULL,
	[FullName] [nvarchar](50) NOT NULL,
	[Username] [nvarchar](50) NOT NULL,
	[Email] [nvarchar](60) NOT NULL,
	[Password] [nvarchar](500) NOT NULL,
	[MobileNumber] [nvarchar](20) NULL,
	[AccountType] [int] NOT NULL,
	[Role] [int] NOT NULL,
	[CreationDate] [datetime2](7) NOT NULL,
	[UpdateDate] [datetime2](7) NOT NULL,
	[IsDeleted] [bit] NOT NULL,
	[Active] [bit] NOT NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[InternalUsers]    Script Date: 8/25/2023 11:01:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[InternalUsers](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[BusinessId] [int] NOT NULL,
	[FullName] [nvarchar](50) NOT NULL,
	[Username] [nvarchar](50) NOT NULL,
	[Email] [nvarchar](60) NULL,
	[Password] [nvarchar](500) NOT NULL,
	[MobileNumber] [nvarchar](20) NULL,
	[AccountType] [int] NOT NULL,
	[Role] [int] NOT NULL,
	[CreationDate] [datetime2](7) NULL,
	[UpdateDate] [datetime2](7) NULL,
	[IsDeleted] [bit] NOT NULL,
	[Active] [bit] NOT NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[OrderDetails]    Script Date: 8/25/2023 11:01:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OrderDetails](
	[OrderDetailsId] [bigint] IDENTITY(1,1) NOT NULL,
	[OrderId] [nvarchar](255) NOT NULL,
	[BusinessId] [int] NOT NULL,
	[CategoryId] [int] NOT NULL,
	[ProductId] [int] NOT NULL,
	[VariantId] [int] NOT NULL,
	[ProductName] [nvarchar](255) NOT NULL,
	[ProductQuantity] [int] NOT NULL,
	[ProductPrice] [decimal](8, 2) NOT NULL,
	[ProductComments] [nvarchar](255) NULL,
	[ProductHaveSelection] [bit] NOT NULL,
 CONSTRAINT [PK_OrderDetails] PRIMARY KEY CLUSTERED 
(
	[OrderDetailsId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[OrderDetailSelectionRelation]    Script Date: 8/25/2023 11:01:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OrderDetailSelectionRelation](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[OrderDetailsId] [int] NOT NULL,
	[BusinessId] [int] NOT NULL,
	[SelectionId] [int] NOT NULL,
	[ChoicesId] [int] NOT NULL,
	[ChoiceName] [nvarchar](255) NULL,
	[ChoicePrice] [decimal](10, 2) NULL,
 CONSTRAINT [PK_OrderDetailSelectionRelation] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Orders]    Script Date: 8/25/2023 11:01:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Orders](
	[OrderId] [bigint] IDENTITY(1,1) NOT NULL,
	[BusinessId] [int] NOT NULL,
	[CustomerId] [int] NOT NULL,
	[OrderInvoiceNumber] [nvarchar](255) NOT NULL,
	[OrderType] [nvarchar](20) NOT NULL,
	[OrderTableId] [int] NOT NULL,
	[OrderStatus] [nvarchar](50) NOT NULL,
	[ServiceChargeAmount] [decimal](10, 2) NOT NULL,
	[DiscountAmount] [decimal](10, 2) NOT NULL,
	[VoucherId] [int] NOT NULL,
	[VoucherDiscountAmount] [decimal](10, 2) NOT NULL,
	[TotalAmount] [decimal](10, 2) NOT NULL,
	[VatAmount] [decimal](10, 2) NOT NULL,
	[VatPercentage] [decimal](10, 2) NOT NULL,
	[VatType] [nvarchar](30) NOT NULL,
	[PaymentStatus] [nvarchar](30) NOT NULL,
	[PaymentMethod] [nvarchar](30) NOT NULL,
	[AveragePreparationTime] [int] NOT NULL,
	[Comments] [nvarchar](255) NOT NULL,
	[DeliveryTime] [int] NOT NULL,
	[CustomerDeliveryId] [int] NOT NULL,
	[CompletedBy] [nvarchar](50) NOT NULL,
	[DeliveryCharges] [decimal](10, 2) NOT NULL,
	[CardPaymentId] [nvarchar](50) NULL,
	[CreatedDate] [nvarchar](50) NULL,
	[ModifiedDate] [datetime] NULL,
	[IsDeleted] [bit] NOT NULL,
 CONSTRAINT [PK_Orders] PRIMARY KEY CLUSTERED 
(
	[OrderId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[PaymentGatewayKeys]    Script Date: 8/25/2023 11:01:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PaymentGatewayKeys](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[BusinessId] [int] NOT NULL,
	[GatewayName] [nvarchar](50) NOT NULL,
	[ApiKey] [nvarchar](500) NOT NULL,
	[ApiSecret] [nvarchar](500) NOT NULL,
	[IsActive] [bit] NOT NULL DEFAULT ((0)),
	[PaymentMode] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [Unique_BusinessId_GatewayName] UNIQUE NONCLUSTERED 
(
	[BusinessId] ASC,
	[GatewayName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Products]    Script Date: 8/25/2023 11:01:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Products](
	[ProductId] [int] IDENTITY(1,1) NOT NULL,
	[BusinessId] [int] NOT NULL,
	[CategoryId] [int] NOT NULL,
	[ProductName] [nvarchar](150) NOT NULL,
	[ProductDescription] [nvarchar](250) NOT NULL,
	[ProductImage] [nvarchar](150) NOT NULL,
	[ProductSortOrder] [int] NOT NULL,
	[ProductQuantity] [int] NOT NULL,
	[IsTableProduct] [bit] NOT NULL,
	[TablePrice] [decimal](18, 2) NOT NULL,
	[TableVat] [decimal](18, 2) NOT NULL,
	[IsPickupProduct] [bit] NOT NULL,
	[PickupPrice] [decimal](18, 2) NOT NULL,
	[PickupVat] [decimal](18, 2) NOT NULL,
	[IsDeliveryProduct] [bit] NOT NULL,
	[DeliveryPrice] [decimal](18, 2) NOT NULL,
	[DeliveryVat] [decimal](18, 2) NOT NULL,
	[HasVariations] [bit] NOT NULL,
	[Featured] [bit] NOT NULL,
	[CreationDate] [datetime2](7) NOT NULL,
	[ModifiedDate] [datetime2](7) NOT NULL,
	[IsDeleted] [bit] NOT NULL,
	[Active] [bit] NOT NULL,
	[IsPopular] [bit] NOT NULL DEFAULT ((0))
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ProductSelection]    Script Date: 8/25/2023 11:01:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProductSelection](
	[ProductSelectionId] [int] IDENTITY(1,1) NOT NULL,
	[ProductId] [int] NULL,
	[SelectionId] [int] NULL,
	[BusinessId] [int] NULL,
	[CreationDate] [datetime2](7) NULL,
	[UpdateDate] [datetime2](7) NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ProductVariants]    Script Date: 8/25/2023 11:01:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProductVariants](
	[VariantId] [int] IDENTITY(1,1) NOT NULL,
	[ProductId] [int] NOT NULL,
	[BusinessId] [int] NOT NULL,
	[VariationName] [nvarchar](80) NOT NULL,
	[VariationPrice] [decimal](18, 2) NOT NULL,
	[CreationDate] [datetime2](7) NOT NULL,
	[UpdateDate] [datetime2](7) NOT NULL,
	[IsDeleted] [bit] NOT NULL,
	[Active] [bit] NOT NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[SelectionChoices]    Script Date: 8/25/2023 11:01:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SelectionChoices](
	[ChoicesId] [int] IDENTITY(1,1) NOT NULL,
	[SelectionId] [int] NULL,
	[BusinessId] [int] NULL,
	[ChoiceName] [nvarchar](100) NULL,
	[ChoicePrice] [decimal](18, 2) NULL,
	[ChoiceSortedBy] [int] NULL,
	[CreationDate] [datetime2](7) NULL,
	[UpdateDate] [datetime2](7) NULL,
	[IsDeleted] [bit] NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Selections]    Script Date: 8/25/2023 11:01:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Selections](
	[SelectionId] [int] IDENTITY(1,1) NOT NULL,
	[BusinessId] [int] NOT NULL,
	[SelectionName] [nvarchar](100) NULL,
	[MinimumSelection] [int] NULL,
	[MaximumSelection] [int] NULL,
	[CreationDate] [datetime2](7) NULL,
	[UpdateDate] [datetime2](7) NULL,
	[IsDeleted] [bit] NULL,
	[Active] [bit] NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Settings]    Script Date: 8/25/2023 11:01:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Settings](
	[SettingsId] [int] IDENTITY(1,1) NOT NULL,
	[BusinessId] [int] NOT NULL,
	[RegisterNumber] [nvarchar](100) NULL,
	[Vat] [decimal](10, 2) NULL,
	[VatType] [nchar](15) NULL,
	[ServiceCharges] [decimal](10, 2) NULL,
	[DeliveryCharges] [decimal](10, 2) NULL,
	[MinimumOrder] [decimal](10, 2) NULL,
	[AveragePrepareTime] [decimal](10, 2) NULL,
	[DeliveryTime] [decimal](10, 2) NULL,
	[IsGuestLoginActive] [bit] NULL,
	[IsDeliveryOrderActive] [bit] NULL,
	[IsCollectionOrderActive] [bit] NULL,
	[IsTableOrderActive] [bit] NULL,
	[CreationDate] [datetime2](7) NULL,
	[UpdateDate] [datetime2](7) NULL,
 CONSTRAINT [PK_Settings] PRIMARY KEY CLUSTERED 
(
	[SettingsId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Users]    Script Date: 8/25/2023 11:01:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Users](
	[UserId] [int] IDENTITY(1,1) NOT NULL,
	[FirstName] [varchar](50) NULL,
	[LastName] [varchar](50) NULL,
	[Email] [varchar](255) NULL,
	[PhoneNumber] [varchar](20) NULL,
	[BusinessId] [int] NULL,
	[PasswordHash] [varchar](255) NULL,
	[Salt] [varchar](255) NULL,
	[isGuest] [bit] NOT NULL DEFAULT ((0)),
	[CreatedAt] [datetime] NULL DEFAULT (getdate()),
	[UpdatedAt] [datetime] NULL DEFAULT (getdate()),
PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Voucher]    Script Date: 8/25/2023 11:01:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Voucher](
	[VoucherId] [int] IDENTITY(1,1) NOT NULL,
	[VoucherCode] [nvarchar](50) NOT NULL,
	[MinValue] [decimal](18, 2) NOT NULL,
	[MaxValue] [decimal](18, 2) NOT NULL,
	[StartDate] [datetime] NOT NULL,
	[EndDate] [datetime] NOT NULL,
	[CreatedOn] [datetime] NOT NULL,
	[CreatedBy] [int] NOT NULL,
	[BusinessId] [int] NOT NULL,
	[IsActive] [bit] NOT NULL,
	[RedeemCount] [int] NOT NULL,
 CONSTRAINT [PK_Voucher] PRIMARY KEY CLUSTERED 
(
	[VoucherId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[VoucherRedemption]    Script Date: 8/25/2023 11:01:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[VoucherRedemption](
	[RedeemId] [int] IDENTITY(1,1) NOT NULL,
	[VoucherId] [int] NOT NULL,
	[UserId] [int] NOT NULL,
	[RedeemDateTime] [datetime] NOT NULL DEFAULT (getdate()),
	[RedeemAmount] [decimal](18, 2) NOT NULL,
	[RedeemCount] [int] NOT NULL DEFAULT ((1)),
PRIMARY KEY CLUSTERED 
(
	[RedeemId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[Orders]  WITH CHECK ADD  CONSTRAINT [FK_Orders_BusinessInfo] FOREIGN KEY([BusinessId])
REFERENCES [dbo].[BusinessInfo] ([BusinessId])
GO
ALTER TABLE [dbo].[Orders] CHECK CONSTRAINT [FK_Orders_BusinessInfo]
GO
ALTER TABLE [dbo].[Products]  WITH CHECK ADD  CONSTRAINT [FK_Products_BusinessInfo] FOREIGN KEY([BusinessId])
REFERENCES [dbo].[BusinessInfo] ([BusinessId])
GO
ALTER TABLE [dbo].[Products] CHECK CONSTRAINT [FK_Products_BusinessInfo]
GO
ALTER TABLE [dbo].[VoucherRedemption]  WITH CHECK ADD FOREIGN KEY([VoucherId])
REFERENCES [dbo].[Voucher] ([VoucherId])
GO
USE [master]
GO
ALTER DATABASE [Flyeats] SET  READ_WRITE 
GO
