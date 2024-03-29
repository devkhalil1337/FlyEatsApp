USE [Flyeats]
GO
/****** Object:  StoredProcedure [dbo].[SP_AddNewSelectionChoice]    Script Date: 11/25/2023 12:48:19 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[SP_AddNewSelectionChoice]
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
