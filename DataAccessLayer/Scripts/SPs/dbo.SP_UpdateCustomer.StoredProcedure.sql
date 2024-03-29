USE [Flyeats]
GO
/****** Object:  StoredProcedure [dbo].[SP_UpdateCustomer]    Script Date: 11/25/2023 12:48:19 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_UpdateCustomer]
    @UserId INT,
    @FirstName VARCHAR(50),
    @LastName VARCHAR(50),
    @Email VARCHAR(255),
    @PhoneNumber VARCHAR(20),
    @BusinessId INT,
    @PasswordHash  [varchar](255),
    @Salt  [varchar](255)
AS
BEGIN
    UPDATE Users
    SET FirstName = @FirstName,
        LastName = @LastName,
        Email = @Email,
        PhoneNumber = @PhoneNumber,
        BusinessId = @BusinessId,
        PasswordHash = @PasswordHash,
        Salt = @Salt,
        UpdatedAt = GETDATE()
    WHERE UserId = @UserId
END

GO
