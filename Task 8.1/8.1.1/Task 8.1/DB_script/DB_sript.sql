CREATE DATABASE DAL

USE DAL

CREATE TYPE [dbo].IntTable AS TABLE
(
	Id INT
)

CREATE TABLE [dbo].[AppUser]
(
	 [ID] INT PRIMARY KEY NOT NULL,
	 [UserName] NVARCHAR(255) NOT NULL,
	 [BirthDate] NVARCHAR(10) NOT NULL,
	 [Age] INT NOT NULL
)

CREATE TABLE UserAward
(
	[ID] INT PRIMARY KEY NOT NULL,
	[Title] NVARCHAR(255) NOT NULL,
)

CREATE TABLE UsersAndAwards
(
	[UserId] INT FOREIGN KEY REFERENCES AppUser(ID) ON DELETE CASCADE,
	[AwardId] INT FOREIGN KEY REFERENCES UserAward(ID) ON DELETE CASCADE,
	CONSTRAINT PairsAreUnique UNIQUE (UserId, AwardId)
)

drop table UserIdentity

CREATE TABLE UserIdentity
(
	[UserId] INT UNIQUE FOREIGN KEY REFERENCES AppUser(ID) ON DELETE CASCADE,
	[PasswordHashSum] NVARCHAR(255) NOT NULL 
)

select * from AppUser

CREATE PROCEDURE GetUsers
AS 
BEGIN
	SELECT * FROM AppUser
END

CREATE PROCEDURE GetAwards
AS 
BEGIN
	SELECT * FROM UserAward
END

CREATE PROCEDURE GetAwardedUsers
@AwardId int
AS
BEGIN
	SELECT [dbo].[UsersAndAwards].[UserId] FROM [dbo].[UsersAndAwards] 
	WHERE [dbo].[UsersAndAwards].[AwardId] = @AwardId
END

ALTER PROCEDURE GetUserAwards
@UserId int
AS
BEGIN

	SELECT [dbo].[UsersAndAwards].[AwardId] FROM [dbo].[UsersAndAwards] 
	WHERE [dbo].[UsersAndAwards].[UserId] = @UserID 
END

ALTER PROCEDURE AddUser
@UserId int,
@UserName nvarchar(255),
@UserBirthDate nvarchar(10),
@UserAge int,
@PasswordHashSum nvarchar(255),
@UserAwards AS [dbo].IntTable READONLY
AS
BEGIN
BEGIN TRANSACTION
	INSERT INTO [dbo].[AppUser] VALUES (@UserId, @UserName, @UserBirthDate, @UserAge)

	IF (@@ERROR <> 0)
		ROLLBACK

	EXEC AddConnectedIds 1, @UserId, @UserAwards

	IF (@@ERROR <> 0)
		ROLLBACK
	--Add Identity
	INSERT INTO [dbo].[UserIdentity] VALUES (@UserId, @PasswordHashSum)

	IF (@@ERROR <> 0)
	ROLLBACK

COMMIT
END

CREATE PROCEDURE AddConnectedIds
@EntityType binary,
@EntityId int,
@ConnectedIds AS [dbo].IntTable READONLY
AS
BEGIN
	DECLARE @ConnectedEntityId int

	DECLARE MyCursor CURSOR LOCAL STATIC READ_ONLY FORWARD_ONLY
	FOR SELECT * FROM @ConnectedIds

	OPEN MyCursor
	FETCH NEXT FROM MyCursor INTO @ConnectedEntityId

	WHILE @@FETCH_STATUS = 0
	BEGIN
		EXEC AddUserAndAward @EntityType, @EntityId, @ConnectedEntityId
		FETCH NEXT FROM MyCursor INTO @ConnectedEntityId
	END

	CLOSE MyCursor
END

CREATE PROCEDURE AddUserAndAward
@IsUserFirst binary,
@UserId int,
@AwardId int
AS
BEGIN
	if @IsUserFirst = 1
	BEGIN
		INSERT INTO [dbo].[UsersAndAwards] VALUES (@UserId, @AwardId)
	END
	ELSE
	BEGIN
		INSERT INTO [dbo].[UsersAndAwards] VALUES (@AwardId, @UserId)
	END
END

CREATE PROCEDURE AddAward
@AwardId int,
@AwardTitle nvarchar(255),
@AwardedUsers AS [dbo].IntTable READONLY
AS
BEGIN
BEGIN TRANSACTION
	INSERT INTO [dbo].[UserAward] VALUES (@AwardId, @AwardTitle)

	IF (@@ERROR <> 0)
		ROLLBACK

	EXEC AddConnectedIds 0, @AwardId, @AwardedUsers
	
	IF (@@ERROR <> 0)
		ROLLBACK
COMMIT
END

CREATE PROCEDURE RemoveEntity
@EntityType binary,
@EntityId int
AS
BEGIN
	BEGIN TRANSACTION

	IF (@EntityType = 1)
		DELETE FROM [dbo].[AppUser] WHERE [dbo].[AppUser].[ID] = @EntityId
	ELSE
		DELETE FROM [dbo].[UserAward] WHERE [dbo].[UserAward].[ID] = @EntityId

	IF (@@ERROR <> 0)
		ROLLBACK

	EXEC UpdateIds @EntityType

	IF (@@ERROR <> 0)
		ROLLBACK

	COMMIT
END

ALTER PROCEDURE CheckIdentity
@UserId int,
@PasswordHashSum nvarchar(255)
AS
BEGIN
	IF (SELECT [dbo].[UserIdentity].[PasswordHashSum] FROM [dbo].[UserIdentity]
	WHERE @UserId = [dbo].[UserIdentity].[UserId]) = @PasswordHashSum
		RETURN 1
	ELSE
		RETURN 0
END

ALTER PROCEDURE UpdateUser
@UserId int,
@UserName nvarchar(255),
@UserBirthDate nvarchar(10),
@UserAge int,
@UserAwards AS [dbo].IntTable READONLY
AS
BEGIN
BEGIN TRANSACTION
	UPDATE [dbo].[AppUser] 
	SET [dbo].[AppUser].[Age] = @UserAge, [dbo].[AppUser].[BirthDate] = @UserBirthDate, [dbo].[AppUser].[UserName] = @UserName
	WHERE [dbo].[AppUser].[ID] = @UserId

	IF (@@ERROR <> 0)
		ROLLBACK

	EXEC UpdateConnectedIds @UserId, 1, @UserAwards
	
	IF (@@ERROR <> 0)
		ROLLBACK

	COMMIT
END

CREATE PROCEDURE UpdateIds
@EntityType binary
AS 
BEGIN
	DECLARE @Counter int
	SET @Counter = 1

	DECLARE @CurrentId int

	IF @EntityType = 1
		DECLARE MyCursor CURSOR LOCAL STATIC READ_ONLY FORWARD_ONLY
		FOR SELECT [dbo].[AppUser].[ID] FROM [dbo].[AppUser]
	ELSE
		DECLARE MyCursor CURSOR LOCAL STATIC READ_ONLY FORWARD_ONLY
		FOR SELECT [dbo].[UserAward].[ID] FROM [dbo].[UserAward]

	OPEN MyCursor
	FETCH NEXT FROM MyCursor INTO @CurrentId

	WHILE @@FETCH_STATUS = 0
	BEGIN
		IF (@CurrentId > 0)
		BEGIN
			IF @EntityType = 1
				UPDATE [dbo].[AppUser] SET [dbo].[AppUser].[ID] = @CurrentId
			ELSE
				UPDATE [dbo].[UserAward] SET [dbo].[UserAward].[ID] = @CurrentId
		END
		FETCH NEXT FROM MyCursor INTO @CurrentId
	END

	CLOSE MyCursor
END

CREATE PROCEDURE UpdateConnectedIds
@EntityId int,
@EntityType binary,
@ConnectedIds AS [dbo].IntTable READONLY
AS
BEGIN
BEGIN TRANSACTION
	IF @EntityType = 1
	BEGIN
		DELETE [dbo].[UsersAndAwards] WHERE [dbo].[UsersAndAwards].[UserId] = @EntityId
	END
	ELSE
	BEGIN
		DELETE [dbo].[UsersAndAwards] WHERE [dbo].[UsersAndAwards].[AwardId] = @EntityId
	END

	IF (@@ERROR <> 0)
		ROLLBACK

	EXEC AddConnectedIds @EntityType, @EntityType, @ConnectedIds

	IF (@@ERROR <> 0)
		ROLLBACK

	COMMIT
END

CREATE PROCEDURE UpdateAward
@AwardId int,
@AwardTitle nvarchar(255),
@AwardedUsers AS [dbo].IntTable READONLY
AS
BEGIN
BEGIN TRANSACTION
	UPDATE [dbo].[UserAward] 
	SET [dbo].[UserAward].Title = @AwardTitle
	WHERE [dbo].[UserAward].[ID] = @AwardId

	IF (@@ERROR <> 0)
		ROLLBACK

	EXEC UpdateConnectedIds @AwardId, 0, @AwardedUsers

	IF (@@ERROR <> 0)
		ROLLBACK
	COMMIT
END

