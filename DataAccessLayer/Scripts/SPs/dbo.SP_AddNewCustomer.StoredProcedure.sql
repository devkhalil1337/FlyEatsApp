USE [Flyeats]
GO
/****** Object:  StoredProcedure [dbo].[SP_AddNewCustomer]    Script Date: 11/25/2023 12:48:19 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_AddNewCustomer]
    @FirstName VARCHAR(50),
    @LastName VARCHAR(50),
    @Email VARCHAR(255),
    @PhoneNumber VARCHAR(20),
    @BusinessId INT,
    @PasswordHash  [varchar](255),
    @Salt [varchar](255)
AS
BEGIN
    INSERT INTO Users (FirstName, LastName, Email, PhoneNumber, BusinessId, PasswordHash, Salt, CreatedAt, UpdatedAt)
    VALUES (@FirstName, @LastName, @Email, @PhoneNumber, @BusinessId, @PasswordHash, @Salt, GETDATE(), GETDATE())
    SELECT SCOPE_IDENTITY();
END

GO
