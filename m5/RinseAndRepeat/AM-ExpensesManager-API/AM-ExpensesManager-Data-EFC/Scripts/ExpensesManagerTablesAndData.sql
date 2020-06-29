
/****** Object:  Table [dbo].[ExpenseLines]    Script Date: 15/01/2020 13:30:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ExpenseLines](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ExpenseId] [int] NOT NULL,
	[ServiceType] [int] NOT NULL,
	[Description] [nvarchar](max) NULL,
	[Amount] [float] NOT NULL,
	[VAT] [float] NOT NULL,
 CONSTRAINT [PK_ExpenseLines] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Expenses]    Script Date: 15/01/2020 13:30:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Expenses](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[PayeeName] [nvarchar](max) NULL,
	[CustomerId] [int] NULL,
	[SupplierId] [int] NULL,
	[EmployeeId] [int] NOT NULL,
	[PaymentDate] [datetime2](7) NOT NULL,
	[PaymentMethod] [int] NOT NULL,
	[BankAccountId] [int] NOT NULL,
	[Reference] [nvarchar](max) NULL,
	[Notes] [nvarchar](max) NULL,
 CONSTRAINT [PK_Expenses] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[ExpenseLines] ON 
GO
INSERT [dbo].[ExpenseLines] ([Id], [ExpenseId], [ServiceType], [Description], [Amount], [VAT]) VALUES (1, 1, 1, N'Regular', 200, 23)
GO
INSERT [dbo].[ExpenseLines] ([Id], [ExpenseId], [ServiceType], [Description], [Amount], [VAT]) VALUES (2, 2, 1, N'Regular', 200, 23)
GO
SET IDENTITY_INSERT [dbo].[ExpenseLines] OFF
GO
SET IDENTITY_INSERT [dbo].[Expenses] ON 
GO
INSERT [dbo].[Expenses] ([Id], [PayeeName], [CustomerId], [SupplierId], [EmployeeId], [PaymentDate], [PaymentMethod], [BankAccountId], [Reference], [Notes]) VALUES (1, N'Art Tech', 1123, NULL, 1, CAST(N'2020-01-01T17:49:18.6291608' AS DateTime2), 1, 1987, N'TESDSFD-324', N'One-off')
GO
INSERT [dbo].[Expenses] ([Id], [PayeeName], [CustomerId], [SupplierId], [EmployeeId], [PaymentDate], [PaymentMethod], [BankAccountId], [Reference], [Notes]) VALUES (2, N'Art Tech', 2123, NULL, 1, CAST(N'2020-01-01T17:49:18.6293709' AS DateTime2), 1, 1987, N'G43534-324', N'One-off')
GO
SET IDENTITY_INSERT [dbo].[Expenses] OFF
GO
ALTER TABLE [dbo].[ExpenseLines]  WITH CHECK ADD  CONSTRAINT [FK_ExpenseLines_Expenses_ExpenseId] FOREIGN KEY([ExpenseId])
REFERENCES [dbo].[Expenses] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ExpenseLines] CHECK CONSTRAINT [FK_ExpenseLines_Expenses_ExpenseId]
GO

