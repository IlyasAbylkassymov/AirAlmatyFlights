Скрипт для создания БД для начала работы:

USE [airAlmatyFlightsDb]
BEGIN
CREATE TABLE [dbo].[Role] (
    [Id] INT NOT NULL PRIMARY KEY IDENTITY(1,1),
    [Code] NVARCHAR(50) NOT NULL
);

CREATE TABLE [dbo].[User] (
    [Id] INT NOT NULL PRIMARY KEY IDENTITY(1,1),
    [Username] NVARCHAR(80) NOT NULL,
    [Password] NVARCHAR(50) NOT NULL,
    [RoleId] INT NOT NULL,
    CONSTRAINT FK_User_Role FOREIGN KEY ([RoleId]) REFERENCES [dbo].[Role]([Id])
);

	CREATE TABLE [dbo].[Flight] (
    [Id] INT NOT NULL PRIMARY KEY IDENTITY(1,1),
    [Origin] NVARCHAR(50) NOT NULL,
    [Destination] NVARCHAR(50) NOT NULL,
    [Departure] DATETIMEOFFSET NOT NULL,
    [Arrival] DATETIMEOFFSET NOT NULL,
    [Status] INT NOT NULL
);


INSERT INTO  [dbo].[Role] (Code)
VALUES ('User'),('Moderator')

INSERT INTO [dbo].[User] (Username, Password, RoleId)
VALUES ('IlyasAb', 'Aa123', 2)

CREATE LOGIN [airAlmatyApp] WITH PASSWORD = 'Aa123456!'
CREATE USER [airAlmatyApp] FOR LOGIN [airAlmatyApp]
ALTER ROLE db_owner ADD MEMBER [airAlmatyApp]


END
