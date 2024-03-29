USE [Flyeats]
GO
/****** Object:  StoredProcedure [dbo].[SP_AddNewSelection]    Script Date: 11/25/2023 12:48:19 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[SP_AddNewSelection]
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
