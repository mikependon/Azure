/****** Object:  Table [dbo].[Tick]    Script Date: 5/23/2020 2:07:37 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Tick](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[MeasurementId] [int] NOT NULL,
	[Value] [int] NOT NULL,
	[CreatedDateUtc] [datetime2](5) NOT NULL,
 CONSTRAINT [PK_Tick] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Tick] ADD  CONSTRAINT [DF_Tick_CreatedDateUtc]  DEFAULT (getutcdate()) FOR [CreatedDateUtc]
GO


