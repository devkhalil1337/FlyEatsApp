USE [Flyeats]
GO
/****** Object:  StoredProcedure [dbo].[SP_UpdateSelection]    Script Date: 11/25/2023 12:48:19 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[SP_UpdateSelection]
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
