CREATE TYPE SiteType AS TABLE
(	
	[Id] INT,
	[Name] VARCHAR(500)
);

CREATE TYPE DepartmentType AS TABLE
(	
	[Id] INT,
	[Name] VARCHAR(500)
);

CREATE TYPE EmployeeType AS TABLE
(
	[Id] INT,
	[Name] VARCHAR(500),
	[Email] VARCHAR(200),
	--[Login] INT,
	[Password] VARCHAR(50),
	[Domain] VARCHAR(50),
	[DepartmentId] int
);

CREATE TYPE UserGroupType AS TABLE
(	
	[Id] INT,
	[Name] VARCHAR(500)
);

CREATE TYPE GroupMappingType AS TABLE
(	
	[UserGroupId] INT,
	[EmployeeLogin] INT
);

CREATE TYPE DeptManagerType AS TABLE
(	
	[DepartmentId] INT,
	[ManagerLogin] INT
);

CREATE TYPE AccessPointType AS TABLE
(	
	[Id] INT,
	[Name] VARCHAR(500),
	[SiteId] INT
);