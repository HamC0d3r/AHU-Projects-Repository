
USE [ProjectsRepositoryDB];




Go 

 --------INSERT--------

CREATE OR ALTER PROCEDURE ProjectPost_InsertNewProjectPost

@Title VARCHAR(255), 
@Body TEXT, 
@ImagePostID INT, 
@UserID INT, 
@LinkID INT, 
@TypePostID INT, 
@CommentsNum INT, 
@LikesNum INT, 
@ContributorsNum INT, 
@CreatedAt DATETIME, 
@UpdatedAt DATETIME, 
@NewProjectPostID INT OUTPUT

AS

BEGIN
  SET NOCOUNT ON;

   INSERT INTO ProjectPost (Title, Body, ImagePostID, UserID, LinkID, TypePostID, CommentsNum, LikesNum, ContributorsNum, CreatedAt, UpdatedAt)
   VALUES (@Title, @Body, @ImagePostID, @UserID, @LinkID, @TypePostID, @CommentsNum, @LikesNum, @ContributorsNum, @CreatedAt, @UpdatedAt);
   SET @NewProjectPostID = SCOPE_IDENTITY();

END
GO


Go 

 --------Update--------

CREATE OR ALTER PROCEDURE ProjectPost_UpdateProjectPost

@ProjectPostID INT,
@Title VARCHAR(255),
@Body TEXT,
@ImagePostID INT,
@UserID INT,
@LinkID INT,
@TypePostID INT,
@CommentsNum INT,
@LikesNum INT,
@ContributorsNum INT,
@CreatedAt DATETIME,
@UpdatedAt DATETIME

AS

BEGIN
  SET NOCOUNT OFF;

   Update ProjectPost 

   SET Title = @Title, Body = @Body, ImagePostID = @ImagePostID, UserID = @UserID, LinkID = @LinkID, TypePostID = @TypePostID, CommentsNum = @CommentsNum, LikesNum = @LikesNum, ContributorsNum = @ContributorsNum, CreatedAt = @CreatedAt, UpdatedAt = @UpdatedAt
   WHERE ProjectPostID = @ProjectPostID;

    SELECT @@ROWCOUNT AS RowsAffected;

END
GO


Go 

 --------Delete--------

CREATE OR ALTER PROCEDURE ProjectPost_DeleteProjectPost

@ProjectPostID INT

AS

BEGIN
  SET NOCOUNT OFF;


    DELETE FROM ProjectPost
   WHERE ProjectPostID = @ProjectPostID;

    SELECT @@ROWCOUNT AS RowsAffected;

END
GO


Go 

 --------Is Exists--------

CREATE OR ALTER PROCEDURE ProjectPost_CheckProjectPostExists

@ProjectPostID INT

AS

BEGIN
  SET NOCOUNT ON;


     IF EXISTS( SELECT 1 FROM ProjectPost 
     WHERE ProjectPostID = @ProjectPostID)
         RETURN 1;
    ELSE
         RETURN 0;

END
GO


Go 

 --------Get Record By--------

CREATE OR ALTER PROCEDURE ProjectPost_GetProjectPostByProjectPostID

@ProjectPostID INT

AS

BEGIN
  SET NOCOUNT ON;

    SELECT * FROM ProjectPost WHERE ProjectPostID = @ProjectPostID;

END
GO


Go 

 --------Get All Records--------

CREATE OR ALTER PROCEDURE ProjectPost_GetAllProjectPost



AS

BEGIN
  SET NOCOUNT ON;

    SELECT * FROM ProjectPost;

END
GO