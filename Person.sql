
USE [ProjectsRepositoryDB];




Go 

 --------INSERT--------

CREATE OR ALTER PROCEDURE Person_InsertNewPerson

@FirstName VARCHAR(100), 
@SecondName VARCHAR(100), 
@ThirdName VARCHAR(100), 
@LastName VARCHAR(100), 
@UniversityID INT, 
@ContactEmail VARCHAR(255), 
@IsEmployee BIT, 
@CreatedAt DATETIME, 
@UpdatedAt DATETIME, 
@Gendor INT, 
@NewPersonID INT OUTPUT

AS

BEGIN
  SET NOCOUNT ON;

   INSERT INTO Person (FirstName, SecondName, ThirdName, LastName, UniversityID, ContactEmail, IsEmployee, CreatedAt, UpdatedAt, Gendor)
   VALUES (@FirstName, @SecondName, @ThirdName, @LastName, @UniversityID, @ContactEmail, @IsEmployee, @CreatedAt, @UpdatedAt, @Gendor);
   SET @NewPersonID = SCOPE_IDENTITY();

END
GO


Go 

 --------Update--------

CREATE OR ALTER PROCEDURE Person_UpdatePerson

@PersonID INT,
@FirstName VARCHAR(100),
@SecondName VARCHAR(100),
@ThirdName VARCHAR(100),
@LastName VARCHAR(100),
@UniversityID INT,
@ContactEmail VARCHAR(255),
@IsEmployee BIT,
@CreatedAt DATETIME,
@UpdatedAt DATETIME,
@Gendor INT

AS

BEGIN
  SET NOCOUNT OFF;

   Update Person 

   SET FirstName = @FirstName, SecondName = @SecondName, ThirdName = @ThirdName, LastName = @LastName, UniversityID = @UniversityID, ContactEmail = @ContactEmail, IsEmployee = @IsEmployee, CreatedAt = @CreatedAt, UpdatedAt = @UpdatedAt, Gendor = @Gendor
   WHERE PersonID = @PersonID;

    SELECT @@ROWCOUNT AS RowsAffected;

END
GO


Go 

 --------Delete--------

CREATE OR ALTER PROCEDURE Person_DeletePerson

@PersonID INT

AS

BEGIN
  SET NOCOUNT OFF;


    DELETE FROM Person
   WHERE PersonID = @PersonID;

    SELECT @@ROWCOUNT AS RowsAffected;

END
GO


Go 

 --------Is Exists--------

CREATE OR ALTER PROCEDURE Person_CheckPersonExists

@PersonID INT

AS

BEGIN
  SET NOCOUNT ON;


     IF EXISTS( SELECT 1 FROM Person 
     WHERE PersonID = @PersonID)
         RETURN 1;
    ELSE
         RETURN 0;

END
GO


Go 

 --------Get Record By--------

CREATE OR ALTER PROCEDURE Person_GetPersonByPersonID

@PersonID INT

AS

BEGIN
  SET NOCOUNT ON;

    SELECT * FROM Person WHERE PersonID = @PersonID;

END
GO


Go 

 --------Get All Records--------

CREATE OR ALTER PROCEDURE Person_GetAllPerson



AS

BEGIN
  SET NOCOUNT ON;

    SELECT * FROM Person;

END
GO