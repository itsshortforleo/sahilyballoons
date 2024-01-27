-- Create new localdb instance in cmd prompt:
-- C:\dev>sqllocaldb stop mssqllocaldb
-- LocalDB instance "mssqllocaldb" stopped.

-- C:\dev>sqllocaldb delete mssqllocaldb
-- LocalDB instance "mssqllocaldb" deleted.

-- C:\dev>sqllocaldb start "MSSQLLocalDB"
-- LocalDB instance "MSSQLLocalDB" started.
-- C:\dev>


-- Create a new database called 'SahilyBalloonsWebsiteDB'
-- Connect to the 'master' database to run this snippet
USE master
GO
-- Create the new database if it does not exist already
IF NOT EXISTS (
    SELECT name
        FROM sys.databases
        WHERE name = N'SahilyBalloonsWebsiteDB'
)
CREATE DATABASE SahilyBalloonsWebsiteDB
GO

-- Create a new table called 'users' in schema 'dbo'
-- Drop the table if it already exists
IF OBJECT_ID('dbo.users', 'U') IS NOT NULL
DROP TABLE dbo.users
GO
-- Create the table in the specified schema
CREATE TABLE dbo.users
(
    users_id INT NOT NULL PRIMARY KEY IDENTITY(1,1), -- primary key column
    created_on DATETIME,
    created_by INT,
    first_name [NVARCHAR](200),
    last_name [NVARCHAR](200),
    full_name [NVARCHAR](200)
    -- specify more columns here
);
GO

-- Create a new table called 'post' in schema 'dbo'
-- Drop the table if it already exists
IF OBJECT_ID('dbo.post', 'U') IS NOT NULL
DROP TABLE dbo.post
GO
-- Create the table in the specified schema
CREATE TABLE dbo.post
(
    post_id INT NOT NULL PRIMARY KEY IDENTITY(1,1), -- primary key column
    created_on DATETIME,
    created_by INT,
    title [NVARCHAR](MAX),
    content [NVARCHAR](MAX),
    is_post_published BIT NOT NULL
);
GO

-- Create a new table called 'comment' in schema 'dbo'
-- Drop the table if it already exists
IF OBJECT_ID('dbo.comment', 'U') IS NOT NULL
DROP TABLE dbo.comment
GO
-- Create the table in the specified schema
CREATE TABLE dbo.comment
(
    comment_id INT NOT NULL PRIMARY KEY IDENTITY(1,1), -- primary key column
	post_parent_id INT FOREIGN KEY REFERENCES post(post_id),
	comment_parent_id INT FOREIGN KEY REFERENCES comment(comment_id),
    created_on DATETIME,
    created_by INT,
    title [NVARCHAR](MAX),
    content [NVARCHAR](MAX),
    is_comment_approved BIT NOT NULL
);
GO


-- --Seed some data
USE [SahilyBalloonsWebsiteDB]
GO
INSERT INTO [dbo].[users]
           ([created_on]
           ,[created_by]
           ,[first_name]
           ,[last_name]
           ,[full_name])
     VALUES
           (GETDATE()
           ,0
           ,N'Leonardo'
           ,N'Lopez'
           ,N'Leonardo Lopez')

INSERT INTO [dbo].[post]
           ([created_on]
           ,[created_by]
           ,[title]
           ,[content]
           ,[is_post_published])
     VALUES
           (GETDATE()
           ,0
		   ,N'Contact'
           ,N'You can contact me by email, or by leaving a reply below.'
           ,1)

INSERT INTO [dbo].[comment]
           ([post_parent_id]
           ,[comment_parent_id]
           ,[created_on]
           ,[created_by]
           ,[title]
           ,[content]
           ,[is_comment_approved])
     VALUES
           (1
           ,NULL
           ,GETDATE()
           ,0
           ,N'My thanks'
           ,N'VRShutdown is a simple tool, but quite useful, thank you.'
           ,1)

INSERT INTO [dbo].[comment]
           ([post_parent_id]
           ,[comment_parent_id]
           ,[created_on]
           ,[created_by]
           ,[title]
           ,[content]
           ,[is_comment_approved])
     VALUES
           (1
           ,NULL
           ,GETDATE()
           ,0
           ,N'Hello'
           ,N'Rock on, my friend.'
           ,1)

INSERT INTO [dbo].[comment]
           ([post_parent_id]
           ,[comment_parent_id]
           ,[created_on]
           ,[created_by]
           ,[title]
           ,[content]
           ,[is_comment_approved])
     VALUES
           (1
           ,NULL
           ,GETDATE()
           ,0
           ,N'bleh'
           ,N'Cool I guess'
           ,1)

INSERT INTO [dbo].[comment]
           ([post_parent_id]
           ,[comment_parent_id]
           ,[created_on]
           ,[created_by]
           ,[title]
           ,[content]
           ,[is_comment_approved])
     VALUES
           (1
           ,NULL
           ,GETDATE()
           ,0
           ,N'pr0n'
           ,N'questionablelinkXXX.com'
           ,0)

GO
-- Scaffold-DbContext Name=ConnectionStrings.SahilyBalloonsWebsiteDB Microsoft.EntityFrameworkCore.SqlServer -outputdir Models -context SahilyBalloonsWebsiteDbContext -contextdir ./ -DataAnnotations -Force

-- Leo example v2 with partial class support:
-- Scaffold-DbContext Name=ConnectionStrings:SahilyBalloonsWebsiteDB Microsoft.EntityFrameworkCore.SqlServer -outputdir Models/Generated -context SahilyBalloonsWebsiteDbContext -ContextNamespace SahilyBalloons.Data -Namespace SahilyBalloons.Data.Models -contextdir ./ -DataAnnotations -Force