USE [Flyeats]
GO
/****** Object:  StoredProcedure [dbo].[sp_GetCustomerById]    Script Date: 11/25/2023 12:48:19 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_GetCustomerById]
  @UserId INT
AS
BEGIN
  SELECT * FROM Users WHERE UserId = @UserId;
END;

GO
