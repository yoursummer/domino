USE [Domino]

GO





IF OBJECT_ID (N'spSignInUser', N'P') IS NOT NULL
DROP PROC spSignInUser

GO

CREATE PROC spSignInUser
	@UsernameOrEmail nvarchar(150),
	@PasswordHash varchar(1024)
AS
	SELECT *
	FROM [User]
	WHERE
		Hidden = 0 AND
		(Email = @UsernameOrEmail OR Name = @UsernameOrEmail) AND
		PasswordHash = @PasswordHash