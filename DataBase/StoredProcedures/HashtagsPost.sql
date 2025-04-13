
USE [ProjectsRepositoryDB];




Go 

 --------INSERT--------

CREATE OR ALTER PROCEDURE HashtagsPost_InsertNewHashtagsPost

@ProjectPostID INT, 
@HashtagID INT, 
@NewHashtagsPostID INT OUTPUT

AS

BEGIN
  SET NOCOUNT ON;

   INSERT INTO HashtagsPost (ProjectPostID, HashtagID)
   VALUES (@ProjectPostID, @HashtagID);
   SET @NewHashtagsPostID = SCOPE_IDENTITY();

END
GO


Go 

 --------Update--------

CREATE OR ALTER PROCEDURE HashtagsPost_UpdateHashtagsPost

@HashtagsPostID INT,
@ProjectPostID INT,
@HashtagID INT

AS

BEGIN
  SET NOCOUNT OFF;

   Update HashtagsPost 

   SET ProjectPostID = @ProjectPostID, HashtagID = @HashtagID
   WHERE HashtagsPostID = @HashtagsPostID;

    SELECT @@ROWCOUNT AS RowsAffected;

END
GO


Go 

 --------Delete--------

CREATE OR ALTER PROCEDURE HashtagsPost_DeleteHashtagsPost

@HashtagsPostID INT

AS

BEGIN
  SET NOCOUNT OFF;


    DELETE FROM HashtagsPost
   WHERE HashtagsPostID = @HashtagsPostID;

    SELECT @@ROWCOUNT AS RowsAffected;

END
GO


Go 

 --------Is Exists--------

CREATE OR ALTER PROCEDURE HashtagsPost_CheckHashtagsPostExists

@HashtagsPostID INT

AS

BEGIN
  SET NOCOUNT ON;


     IF EXISTS( SELECT 1 FROM HashtagsPost 
     WHERE HashtagsPostID = @HashtagsPostID)
         RETURN 1;
    ELSE
         RETURN 0;

END
GO


Go 

 --------Get Record By--------

CREATE OR ALTER PROCEDURE HashtagsPost_GetHashtagsPostByHashtagsPostID

@HashtagsPostID INT

AS

BEGIN
  SET NOCOUNT ON;

    SELECT * FROM HashtagsPost WHERE HashtagsPostID = @HashtagsPostID;

END
GO


Go 

 --------Get All Records--------

CREATE OR ALTER PROCEDURE HashtagsPost_GetAllHashtagsPost



AS

BEGIN
  SET NOCOUNT ON;

    SELECT * FROM HashtagsPost;

END
GO