
/****** Object:  Table [dbo].[Attachments]    Script Date: 15/01/2020 16:54:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Attachments](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[FilePath] [nvarchar](max) NULL,
	[SupplierId] [int] NOT NULL,
 CONSTRAINT [PK_Attachments] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SupplierNotes]    Script Date: 15/01/2020 16:54:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SupplierNotes](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[NoteText] [nvarchar](max) NULL,
	[SupplierId] [int] NOT NULL,
 CONSTRAINT [PK_SupplierNotes] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Suppliers]    Script Date: 15/01/2020 16:54:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Suppliers](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[AccountId] [int] NOT NULL,
	[ContactName] [nvarchar](max) NULL,
	[Company] [nvarchar](max) NULL,
	[Email] [nvarchar](max) NULL,
	[Phone] [nvarchar](max) NULL,
	[Mobile] [nvarchar](max) NULL,
	[Fax] [nvarchar](max) NULL,
	[Website] [nvarchar](max) NULL,
 CONSTRAINT [PK_Suppliers] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Attachments] ON 
GO
INSERT [dbo].[Attachments] ([Id], [FilePath], [SupplierId]) VALUES (1, N'//supplier/234/agreement.txt', 1)
GO
INSERT [dbo].[Attachments] ([Id], [FilePath], [SupplierId]) VALUES (2, N'//supplier/234/agreement.txt', 2)
GO
SET IDENTITY_INSERT [dbo].[Attachments] OFF
GO
SET IDENTITY_INSERT [dbo].[SupplierNotes] ON 
GO
INSERT [dbo].[SupplierNotes] ([Id], [NoteText], [SupplierId]) VALUES (1, N'No weekend deliveries!', 1)
GO
INSERT [dbo].[SupplierNotes] ([Id], [NoteText], [SupplierId]) VALUES (2, N'No weekend deliveries!', 2)
GO
SET IDENTITY_INSERT [dbo].[SupplierNotes] OFF
GO
SET IDENTITY_INSERT [dbo].[Suppliers] ON 
GO
INSERT [dbo].[Suppliers] ([Id], [AccountId], [ContactName], [Company], [Email], [Phone], [Mobile], [Fax], [Website]) VALUES (1, 1987, N'Mr Enzo Berrari', N'Berrari-Ltd', N'Enzo@Berrari.com', N'324232', N'1232432', N'2342342', N'http://www.berrariltd.com')
GO
INSERT [dbo].[Suppliers] ([Id], [AccountId], [ContactName], [Company], [Email], [Phone], [Mobile], [Fax], [Website]) VALUES (2, 1987, N'Mr Malavio Fritorie', N'Renotton-Ltd', N'Malavio.Fritorie@Renotton.com', N'334232', N'4432432', N'2242342', N'www.Renotton-ltd.com')
GO
SET IDENTITY_INSERT [dbo].[Suppliers] OFF
GO
ALTER TABLE [dbo].[Attachments]  WITH CHECK ADD  CONSTRAINT [FK_Attachments_Suppliers_SupplierId] FOREIGN KEY([SupplierId])
REFERENCES [dbo].[Suppliers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Attachments] CHECK CONSTRAINT [FK_Attachments_Suppliers_SupplierId]
GO
