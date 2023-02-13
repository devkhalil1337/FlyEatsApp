CREATE PROCEDURE sp_CreateUser
  @FirstName VARCHAR(50),
  @LastName VARCHAR(50),
  @Email VARCHAR(255),
  @PhoneNumber VARCHAR(20),
  @BusinessId INT
AS
BEGIN
  INSERT INTO Users (FirstName, LastName, Email, PhoneNumber, BusinessId)
  VALUES (@FirstName, @LastName, @Email, @PhoneNumber, @BusinessId);
END;

CREATE PROCEDURE sp_GetUserById
  @UserId INT
AS
BEGIN
  SELECT * FROM Users WHERE UserId = @UserId;
END;

CREATE PROCEDURE sp_GetAllUsers
AS
BEGIN
  SELECT * FROM Users;
END;

CREATE PROCEDURE sp_UpdateUser
  @UserId INT,
  @FirstName VARCHAR(50),
  @LastName VARCHAR(50),
  @Email VARCHAR(255),
  @PhoneNumber VARCHAR(20),
  @BusinessId INT
AS
BEGIN
  UPDATE Users
  SET FirstName = @FirstName,
      LastName = @LastName,
      Email = @Email,
      PhoneNumber = @PhoneNumber,
      BusinessId = @BusinessId
  WHERE UserId = @UserId;
END;

CREATE PROCEDURE sp_DeleteUser
  @UserId INT
AS
BEGIN
  DELETE FROM Users WHERE UserId = @UserId;
END;
