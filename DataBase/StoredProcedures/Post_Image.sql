
USE [ProjectsRepositoryDB];




Go 

 --------INSERT--------

CREATE OR ALTER PROCEDURE Post_Image_InsertNewPost_Image

@ImagePath VARCHAR(500), 
@NewImagePostID INT OUTPUT

AS

BEGIN
  SET NOCOUNT ON;

   INSERT INTO Post_Image (ImagePath)
   VALUES (@ImagePath);
   SET @NewImagePostID = SCOPE_IDENTITY();

END
GO


Go 

 --------Update--------

CREATE OR ALTER PROCEDURE Post_Image_UpdatePost_Image

@ImagePostID INT,
@ImagePath VARCHAR(500)

AS

BEGIN
  SET NOCOUNT OFF;

   Update Post_Image 

   SET ImagePath = @ImagePath
   WHERE ImagePostID = @ImagePostID;

    SELECT @@ROWCOUNT AS RowsAffected;

END
GO


Go 

 --------Delete--------

CREATE OR ALTER PROCEDURE Post_Image_DeletePost_Image

@ImagePostID INT

AS

BEGIN
  SET NOCOUNT OFF;


    DELETE FROM Post_Image
   WHERE ImagePostID = @ImagePostID;

    SELECT @@ROWCOUNT AS RowsAffected;

END
GO


Go 

 --------Is Exists--------

CREATE OR ALTER PROCEDURE Post_Image_CheckPost_ImageExists

@ImagePostID INT

AS

BEGIN
  SET NOCOUNT ON;


     IF EXISTS( SELECT 1 FROM Post_Image 
     WHERE ImagePostID = @ImagePostID)
         RETURN 1;
    ELSE
         RETURN 0;

END
GO


Go 

 --------Get Record By--------

CREATE OR ALTER PROCEDURE Post_Image_GetPost_ImageByImagePostID

@ImagePostID INT

AS

BEGIN
  SET NOCOUNT ON;

    SELECT * FROM Post_Image WHERE ImagePostID = @ImagePostID;

END
GO


Go 

 --------Get All Records--------

CREATE OR ALTER PROCEDURE Post_Image_GetAllPost_Image



AS

BEGIN
  SET NOCOUNT ON;

    SELECT * FROM Post_Image;

END
GO