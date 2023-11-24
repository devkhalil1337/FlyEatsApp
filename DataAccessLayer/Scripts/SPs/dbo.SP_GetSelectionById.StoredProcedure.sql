USE [Flyeats]
GO
/****** Object:  StoredProcedure [dbo].[SP_GetSelectionById]    Script Date: 11/25/2023 12:48:19 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[SP_GetSelectionById]
(
	@SelectionId int,
	@BusinessId int
)
AS  
BEGIN  

   select * from Selections where SelectionId=@SelectionId and BusinessId = @BusinessId;
END  

GO
