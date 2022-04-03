CREATE DATABASE TelnetDb
GO

USE TelnetDb
GO

CREATE TABLE Users(
	Id			INT				PRIMARY KEY		IDENTITY(1,1),
	Email		NVARCHAR(MAX)	NOT NULL,
	Password	NVARCHAR(MAX)	NOT NULL,
	Role		NVARCHAR(MAX)	NOT NULL,
	NewsMaker	BIT				NOT NULL
)
GO

CREATE TABLE News(
	Id			INT				PRIMARY KEY		IDENTITY(1,1),
	Title		NVARCHAR(MAX)	NOT NULL,
	Text		NVARCHAR(MAX)	NOT NULL,
)
GO

CREATE PROCEDURE sp_GetUser
    @email		NVARCHAR(MAX)
AS
    SELECT TOP 1 * FROM Users
	WHERE Email=@email
GO

CREATE PROCEDURE sp_GetUserById
    @id			INT
AS
    SELECT * FROM Users
	WHERE Id=@id
GO

CREATE PROCEDURE sp_CheckUser
    @email		NVARCHAR(MAX),
	@password	NVARCHAR(MAX)
AS
    SELECT TOP 1 * FROM Users
	WHERE Email=@email AND Password=@password
GO

CREATE PROCEDURE sp_InsertUser
    @email		NVARCHAR(MAX),
	@password	NVARCHAR(MAX),
	@role		NVARCHAR(MAX),
	@newsMaker	BIT
AS
    INSERT INTO Users (Email, Password, Role, NewsMaker)
	VALUES (@email, @password, @role, @newsMaker)
GO

CREATE PROCEDURE sp_GetUsers
AS
    SELECT * FROM Users
GO

CREATE PROCEDURE sp_DeleteUser
    @id		INT
AS
    DELETE FROM Users 
	WHERE Id=@id
GO

CREATE PROCEDURE sp_UpdateUser
    @id				INT,
	@email			NVARCHAR(MAX),
	@password		NVARCHAR(MAX),
	@role			NVARCHAR(MAX),
	@newsMaker		BIT
AS
    UPDATE Users
	SET Email=@email, Password=@password, Role=@role , NewsMaker=@newsMaker
	WHERE Id=@id
GO

CREATE PROCEDURE sp_GetRoles
AS
	SELECT DISTINCT Role FROM Users
GO

CREATE PROCEDURE sp_GetMakes
AS
	SELECT DISTINCT NewsMaker FROM Users
GO


CREATE PROCEDURE sp_InsertNews
	@title		NVARCHAR(MAX),
    @text		NVARCHAR(MAX)
AS
    INSERT INTO News (Title, Text)
	VALUES (@title, @text)
GO

CREATE PROCEDURE sp_GetNewss
AS
    SELECT * FROM News
GO

CREATE PROCEDURE sp_GetNews
	@id				INT
AS
    SELECT * FROM News
	WHERE Id=@id
GO

CREATE PROCEDURE sp_UpdateNews
	@id				INT,
    @title			NVARCHAR(MAX),
	@text			NVARCHAR(MAX)
AS
    UPDATE News
	SET Title=@title, Text=@text
	WHERE Id=@id
GO

CREATE PROCEDURE sp_DeleteNews
    @id				INT
AS
    DELETE FROM News
	WHERE Id=@id
GO


EXEC sp_InsertUser @email='user', @password='user', @role='User', @newsMaker=0
GO

EXEC sp_InsertUser @email='admin', @password='admin', @role='Admin', @newsMaker=1
GO

SELECT * FROM Users
GO