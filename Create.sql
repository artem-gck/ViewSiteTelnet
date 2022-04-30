CREATE DATABASE TelnetDb
GO

USE TelnetDb
GO

CREATE TABLE Roles(
	Id			INT				PRIMARY KEY		IDENTITY(1,1),
	Role		NVARCHAR(MAX)	NOT NULL,
)
GO

CREATE TABLE Users(
	Id			INT				PRIMARY KEY		IDENTITY(1,1),
	Email		NVARCHAR(MAX)	NOT NULL,
	Password	NVARCHAR(MAX)	NOT NULL,
	RoleId		INT				NOT NULL,
	NewsMaker	BIT				NOT NULL,
	FOREIGN KEY (RoleId) REFERENCES Roles(Id)
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
    SELECT TOP 1 Users.Id, Email, Password, Role, NewsMaker FROM Users
	INNER JOIN Roles
	ON Roles.Id = Users.RoleId
	WHERE Email=@email
GO

CREATE PROCEDURE sp_GetUserById
    @id			INT
AS
    SELECT Users.Id, Email, Password, Role, NewsMaker FROM Users
	INNER JOIN Roles
	ON Users.RoleId = Roles.Id
	WHERE Users.Id=@id
GO

CREATE PROCEDURE sp_CheckUser
    @email		NVARCHAR(MAX),
	@password	NVARCHAR(MAX)
AS
    SELECT TOP 1 Users.Id, Email, Password, Role, NewsMaker FROM Users
	INNER JOIN Roles
	ON Users.RoleId = Roles.Id
	WHERE Email=@email AND Password=@password
GO

CREATE PROCEDURE sp_InsertUser
    @email		NVARCHAR(MAX),
	@password	NVARCHAR(MAX),
	@role		INT,
	@newsMaker	BIT
AS
    INSERT INTO Users (Email, Password, RoleId, NewsMaker)
	VALUES (@email, @password, @role, @newsMaker)
GO

CREATE PROCEDURE sp_GetRole
	@role		NVARCHAR(MAX)
AS
	SELECT Id FROM Roles
	WHERE Role=@role
GO

CREATE PROCEDURE sp_GetUsers
AS
    SELECT Users.Id, Email, Password, Role, NewsMaker FROM Users
	INNER JOIN Roles
	ON Users.RoleId = Roles.Id
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
	@role			INT,
	@newsMaker		BIT
AS
    UPDATE Users
	SET Email=@email, Password=@password, RoleId=@role , NewsMaker=@newsMaker
	WHERE Id=@id
GO

CREATE PROCEDURE sp_GetRoles
AS
	SELECT Role FROM Roles
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

INSERT INTO Roles (Role)
	VALUES ('Admin')
GO

INSERT INTO Roles (Role)
	VALUES ('User')
GO


EXEC sp_InsertUser @email='user', @password='user', @role=2, @newsMaker=0
GO

EXEC sp_InsertUser @email='admin', @password='admin', @role=1, @newsMaker=1
GO

EXEC sp_InsertNews @title='First massage', @text='Hello world!!!'
GO

EXEC sp_GetUsers
GO

SELECT * FROM Users
GO