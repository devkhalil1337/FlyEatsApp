USE [Flyeats]
GO
/****** Object:  StoredProcedure [dbo].[SP_UpdateCategory]    Script Date: 11/25/2023 12:48:19 AM ******/
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
