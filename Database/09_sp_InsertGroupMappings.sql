--DROP PROCEDURE [dbo].[InsertGroupMappings]
IF EXISTS (SELECT * FROM sysobjects WHERE id = object_id(N'[dbo].[InsertGroupMappings]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
BEGIN
	DROP PROCEDURE [dbo].[InsertGroupMappings]
END
GO

CREATE PROCEDURE [dbo].[InsertGroupMappings]
(                
	@mappings GroupMappingType READONLY	   
)
AS 
BEGIN	--check
	INSERT INTO [dbo].[UserGroupMappings] ([UserGroupId], [EmployeeId])
	SELECT m.[UserGroupId], m.[EmployeeLogin]
	FROM @mappings m
	LEFT OUTER JOIN [dbo].[UserGroupMappings] u2
	ON m.[UserGroupId] = u2.UserGroupId
	AND m.[EmployeeLogin] = u2.EmployeeId
	WHERE u2.Id IS NULL 
END