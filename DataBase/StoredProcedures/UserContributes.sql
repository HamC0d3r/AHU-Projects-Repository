
USE [ProjectsRepositoryDB];




Go 

 --------INSERT--------

CREATE OR ALTER PROCEDURE UserContributes_InsertNewUserContribute

@UserID INT, 
@ProjectPostID INT, 
@Description TEXT, 
@NewContributeID INT OUTPUT

AS

BEGIN
  SET NOCOUNT ON;

   INSERT INTO UserContributes (UserID, ProjectPostID, Description)
   VALUES (@UserID, @ProjectPostID, @Description);
   SET @NewContributeID = SCOPE_IDENTITY();

END
GO


Go 

 --------Update--------

CREATE OR ALTER PROCEDURE UserContributes_UpdateUserContribute

@ContributeID INT,
@UserID INT,
@ProjectPostID INT,
@Description TEXT

AS

BEGIN
  SET NOCOUNT OFF;

   Update UserContributes 

   SET UserID = @UserID, ProjectPostID = @ProjectPostID, Description = @Description
   WHERE ContributeID = @ContributeID;

    SELECT @@ROWCOUNT AS RowsAffected;

END
GO


Go 

 --------Delete--------

CREATE OR ALTER PROCEDURE UserContributes_DeleteUserContribute

@ContributeID INT

AS

BEGIN
  SET NOCOUNT OFF;


    DELETE FROM UserContributes
   WHERE ContributeID = @ContributeID;

    SELECT @@ROWCOUNT AS RowsAffected;

END
GO


Go 

 --------Is Exists--------

CREATE OR ALTER PROCEDURE UserContributes_CheckUserContributeExists

@ContributeID INT

AS

BEGIN
  SET NOCOUNT ON;


     IF EXISTS( SELECT 1 FROM UserContributes 
     WHERE ContributeID = @ContributeID)
         RETURN 1;
    ELSE
         RETURN 0;

END
GO


Go 

 --------Get Record By--------

CREATE OR ALTER PROCEDURE UserContributes_GetUserContributeByContributeID

@ContributeID INT

AS

BEGIN
  SET NOCOUNT ON;

    SELECT * FROM UserContributes WHERE ContributeID = @ContributeID;

END
GO


Go 

 --------Get All Records--------

CREATE OR ALTER PROCEDURE UserContributes_GetAllUserContributes



AS

BEGIN
  SET NOCOUNT ON;

    SELECT * FROM UserContributes;

END
GO