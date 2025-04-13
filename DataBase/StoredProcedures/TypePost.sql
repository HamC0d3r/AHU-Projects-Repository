
USE [ProjectsRepositoryDB];




Go 

 --------INSERT--------

CREATE OR ALTER PROCEDURE TypePost_InsertNewTypePost

@TypePostName VARCHAR(100), 
@NewTypePostID INT OUTPUT

AS

BEGIN
  SET NOCOUNT ON;

   INSERT INTO TypePost (TypePostName)
   VALUES (@TypePostName);
   SET @NewTypePostID = SCOPE_IDENTITY();

END
GO


Go 

 --------Update--------

CREATE OR ALTER PROCEDURE TypePost_UpdateTypePost

@TypePostID INT,
@TypePostName VARCHAR(100)

AS

BEGIN
  SET NOCOUNT OFF;

   Update TypePost 

   SET TypePostName = @TypePostName
   WHERE TypePostID = @TypePostID;

    SELECT @@ROWCOUNT AS RowsAffected;

END
GO


Go 

 --------Delete--------

CREATE OR ALTER PROCEDURE TypePost_DeleteTypePost

@TypePostID INT

AS

BEGIN
  SET NOCOUNT OFF;


    DELETE FROM TypePost
   WHERE TypePostID = @TypePostID;

    SELECT @@ROWCOUNT AS RowsAffected;

END
GO


Go 

 --------Is Exists--------

CREATE OR ALTER PROCEDURE TypePost_CheckTypePostExists

@TypePostID INT

AS

BEGIN
  SET NOCOUNT ON;


     IF EXISTS( SELECT 1 FROM TypePost 
     WHERE TypePostID = @TypePostID)
         RETURN 1;
    ELSE
         RETURN 0;

END
GO


Go 

 --------Get Record By--------

CREATE OR ALTER PROCEDURE TypePost_GetTypePostByTypePostID

@TypePostID INT

AS

BEGIN
  SET NOCOUNT ON;

    SELECT * FROM TypePost WHERE TypePostID = @TypePostID;

END
GO


Go 

 --------Get All Records--------

CREATE OR ALTER PROCEDURE TypePost_GetAllTypePost



AS

BEGIN
  SET NOCOUNT ON;

    SELECT * FROM TypePost;

END
GO