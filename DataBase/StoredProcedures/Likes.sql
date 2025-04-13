
USE [ProjectsRepositoryDB];




Go 

 --------INSERT--------

CREATE OR ALTER PROCEDURE Likes_InsertNewLike

@ProjectPostID INT, 
@UserID INT, 
@Date DATETIME, 
@TypeOfLike VARCHAR(50), 
@NewLikeID INT OUTPUT

AS

BEGIN
  SET NOCOUNT ON;

   INSERT INTO Likes (ProjectPostID, UserID, Date, TypeOfLike)
   VALUES (@ProjectPostID, @UserID, @Date, @TypeOfLike);
   SET @NewLikeID = SCOPE_IDENTITY();

END
GO


Go 

 --------Update--------

CREATE OR ALTER PROCEDURE Likes_UpdateLike

@LikeID INT,
@ProjectPostID INT,
@UserID INT,
@Date DATETIME,
@TypeOfLike VARCHAR(50)

AS

BEGIN
  SET NOCOUNT OFF;

   Update Likes 

   SET ProjectPostID = @ProjectPostID, UserID = @UserID, Date = @Date, TypeOfLike = @TypeOfLike
   WHERE LikeID = @LikeID;

    SELECT @@ROWCOUNT AS RowsAffected;

END
GO


Go 

 --------Delete--------

CREATE OR ALTER PROCEDURE Likes_DeleteLike

@LikeID INT

AS

BEGIN
  SET NOCOUNT OFF;


    DELETE FROM Likes
   WHERE LikeID = @LikeID;

    SELECT @@ROWCOUNT AS RowsAffected;

END
GO


Go 

 --------Is Exists--------

CREATE OR ALTER PROCEDURE Likes_CheckLikeExists

@LikeID INT

AS

BEGIN
  SET NOCOUNT ON;


     IF EXISTS( SELECT 1 FROM Likes 
     WHERE LikeID = @LikeID)
         RETURN 1;
    ELSE
         RETURN 0;

END
GO


Go 

 --------Get Record By--------

CREATE OR ALTER PROCEDURE Likes_GetLikeByLikeID

@LikeID INT

AS

BEGIN
  SET NOCOUNT ON;

    SELECT * FROM Likes WHERE LikeID = @LikeID;

END
GO


Go 

 --------Get All Records--------

CREATE OR ALTER PROCEDURE Likes_GetAllLikes



AS

BEGIN
  SET NOCOUNT ON;

    SELECT * FROM Likes;

END
GO