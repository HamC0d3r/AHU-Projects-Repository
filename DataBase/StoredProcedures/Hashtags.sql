
USE [ProjectsRepositoryDB];




Go 

 --------INSERT--------

CREATE OR ALTER PROCEDURE Hashtags_InsertNewHashtag

@HashtagName VARCHAR(100), 
@PostsCount INT, 
@NewHashtagID INT OUTPUT

AS

BEGIN
  SET NOCOUNT ON;

   INSERT INTO Hashtags (HashtagName, PostsCount)
   VALUES (@HashtagName, @PostsCount);
   SET @NewHashtagID = SCOPE_IDENTITY();

END
GO


Go 

 --------Update--------

CREATE OR ALTER PROCEDURE Hashtags_UpdateHashtag

@HashtagID INT,
@HashtagName VARCHAR(100),
@PostsCount INT

AS

BEGIN
  SET NOCOUNT OFF;

   Update Hashtags 

   SET HashtagName = @HashtagName, PostsCount = @PostsCount
   WHERE HashtagID = @HashtagID;

    SELECT @@ROWCOUNT AS RowsAffected;

END
GO


Go 

 --------Delete--------

CREATE OR ALTER PROCEDURE Hashtags_DeleteHashtag

@HashtagID INT

AS

BEGIN
  SET NOCOUNT OFF;


    DELETE FROM Hashtags
   WHERE HashtagID = @HashtagID;

    SELECT @@ROWCOUNT AS RowsAffected;

END
GO


Go 

 --------Is Exists--------

CREATE OR ALTER PROCEDURE Hashtags_CheckHashtagExists

@HashtagID INT

AS

BEGIN
  SET NOCOUNT ON;


     IF EXISTS( SELECT 1 FROM Hashtags 
     WHERE HashtagID = @HashtagID)
         RETURN 1;
    ELSE
         RETURN 0;

END
GO


Go 

 --------Get Record By--------

CREATE OR ALTER PROCEDURE Hashtags_GetHashtagByHashtagID

@HashtagID INT

AS

BEGIN
  SET NOCOUNT ON;

    SELECT * FROM Hashtags WHERE HashtagID = @HashtagID;

END
GO


Go 

 --------Get All Records--------

CREATE OR ALTER PROCEDURE Hashtags_GetAllHashtags



AS

BEGIN
  SET NOCOUNT ON;

    SELECT * FROM Hashtags;

END
GO