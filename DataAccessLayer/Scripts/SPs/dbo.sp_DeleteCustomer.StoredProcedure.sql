USE [Flyeats]
GO
/****** Object:  StoredProcedure [dbo].[sp_DeleteCustomer]    Script Date: 11/25/2023 12:48:19 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_DeleteCustomer]
  @UserId INT
AS
BEGIN
  DELETE FROM Users WHERE UserId = @UserId;
END;

GO
