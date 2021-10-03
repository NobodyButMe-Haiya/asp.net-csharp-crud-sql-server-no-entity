This source code was build using Visual Studio For Mac

This code is example Create , Read , Update Delete using Asp.net .net Core 5 with SQL SERVER using docker . The purpose is to educate simple crud application is pretty easy with .net Core and MySQL can deploy in linux /mac . C# is pretty easy language so developer from PHP can migrate in .

**Software Required**

Visual Studio For Mac
SQL SERVER 2017 Docker https://hub.docker.com/_/microsoft-mssql-server
** this only we try work 
** Do remember SQL SERVER will changed the password 
Tutorial installation docker : https://docs.microsoft.com/en-us/sql/linux/quickstart-install-connect-docker?view=sql-server-linux-2017&preserve-view=true&pivots=cs1-bash

**SQL**
```
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[person](
	[personId] [int] IDENTITY(1,1) NOT NULL,
	[name] [varchar](255) NULL,
	[age] [int] NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[person] ADD PRIMARY KEY CLUSTERED 
(
	[personId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
```



**Nuget Dependency**

Asp.net Core Mvc Razor

System.Data.SqlClient

