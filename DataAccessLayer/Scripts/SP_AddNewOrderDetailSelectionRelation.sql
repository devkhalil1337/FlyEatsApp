USE [Flyeats]
GO
/****** Object:  StoredProcedure [dbo].[SP_AddNewOrderDetailSelectionRelation]    Script Date: 2/11/2023 4:02:30 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [dbo].[SP_AddNewOrderDetailSelectionRelation]
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
