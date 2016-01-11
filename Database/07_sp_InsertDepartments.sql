--DROP PROCEDURE [dbo].[InsertDepartments]
IF EXISTS (SELECT * FROM sysobjects WHERE id = object_id(N'[dbo].[InsertDepartments]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
BEGIN
	DROP PROCEDURE [dbo].[InsertDepartments]
END
GO

CREATE PROCEDURE [dbo].[InsertDepartments]
(                
	@departments DepartmentType READONLY	   
)
AS 
BEGIN	
	INSERT INTO [Departments] (Id, Name)
	SELECT d.[Id], d.[Name]
	FROM @departments d	
	LEFT OUTER JOIN [Departments] d2
	ON d.Id = d2.Id
	WHERE d2.Id IS NULL
END