
USE [ProjectsRepositoryDB];




Go 

 --------INSERT--------

CREATE OR ALTER PROCEDURE Specialty_InsertNewSpecialty

@SpecialtyName VARCHAR(255), 
@NewSpecialtyID INT OUTPUT

AS

BEGIN
  SET NOCOUNT ON;

   INSERT INTO Specialty (SpecialtyName)
   VALUES (@SpecialtyName);
   SET @NewSpecialtyID = SCOPE_IDENTITY();

END
GO


Go 

 --------Update--------

CREATE OR ALTER PROCEDURE Specialty_UpdateSpecialty

@SpecialtyID INT,
@SpecialtyName VARCHAR(255)

AS

BEGIN
  SET NOCOUNT OFF;

   Update Specialty 

   SET SpecialtyName = @SpecialtyName
   WHERE SpecialtyID = @SpecialtyID;

    SELECT @@ROWCOUNT AS RowsAffected;

END
GO


Go 

 --------Delete--------

CREATE OR ALTER PROCEDURE Specialty_DeleteSpecialty

@SpecialtyID INT

AS

BEGIN
  SET NOCOUNT OFF;


    DELETE FROM Specialty
   WHERE SpecialtyID = @SpecialtyID;

    SELECT @@ROWCOUNT AS RowsAffected;

END
GO


Go 

 --------Is Exists--------

CREATE OR ALTER PROCEDURE Specialty_CheckSpecialtyExists

@SpecialtyID INT

AS

BEGIN
  SET NOCOUNT ON;


     IF EXISTS( SELECT 1 FROM Specialty 
     WHERE SpecialtyID = @SpecialtyID)
         RETURN 1;
    ELSE
         RETURN 0;

END
GO


Go 

 --------Get Record By--------

CREATE OR ALTER PROCEDURE Specialty_GetSpecialtyBySpecialtyID

@SpecialtyID INT

AS

BEGIN
  SET NOCOUNT ON;

    SELECT * FROM Specialty WHERE SpecialtyID = @SpecialtyID;

END
GO


Go 

 --------Get All Records--------

CREATE OR ALTER PROCEDURE Specialty_GetAllSpecialty



AS

BEGIN
  SET NOCOUNT ON;

    SELECT * FROM Specialty;

END
GO