--DROP PROCEDURE [dbo].[InsertSites]
IF EXISTS (SELECT * FROM sysobjects WHERE id = object_id(N'[dbo].[InsertSites]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
BEGIN
	DROP PROCEDURE [dbo].[InsertSites]
END
GO

CREATE PROCEDURE [dbo].[InsertSites]
(                
	@sites SiteType READONLY	   
)
AS 
BEGIN	
	INSERT INTO [Sites] (Id, Name)
	SELECT s1.[Id], s1.[Name]
	FROM @sites s1	
	LEFT OUTER JOIN [Sites] s2
	ON s1.Id = s2.Id
	WHERE s2.Id IS NULL
END