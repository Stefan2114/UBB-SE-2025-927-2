CREATE OR ALTER PROCEDURE sp_userFollowers(@userId BIGINT)
AS
	SELECT [dbo].[Users].*
	FROM
	(
		SELECT US.FollowerId Id
		FROM [dbo].[UserFollowers] AS US
		WHERE US.UserId = @userId
	) UserFollowers
	INNER JOIN 
	[dbo].[Users] ON [dbo].[Users].Id = UserFollowers.Id 

-- sp_groupUsers
