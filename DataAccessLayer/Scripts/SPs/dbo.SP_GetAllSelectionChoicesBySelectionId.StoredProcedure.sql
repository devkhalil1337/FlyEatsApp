USE [Flyeats]
GO
/****** Object:  StoredProcedure [dbo].[SP_GetAllSelectionChoicesBySelectionId]    Script Date: 11/25/2023 12:48:19 AM ******/
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
