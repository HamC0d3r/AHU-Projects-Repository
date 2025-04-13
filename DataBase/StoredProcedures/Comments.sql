
USE [ProjectsRepositoryDB];




Go 

 --------INSERT--------

CREATE OR ALTER PROCEDURE Comments_InsertNewComment

@ProjectPostID INT, 
@UserID INT, 
@Date DATETIME, 
@ImagePath VARCHAR(255), 
@NewCommentID INT OUTPUT

AS

BEGIN
  SET NOCOUNT ON;

   INSERT INTO Comments (ProjectPostID, UserID, Date, ImagePath)
   VALUES (@ProjectPostID, @UserID, @Date, @ImagePath);
   SET @NewCommentID = SCOPE_IDENTITY();

END
GO


Go 

 --------Update--------

CREATE OR ALTER PROCEDURE Comments_UpdateComment

@CommentID INT,
@ProjectPostID INT,
@UserID INT,
@Date DATETIME,
@ImagePath VARCHAR(255)

AS

BEGIN
  SET NOCOUNT OFF;

   Update Comments 

   SET ProjectPostID = @ProjectPostID, UserID = @UserID, Date = @Date, ImagePath = @ImagePath
   WHERE CommentID = @CommentID;

    SELECT @@ROWCOUNT AS RowsAffected;

END
GO


Go 

 --------Delete--------

CREATE OR ALTER PROCEDURE Comments_DeleteComment

@CommentID INT

AS

BEGIN
  SET NOCOUNT OFF;


    DELETE FROM Comments
   WHERE CommentID = @CommentID;

    SELECT @@ROWCOUNT AS RowsAffected;

END
GO


Go 

 --------Is Exists--------

CREATE OR ALTER PROCEDURE Comments_CheckCommentExists

@CommentID INT

AS

BEGIN
  SET NOCOUNT ON;


     IF EXISTS( SELECT 1 FROM Comments 
     WHERE CommentID = @CommentID)
         RETURN 1;
    ELSE
         RETURN 0;

END
GO


Go 

 --------Get Record By--------

CREATE OR ALTER PROCEDURE Comments_GetCommentByCommentID

@CommentID INT

AS

BEGIN
  SET NOCOUNT ON;

    SELECT * FROM Comments WHERE CommentID = @CommentID;

END
GO


Go 

 --------Get All Records--------

CREATE OR ALTER PROCEDURE Comments_GetAllComments



AS

BEGIN
  SET NOCOUNT ON;

    SELECT * FROM Comments;

END
GO