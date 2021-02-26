USE [obmen_rn]
GO

/****** Object:  Table [dbo].[LoadPackage]    Script Date: 26.02.21 20:25:11 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[LoadPackage](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[dbname] [nvarchar](128) NOT NULL,
	[TableName] [nchar](10) NOT NULL,
	[SQLScript] [nvarchar](max) NOT NULL,
	[PrKey] [nvarchar](50) NOT NULL,
	[PowerOn] [bit] NOT NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO


