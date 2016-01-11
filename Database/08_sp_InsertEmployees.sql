--DROP PROCEDURE [dbo].[InsertEmployees]
IF EXISTS (SELECT * FROM sysobjects WHERE id = object_id(N'[dbo].[InsertEmployees]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
BEGIN
	DROP PROCEDURE [dbo].[InsertEmployees]
END
GO

CREATE PROCEDURE [dbo].[InsertEmployees]
(                
	@employees EmployeeType READONLY	   
)
AS 
BEGIN	
	INSERT INTO [Employees] ([Id], [Name], [Email], [Password], [Domain], [DepartmentId]) --[Login], 
	SELECT e.[Id], e.[Name], e.[Email], e.[Password], e.[Domain], e.[DepartmentId] --e.[Login], 
	FROM @employees e
	LEFT OUTER JOIN [Employees] e2 --//TODO: check
	ON e.[Id] = e2.[Id]
	WHERE e2.[Id] IS NULL
END