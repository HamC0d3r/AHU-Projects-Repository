
USE [ProjectsRepositoryDB];




Go 

 --------INSERT--------

CREATE OR ALTER PROCEDURE Technologies_InsertNewTechnology

@TechnologyName VARCHAR(255), 
@NewTechnologyID INT OUTPUT

AS

BEGIN
  SET NOCOUNT ON;

   INSERT INTO Technologies (TechnologyName)
   VALUES (@TechnologyName);
   SET @NewTechnologyID = SCOPE_IDENTITY();

END
GO


Go 

 --------Update--------

CREATE OR ALTER PROCEDURE Technologies_UpdateTechnology

@TechnologyID INT,
@TechnologyName VARCHAR(255)

AS

BEGIN
  SET NOCOUNT OFF;

   Update Technologies 

   SET TechnologyName = @TechnologyName
   WHERE TechnologyID = @TechnologyID;

    SELECT @@ROWCOUNT AS RowsAffected;

END
GO


Go 

 --------Delete--------

CREATE OR ALTER PROCEDURE Technologies_DeleteTechnology

@TechnologyID INT

AS

BEGIN
  SET NOCOUNT OFF;


    DELETE FROM Technologies
   WHERE TechnologyID = @TechnologyID;

    SELECT @@ROWCOUNT AS RowsAffected;

END
GO


Go 

 --------Is Exists--------

CREATE OR ALTER PROCEDURE Technologies_CheckTechnologyExists

@TechnologyID INT

AS

BEGIN
  SET NOCOUNT ON;


     IF EXISTS( SELECT 1 FROM Technologies 
     WHERE TechnologyID = @TechnologyID)
         RETURN 1;
    ELSE
         RETURN 0;

END
GO


Go 

 --------Get Record By--------

CREATE OR ALTER PROCEDURE Technologies_GetTechnologyByTechnologyID

@TechnologyID INT

AS

BEGIN
  SET NOCOUNT ON;

    SELECT * FROM Technologies WHERE TechnologyID = @TechnologyID;

END
GO


Go 

 --------Get All Records--------

CREATE OR ALTER PROCEDURE Technologies_GetAllTechnologies



AS

BEGIN
  SET NOCOUNT ON;

    SELECT * FROM Technologies;

END
GO