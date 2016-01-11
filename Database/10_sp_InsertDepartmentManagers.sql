--DROP PROCEDURE [dbo].[InsertDepartmentManagers]
IF EXISTS (SELECT * FROM sysobjects WHERE id = object_id(N'[dbo].[InsertDepartmentManagers]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
BEGIN
	DROP PROCEDURE [dbo].[InsertDepartmentManagers]
END
GO

CREATE PROCEDURE [dbo].[InsertDepartmentManagers]
(                
	@mappings DeptManagerType READONLY	   
)
AS 
BEGIN	
	INSERT INTO [dbo].[DepartmentManagers] ([DepartmentId], [ManagerId])
	SELECT m.[DepartmentId], m.[ManagerLogin]
	FROM @mappings m
	LEFT OUTER JOIN [dbo].[DepartmentManagers] dm
	ON m.[DepartmentId] = dm.DepartmentId
	AND m.[ManagerLogin] = dm.ManagerId
	WHERE dm.Id IS NULL 
END