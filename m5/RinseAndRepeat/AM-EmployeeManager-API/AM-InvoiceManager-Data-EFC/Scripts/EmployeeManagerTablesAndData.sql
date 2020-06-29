/****** Object:  Table [dbo].[Absences]    Script Date: 13/01/2020 16:59:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Absences](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[EmployeeId] [int] NOT NULL,
	[StartDateTime] [datetime2](7) NOT NULL,
	[EndDateTime] [datetime2](7) NOT NULL,
	[Description] [nvarchar](max) NULL,
	[Notes] [nvarchar](max) NULL,
	[Paid] [bit] NOT NULL,
 CONSTRAINT [PK_Absences] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Employees]    Script Date: 13/01/2020 16:59:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Employees](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[AccountId] [int] NOT NULL,
	[Title] [nvarchar](max) NULL,
	[FirstName] [nvarchar](max) NULL,
	[LastName] [nvarchar](max) NULL,
	[DisplayNameAs] [nvarchar](max) NULL,
	[Address] [nvarchar](max) NULL,
	[Notes] [nvarchar](max) NULL,
	[Email] [nvarchar](max) NULL,
	[Phone] [nvarchar](max) NULL,
	[Mobile] [nvarchar](max) NULL,
	[DOB] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_Employees] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Equipments]    Script Date: 13/01/2020 16:59:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Equipments](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[EmployeeId] [int] NOT NULL,
	[LoanStartDateTime] [datetime2](7) NOT NULL,
	[LoanEndDateTime] [datetime2](7) NOT NULL,
	[Reference] [nvarchar](max) NULL,
	[Name] [nvarchar](max) NULL,
	[ExpectedReturnDate] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_Equipments] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Holidays]    Script Date: 13/01/2020 16:59:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Holidays](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[EmployeeId] [int] NOT NULL,
	[StartDateTime] [datetime2](7) NOT NULL,
	[EndDateTime] [datetime2](7) NOT NULL,
	[OnCall] [bit] NOT NULL,
	[OnCallRateMultiplier] [int] NOT NULL,
	[Paid] [bit] NOT NULL,
 CONSTRAINT [PK_Holidays] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Pays]    Script Date: 13/01/2020 16:59:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Pays](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[HourlyRate] [float] NOT NULL,
	[EmployeeId] [int] NOT NULL,
	[DefaultRate] [bit] NOT NULL,
	[StartTime] [datetime2](7) NOT NULL,
	[EndTime] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_Pays] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TaxInformations]    Script Date: 13/01/2020 16:59:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TaxInformations](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[EmployeeId] [int] NOT NULL,
	[TaxCode] [nvarchar](max) NULL,
	[VAT] [bit] NOT NULL,
	[VATRef] [nvarchar](max) NULL,
 CONSTRAINT [PK_TaxInformations] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Trainings]    Script Date: 13/01/2020 16:59:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Trainings](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[EmployeeId] [int] NOT NULL,
	[StartDateTime] [datetime2](7) NOT NULL,
	[EndDateTime] [datetime2](7) NOT NULL,
	[Description] [nvarchar](max) NULL,
	[Name] [nvarchar](max) NULL,
	[Certification] [bit] NOT NULL,
	[CertificationName] [nvarchar](max) NULL,
 CONSTRAINT [PK_Trainings] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Absences] ON 
GO
INSERT [dbo].[Absences] ([Id], [EmployeeId], [StartDateTime], [EndDateTime], [Description], [Notes], [Paid]) VALUES (1, 1, CAST(N'2020-01-02T17:49:18.6259217' AS DateTime2), CAST(N'2020-01-04T17:49:18.6259726' AS DateTime2), N'Sickness', N'Sickness', 1)
GO
INSERT [dbo].[Absences] ([Id], [EmployeeId], [StartDateTime], [EndDateTime], [Description], [Notes], [Paid]) VALUES (2, 2, CAST(N'2020-01-02T17:49:18.6260265' AS DateTime2), CAST(N'2020-01-04T17:49:18.6260289' AS DateTime2), N'Sickness', N'Sickness', 1)
GO
SET IDENTITY_INSERT [dbo].[Absences] OFF
GO
SET IDENTITY_INSERT [dbo].[Employees] ON 
GO
INSERT [dbo].[Employees] ([Id], [AccountId], [Title], [FirstName], [LastName], [DisplayNameAs], [Address], [Notes], [Email], [Phone], [Mobile], [DOB]) VALUES (1, 1987, N'Mr', N'Freddie', N'Dennies', N'EddieD', N'23 Parkhill Road Smethwick B66 53N', N'Paternity due soon!', N'eddie.dennies@AllFinances298.com', N'0121324435', N'072341123441', CAST(N'1975-01-01T00:00:00.0000000' AS DateTime2))
GO
INSERT [dbo].[Employees] ([Id], [AccountId], [Title], [FirstName], [LastName], [DisplayNameAs], [Address], [Notes], [Email], [Phone], [Mobile], [DOB]) VALUES (2, 1987, N'Mr', N'Jon', N'Jordan', N'EddieD', N'23 Windmill Road Smethwick B66 34R', N'Paternity due soon!', N'ron.jordan@AllFinances298.com', N'0121324435', N'072341123441', CAST(N'1975-01-01T00:00:00.0000000' AS DateTime2))
GO
SET IDENTITY_INSERT [dbo].[Employees] OFF
GO
SET IDENTITY_INSERT [dbo].[Equipments] ON 
GO
INSERT [dbo].[Equipments] ([Id], [EmployeeId], [LoanStartDateTime], [LoanEndDateTime], [Reference], [Name], [ExpectedReturnDate]) VALUES (1, 1, CAST(N'2020-01-03T17:49:18.6283638' AS DateTime2), CAST(N'2020-01-23T17:49:18.6284149' AS DateTime2), N'D32432', N'De 23l Laptop', CAST(N'2020-07-21T17:49:18.6283089' AS DateTime2))
GO
INSERT [dbo].[Equipments] ([Id], [EmployeeId], [LoanStartDateTime], [LoanEndDateTime], [Reference], [Name], [ExpectedReturnDate]) VALUES (2, 2, CAST(N'2020-01-03T17:49:18.6285693' AS DateTime2), CAST(N'2020-01-23T17:49:18.6285703' AS DateTime2), N'D32432', N'De 23l Laptop', CAST(N'2020-07-21T17:49:18.6285671' AS DateTime2))
GO
SET IDENTITY_INSERT [dbo].[Equipments] OFF
GO
SET IDENTITY_INSERT [dbo].[Holidays] ON 
GO
INSERT [dbo].[Holidays] ([Id], [EmployeeId], [StartDateTime], [EndDateTime], [OnCall], [OnCallRateMultiplier], [Paid]) VALUES (1, 1, CAST(N'2020-01-05T17:49:18.6252571' AS DateTime2), CAST(N'2020-01-06T17:49:18.6253083' AS DateTime2), 1, 2, 1)
GO
INSERT [dbo].[Holidays] ([Id], [EmployeeId], [StartDateTime], [EndDateTime], [OnCall], [OnCallRateMultiplier], [Paid]) VALUES (2, 2, CAST(N'2020-01-05T17:49:18.6253622' AS DateTime2), CAST(N'2020-01-06T17:49:18.6253644' AS DateTime2), 1, 2, 1)
GO
SET IDENTITY_INSERT [dbo].[Holidays] OFF
GO
SET IDENTITY_INSERT [dbo].[Pays] ON 
GO
INSERT [dbo].[Pays] ([Id], [HourlyRate], [EmployeeId], [DefaultRate], [StartTime], [EndTime]) VALUES (1, 10, 1, 1, CAST(N'2020-01-03T17:49:18.6277605' AS DateTime2), CAST(N'2020-07-21T17:49:18.6278118' AS DateTime2))
GO
INSERT [dbo].[Pays] ([Id], [HourlyRate], [EmployeeId], [DefaultRate], [StartTime], [EndTime]) VALUES (2, 10, 2, 1, CAST(N'2020-01-03T17:49:18.6278648' AS DateTime2), CAST(N'2020-07-21T17:49:18.6278668' AS DateTime2))
GO
SET IDENTITY_INSERT [dbo].[Pays] OFF
GO
SET IDENTITY_INSERT [dbo].[TaxInformations] ON 
GO
INSERT [dbo].[TaxInformations] ([Id], [EmployeeId], [TaxCode], [VAT], [VATRef]) VALUES (1, 1, N'DFE543R323E', 1, N'VD-3R32342E')
GO
INSERT [dbo].[TaxInformations] ([Id], [EmployeeId], [TaxCode], [VAT], [VATRef]) VALUES (2, 2, N'DFE543R323E', 1, N'VD-3R32342E')
GO
SET IDENTITY_INSERT [dbo].[TaxInformations] OFF
GO
SET IDENTITY_INSERT [dbo].[Trainings] ON 
GO
INSERT [dbo].[Trainings] ([Id], [EmployeeId], [StartDateTime], [EndDateTime], [Description], [Name], [Certification], [CertificationName]) VALUES (1, 1, CAST(N'2019-09-25T17:49:18.6266622' AS DateTime2), CAST(N'2020-04-12T17:49:18.6267133' AS DateTime2), N'SQL Server MCP', N'SQL MCP Training', 1, N'MCP')
GO
INSERT [dbo].[Trainings] ([Id], [EmployeeId], [StartDateTime], [EndDateTime], [Description], [Name], [Certification], [CertificationName]) VALUES (2, 2, CAST(N'2019-09-25T17:49:18.6267678' AS DateTime2), CAST(N'2020-04-12T17:49:18.6267698' AS DateTime2), N'SQL Server MCP', N'SQL MCP Training', 1, N'MCP')
GO
SET IDENTITY_INSERT [dbo].[Trainings] OFF
GO
ALTER TABLE [dbo].[Absences]  WITH CHECK ADD  CONSTRAINT [FK_Absences_Employees_EmployeeId] FOREIGN KEY([EmployeeId])
REFERENCES [dbo].[Employees] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Absences] CHECK CONSTRAINT [FK_Absences_Employees_EmployeeId]
GO
ALTER TABLE [dbo].[Employees]  WITH CHECK ADD  CONSTRAINT [FK_Employees_Accounts_AccountId] FOREIGN KEY([AccountId])
REFERENCES [dbo].[Accounts] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Employees] CHECK CONSTRAINT [FK_Employees_Accounts_AccountId]
GO
ALTER TABLE [dbo].[Equipments]  WITH CHECK ADD  CONSTRAINT [FK_Equipments_Employees_EmployeeId] FOREIGN KEY([EmployeeId])
REFERENCES [dbo].[Employees] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Equipments] CHECK CONSTRAINT [FK_Equipments_Employees_EmployeeId]
GO
ALTER TABLE [dbo].[Holidays]  WITH CHECK ADD  CONSTRAINT [FK_Holidays_Employees_EmployeeId] FOREIGN KEY([EmployeeId])
REFERENCES [dbo].[Employees] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Holidays] CHECK CONSTRAINT [FK_Holidays_Employees_EmployeeId]
GO
ALTER TABLE [dbo].[Pays]  WITH CHECK ADD  CONSTRAINT [FK_Pays_Employees_EmployeeId] FOREIGN KEY([EmployeeId])
REFERENCES [dbo].[Employees] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Pays] CHECK CONSTRAINT [FK_Pays_Employees_EmployeeId]
GO
ALTER TABLE [dbo].[TaxInformations]  WITH CHECK ADD  CONSTRAINT [FK_TaxInformations_Employees_EmployeeId] FOREIGN KEY([EmployeeId])
REFERENCES [dbo].[Employees] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[TaxInformations] CHECK CONSTRAINT [FK_TaxInformations_Employees_EmployeeId]
GO
ALTER TABLE [dbo].[Trainings]  WITH CHECK ADD  CONSTRAINT [FK_Trainings_Employees_EmployeeId] FOREIGN KEY([EmployeeId])
REFERENCES [dbo].[Employees] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Trainings] CHECK CONSTRAINT [FK_Trainings_Employees_EmployeeId]
GO
