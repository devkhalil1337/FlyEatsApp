USE [Flyeats]
GO
/****** Object:  StoredProcedure [dbo].[SP_AddNewCategory]    Script Date: 11/25/2023 12:48:19 AM ******/
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
