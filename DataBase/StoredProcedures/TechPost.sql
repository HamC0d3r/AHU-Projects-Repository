
USE [ProjectsRepositoryDB];




Go 

 --------INSERT--------

CREATE OR ALTER PROCEDURE TechPost_InsertNewTechPost

@ProjectPostID INT, 
@TechnologyID INT, 
@NewTechPostID INT OUTPUT

AS

BEGIN
  SET NOCOUNT ON;

   INSERT INTO TechPost (ProjectPostID, TechnologyID)
   VALUES (@ProjectPostID, @TechnologyID);
   SET @NewTechPostID = SCOPE_IDENTITY();

END
GO


Go 

 --------Update--------

CREATE OR ALTER PROCEDURE TechPost_UpdateTechPost

@TechPostID INT,
@ProjectPostID INT,
@TechnologyID INT

AS

BEGIN
  SET NOCOUNT OFF;

   Update TechPost 

   SET ProjectPostID = @ProjectPostID, TechnologyID = @TechnologyID
   WHERE TechPostID = @TechPostID;

    SELECT @@ROWCOUNT AS RowsAffected;

END
GO


Go 

 --------Delete--------

CREATE OR ALTER PROCEDURE TechPost_DeleteTechPost

@TechPostID INT

AS

BEGIN
  SET NOCOUNT OFF;


    DELETE FROM TechPost
   WHERE TechPostID = @TechPostID;

    SELECT @@ROWCOUNT AS RowsAffected;

END
GO


Go 

 --------Is Exists--------

CREATE OR ALTER PROCEDURE TechPost_CheckTechPostExists

@TechPostID INT

AS

BEGIN
  SET NOCOUNT ON;


     IF EXISTS( SELECT 1 FROM TechPost 
     WHERE TechPostID = @TechPostID)
         RETURN 1;
    ELSE
         RETURN 0;

END
GO


Go 

 --------Get Record By--------

CREATE OR ALTER PROCEDURE TechPost_GetTechPostByTechPostID

@TechPostID INT

AS

BEGIN
  SET NOCOUNT ON;

    SELECT * FROM TechPost WHERE TechPostID = @TechPostID;

END
GO


Go 

 --------Get All Records--------

CREATE OR ALTER PROCEDURE TechPost_GetAllTechPost



AS

BEGIN
  SET NOCOUNT ON;

    SELECT * FROM TechPost;

END
GO