
USE [ProjectsRepositoryDB];




Go 

 --------INSERT--------

CREATE OR ALTER PROCEDURE Users_InsertNewUser

@UserName VARCHAR(100), 
@Email VARCHAR(255), 
@PasswordHash VARCHAR(MAX), 
@ProfileImagePath VARCHAR(255), 
@SpecialtyID INT, 
@Points INT, 
@CreatedAt DATETIME, 
@UpdatedAt DATETIME, 
@PersonID INT, 
@NewUserID INT OUTPUT

AS

BEGIN
  SET NOCOUNT ON;

   INSERT INTO Users (UserName, Email, PasswordHash, ProfileImagePath, SpecialtyID, Points, CreatedAt, UpdatedAt, PersonID)
   VALUES (@UserName, @Email, @PasswordHash, @ProfileImagePath, @SpecialtyID, @Points, @CreatedAt, @UpdatedAt, @PersonID);
   SET @NewUserID = SCOPE_IDENTITY();

END
GO


Go 

 --------Update--------

CREATE OR ALTER PROCEDURE Users_UpdateUser

@UserID INT,
@UserName VARCHAR(100),
@Email VARCHAR(255),
@PasswordHash VARCHAR(MAX),
@ProfileImagePath VARCHAR(255),
@SpecialtyID INT,
@Points INT,
@CreatedAt DATETIME,
@UpdatedAt DATETIME,
@PersonID INT

AS

BEGIN
  SET NOCOUNT OFF;

   Update Users 

   SET UserName = @UserName, Email = @Email, PasswordHash = @PasswordHash, ProfileImagePath = @ProfileImagePath, SpecialtyID = @SpecialtyID, Points = @Points, CreatedAt = @CreatedAt, UpdatedAt = @UpdatedAt, PersonID = @PersonID
   WHERE UserID = @UserID;

    SELECT @@ROWCOUNT AS RowsAffected;

END
GO


Go 

 --------Delete--------

CREATE OR ALTER PROCEDURE Users_DeleteUser

@UserID INT

AS

BEGIN
  SET NOCOUNT OFF;


    DELETE FROM Users
   WHERE UserID = @UserID;

    SELECT @@ROWCOUNT AS RowsAffected;

END
GO


Go 

 --------Is Exists--------

CREATE OR ALTER PROCEDURE Users_CheckUserExists

@UserID INT

AS

BEGIN
  SET NOCOUNT ON;


     IF EXISTS( SELECT 1 FROM Users 
     WHERE UserID = @UserID)
         RETURN 1;
    ELSE
         RETURN 0;

END
GO


Go 

 --------Get Record By--------

CREATE OR ALTER PROCEDURE Users_GetUserByUserID

@UserID INT

AS

BEGIN
  SET NOCOUNT ON;

    SELECT * FROM Users WHERE UserID = @UserID;

END
GO


Go 

 --------Get All Records--------

CREATE OR ALTER PROCEDURE Users_GetAllUsers



AS

BEGIN
  SET NOCOUNT ON;

    SELECT * FROM Users;

END
GO