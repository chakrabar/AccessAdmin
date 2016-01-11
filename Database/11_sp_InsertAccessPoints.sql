--DROP PROCEDURE [dbo].[InsertAccessPoints]
IF EXISTS (SELECT * FROM sysobjects WHERE id = object_id(N'[dbo].[InsertAccessPoints]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
BEGIN
	DROP PROCEDURE [dbo].[InsertAccessPoints]
END
GO

CREATE PROCEDURE [dbo].[InsertAccessPoints]
(                
	@accessPoints AccessPointType READONLY	   
)
AS 
BEGIN	
	INSERT INTO [AccessPoints] ([Id], [Name], [SiteId])
	SELECT e.[Id], e.[Name], e.[SiteId]
	FROM @accessPoints e
	LEFT OUTER JOIN [AccessPoints] a2
	ON e.[Id] = a2.[Id]
	WHERE a2.[Id] IS NULL
END