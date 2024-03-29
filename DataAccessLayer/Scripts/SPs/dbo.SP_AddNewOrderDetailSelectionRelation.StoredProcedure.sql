USE [Flyeats]
GO
/****** Object:  StoredProcedure [dbo].[SP_AddNewOrderDetailSelectionRelation]    Script Date: 11/25/2023 12:48:19 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[SP_AddNewOrderDetailSelectionRelation]
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
           SELECT SCOPE_IDENTITY();
END  


GO
