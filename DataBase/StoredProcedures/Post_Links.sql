
USE [ProjectsRepositoryDB];




Go 

 --------INSERT--------

CREATE OR ALTER PROCEDURE Post_Links_InsertNewPost_Link

@Link VARCHAR(500), 
@NewLinkID INT OUTPUT

AS

BEGIN
  SET NOCOUNT ON;

   INSERT INTO Post_Links (Link)
   VALUES (@Link);
   SET @NewLinkID = SCOPE_IDENTITY();

END
GO


Go 

 --------Update--------

CREATE OR ALTER PROCEDURE Post_Links_UpdatePost_Link

@LinkID INT,
@Link VARCHAR(500)

AS

BEGIN
  SET NOCOUNT OFF;

   Update Post_Links 

   SET Link = @Link
   WHERE LinkID = @LinkID;

    SELECT @@ROWCOUNT AS RowsAffected;

END
GO


Go 

 --------Delete--------

CREATE OR ALTER PROCEDURE Post_Links_DeletePost_Link

@LinkID INT

AS

BEGIN
  SET NOCOUNT OFF;


    DELETE FROM Post_Links
   WHERE LinkID = @LinkID;

    SELECT @@ROWCOUNT AS RowsAffected;

END
GO


Go 

 --------Is Exists--------

CREATE OR ALTER PROCEDURE Post_Links_CheckPost_LinkExists

@LinkID INT

AS

BEGIN
  SET NOCOUNT ON;


     IF EXISTS( SELECT 1 FROM Post_Links 
     WHERE LinkID = @LinkID)
         RETURN 1;
    ELSE
         RETURN 0;

END
GO


Go 

 --------Get Record By--------

CREATE OR ALTER PROCEDURE Post_Links_GetPost_LinkByLinkID

@LinkID INT

AS

BEGIN
  SET NOCOUNT ON;

    SELECT * FROM Post_Links WHERE LinkID = @LinkID;

END
GO


Go 

 --------Get All Records--------

CREATE OR ALTER PROCEDURE Post_Links_GetAllPost_Links



AS

BEGIN
  SET NOCOUNT ON;

    SELECT * FROM Post_Links;

END
GO