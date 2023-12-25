CREATE TABLE [dbo].[rabota](
	[Id] [int] IDENTITY(1,1) PRIMARY KEY NOT NULL,
	[number] [varchar](30) NOT NULL,
	[family] [varchar](30) NOT NULL,
	[imya] [varchar](30) NOT NULL,
	[otch] [varchar](30) NOT NULL,
	[title_of_ceh] [varchar](30) NOT NULL,
	[type] [varchar](30) NOT NULL,
	[price] [decimal](10, 2) NOT NULL,
	[monday] [varchar](30) NOT NULL,
	[tuesday] [varchar](30) NOT NULL,
	[wednesday] [varchar](30) NOT NULL,
	[thursday] [varchar](30) NOT NULL,
	[friday] [varchar](30) NOT NULL,
	[photo] [varbinary](max) NULL
);

CREATE TABLE [dbo].[User](
	[Id] [int] IDENTITY(1,1) PRIMARY KEY NOT NULL,
	[Name] [varchar](30) NOT NULL,
	[Surname] [varchar](30) NOT NULL,
	[Patronymic] [varchar](30) NOT NULL,
	[Login] [varchar](30) NOT NULL,
	[Password] [varchar](30) NOT NULL,
	[Role] [varchar](30) NOT NULL
);

INSERT INTO [dbo].[User] VALUES(
	'admin',
	'admin',
	'admin',
	'admin',
	'admin',
	'admin'
);

INSERT INTO [dbo].[rabota] VALUES(
	'1',
	'Ivanov',
	'Ivan',
	'Ivanovich',
	'Ceh 1',
	'Num 1',
	12.25,
	'20',
	'15',
	'25',
	'30',
	'10',
	(SELECT BulkColumn FROM OPENROWSET(BULK N'D:\vaza.bmp', Single_Blob) as img)
);