
USE [ProjectsRepositoryDB];




Go 

 --------INSERT--------

CREATE OR ALTER PROCEDURE University_InsertNewUniversity

@UniversityName VARCHAR(255), 
@SiteLink VARCHAR(255), 
@NewUniversityID INT OUTPUT

AS

BEGIN
  SET NOCOUNT ON;

   INSERT INTO University (UniversityName, SiteLink)
   VALUES (@UniversityName, @SiteLink);
   SET @NewUniversityID = SCOPE_IDENTITY();

END
GO


Go 

 --------Update--------

CREATE OR ALTER PROCEDURE University_UpdateUniversity

@UniversityID INT,
@UniversityName VARCHAR(255),
@SiteLink VARCHAR(255)

AS

BEGIN
  SET NOCOUNT OFF;

   Update University 

   SET UniversityName = @UniversityName, SiteLink = @SiteLink
   WHERE UniversityID = @UniversityID;

    SELECT @@ROWCOUNT AS RowsAffected;

END
GO


Go 

 --------Delete--------

CREATE OR ALTER PROCEDURE University_DeleteUniversity

@UniversityID INT

AS

BEGIN
  SET NOCOUNT OFF;


    DELETE FROM University
   WHERE UniversityID = @UniversityID;

    SELECT @@ROWCOUNT AS RowsAffected;

END
GO


Go 

 --------Is Exists--------

CREATE OR ALTER PROCEDURE University_CheckUniversityExists

@UniversityID INT

AS

BEGIN
  SET NOCOUNT ON;


     IF EXISTS( SELECT 1 FROM University 
     WHERE UniversityID = @UniversityID)
         RETURN 1;
    ELSE
         RETURN 0;

END
GO


Go 

 --------Get Record By--------

CREATE OR ALTER PROCEDURE University_GetUniversityByUniversityID

@UniversityID INT

AS

BEGIN
  SET NOCOUNT ON;

    SELECT * FROM University WHERE UniversityID = @UniversityID;

END
GO


Go 

 --------Get All Records--------

CREATE OR ALTER PROCEDURE University_GetAllUniversity



AS

BEGIN
  SET NOCOUNT ON;

    SELECT * FROM University;

END
GO