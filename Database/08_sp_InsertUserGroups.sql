--DROP PROCEDURE [dbo].[InsertUserGroups]
IF EXISTS (SELECT * FROM sysobjects WHERE id = object_id(N'[dbo].[InsertUserGroups]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
BEGIN
	DROP PROCEDURE [dbo].[InsertUserGroups]
END
GO

CREATE PROCEDURE [dbo].[InsertUserGroups]
(                
	@userGroups UserGroupType READONLY	   
)
AS 
BEGIN	
	INSERT INTO [UserGroups] (Id, Name)
	SELECT u1.[Id], u1.[Name]
	FROM @userGroups u1
	LEFT OUTER JOIN [UserGroups] u2
	ON u2.Id = u1.Id
	WHERE u2.Id IS NULL 
END