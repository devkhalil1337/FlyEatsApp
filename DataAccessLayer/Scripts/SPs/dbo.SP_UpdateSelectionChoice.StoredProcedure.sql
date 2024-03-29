USE [Flyeats]
GO
/****** Object:  StoredProcedure [dbo].[SP_UpdateSelectionChoice]    Script Date: 11/25/2023 12:48:19 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[SP_UpdateSelectionChoice]
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
