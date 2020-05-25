/****** Object:  Table [dbo].[Tick]    Script Date: 5/25/2020 11:12:01 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Tick](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[MeasurementId] [int] NOT NULL,
	[Value] [decimal](18, 5) NOT NULL,
	[Receiver] [nvarchar](264) NOT NULL,
	[Version] [nvarchar](32) NOT NULL,
	[PublishedDateUtc] [datetime2](5) NOT NULL,
	[CreatedDateUtc] [datetime2](5) NOT NULL,
 CONSTRAINT [PK_Tick] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Tick] ADD  CONSTRAINT [DF_Tick_CreatedDateUtc]  DEFAULT (getutcdate()) FOR [CreatedDateUtc]
GO


