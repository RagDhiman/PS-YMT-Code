
/****** Object:  Table [dbo].[CreditNotes]    Script Date: 12/01/2020 16:07:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CreditNotes](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[InvoiceId] [int] NULL,
	[CustomerId] [int] NULL,
	[CreditNoteDate] [datetime2](7) NOT NULL,
	[Message] [nvarchar](max) NULL,
 CONSTRAINT [PK_CreditNotes] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Credits]    Script Date: 12/01/2020 16:07:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Credits](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[InvoiceId] [int] NOT NULL,
	[CreditDate] [datetime2](7) NOT NULL,
	[CreditAmount] [float] NOT NULL,
	[ProductCredit] [int] NOT NULL,
	[CustomerName] [nvarchar](max) NULL,
	[AccountNo] [nvarchar](max) NULL,
	[SortCode] [nvarchar](max) NULL,
	[HasCreditAgreement] [bit] NOT NULL,
 CONSTRAINT [PK_Credits] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DelayedChargeLines]    Script Date: 12/01/2020 16:07:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DelayedChargeLines](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[DelayedChargeId] [int] NOT NULL,
	[Service] [int] NOT NULL,
	[Quantity] [float] NOT NULL,
	[Rate] [float] NOT NULL,
	[VAT] [float] NOT NULL,
 CONSTRAINT [PK_DelayedChargeLines] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DelayedCharges]    Script Date: 12/01/2020 16:07:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DelayedCharges](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[InvoiceId] [int] NOT NULL,
	[CustomerId] [int] NULL,
	[DelayedChargeDate] [datetime2](7) NOT NULL,
	[Message] [nvarchar](max) NULL,
 CONSTRAINT [PK_DelayedCharges] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[EstimateLines]    Script Date: 12/01/2020 16:07:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EstimateLines](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[EstimateId] [int] NOT NULL,
	[Service] [int] NOT NULL,
	[Quantity] [float] NOT NULL,
	[Rate] [float] NOT NULL,
	[VAT] [float] NOT NULL,
 CONSTRAINT [PK_EstimateLines] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Estimates]    Script Date: 12/01/2020 16:07:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Estimates](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[InvoiceId] [int] NOT NULL,
	[CustomerId] [int] NULL,
	[EstimateDate] [datetime2](7) NOT NULL,
	[ExpirationDate] [datetime2](7) NOT NULL,
	[Message] [nvarchar](max) NULL,
 CONSTRAINT [PK_Estimates] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[InvoiceLines]    Script Date: 12/01/2020 16:07:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[InvoiceLines](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[InvoiceId] [int] NOT NULL,
	[Service] [int] NOT NULL,
	[Quantity] [float] NOT NULL,
	[Rate] [float] NOT NULL,
	[VAT] [float] NOT NULL,
 CONSTRAINT [PK_InvoiceLines] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Invoices]    Script Date: 12/01/2020 16:07:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Invoices](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CustomerId] [int] NOT NULL,
	[InvoiceDate] [datetime2](7) NOT NULL,
	[DueDate] [datetime2](7) NOT NULL,
	[Message] [nvarchar](max) NULL,
 CONSTRAINT [PK_Invoices] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Payments]    Script Date: 12/01/2020 16:07:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Payments](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[InvoiceId] [int] NOT NULL,
	[CustomerId] [int] NULL,
	[PaymentDate] [datetime2](7) NOT NULL,
	[PaymentMethod] [int] NOT NULL,
	[Memo] [nvarchar](max) NULL,
	[AmountReceived] [float] NOT NULL,
 CONSTRAINT [PK_Payments] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SalesReceiptLines]    Script Date: 12/01/2020 16:07:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SalesReceiptLines](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[SalesReceiptId] [int] NOT NULL,
	[Service] [int] NOT NULL,
	[Quantity] [float] NOT NULL,
	[Rate] [float] NOT NULL,
	[VAT] [float] NOT NULL,
 CONSTRAINT [PK_SalesReceiptLines] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SalesReceipts]    Script Date: 12/01/2020 16:07:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SalesReceipts](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[InvoiceId] [int] NOT NULL,
	[BankAccountId] [int] NOT NULL,
	[CustomerId] [int] NULL,
	[SalesReceiptDate] [datetime2](7) NOT NULL,
	[PaymentMethod] [int] NOT NULL,
	[ReferenceNo] [nvarchar](max) NULL,
	[Message] [nvarchar](max) NULL,
 CONSTRAINT [PK_SalesReceipts] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[CreditNotes] ON 
GO
INSERT [dbo].[CreditNotes] ([Id], [InvoiceId], [CustomerId], [CreditNoteDate], [Message]) VALUES (1, 11232, 1123, CAST(N'2020-01-01T18:01:18.6111225' AS DateTime2), N'Credit Note')
GO
INSERT [dbo].[CreditNotes] ([Id], [InvoiceId], [CustomerId], [CreditNoteDate], [Message]) VALUES (2, 11232, 1123, CAST(N'2019-12-30T19:52:18.6139256' AS DateTime2), N'Credit Note')
GO
INSERT [dbo].[CreditNotes] ([Id], [InvoiceId], [CustomerId], [CreditNoteDate], [Message]) VALUES (3, 11232, 1123, CAST(N'2020-01-07T07:33:18.6139377' AS DateTime2), N'Credit Note')
GO
INSERT [dbo].[CreditNotes] ([Id], [InvoiceId], [CustomerId], [CreditNoteDate], [Message]) VALUES (4, 11232, 1123, CAST(N'2019-12-29T17:50:18.6139472' AS DateTime2), N'Credit Note')
GO
INSERT [dbo].[CreditNotes] ([Id], [InvoiceId], [CustomerId], [CreditNoteDate], [Message]) VALUES (5, 21324, 1123, CAST(N'2019-12-31T18:33:18.6139477' AS DateTime2), N'Credit Note')
GO
INSERT [dbo].[CreditNotes] ([Id], [InvoiceId], [CustomerId], [CreditNoteDate], [Message]) VALUES (6, 21324, 1123, CAST(N'2019-12-31T18:33:18.6139483' AS DateTime2), N'Credit Note')
GO
INSERT [dbo].[CreditNotes] ([Id], [InvoiceId], [CustomerId], [CreditNoteDate], [Message]) VALUES (7, 21324, 1123, CAST(N'2019-12-31T18:33:18.6139487' AS DateTime2), N'Credit Note')
GO
INSERT [dbo].[CreditNotes] ([Id], [InvoiceId], [CustomerId], [CreditNoteDate], [Message]) VALUES (8, 21324, 1123, CAST(N'2019-12-31T18:33:18.6139491' AS DateTime2), N'Credit Note')
GO
INSERT [dbo].[CreditNotes] ([Id], [InvoiceId], [CustomerId], [CreditNoteDate], [Message]) VALUES (9, 21324, 1123, CAST(N'2019-12-31T18:33:18.6139496' AS DateTime2), N'Credit Note')
GO
SET IDENTITY_INSERT [dbo].[CreditNotes] OFF
GO
SET IDENTITY_INSERT [dbo].[Credits] ON 
GO
INSERT [dbo].[Credits] ([Id], [InvoiceId], [CreditDate], [CreditAmount], [ProductCredit], [CustomerName], [AccountNo], [SortCode], [HasCreditAgreement]) VALUES (1, 11232, CAST(N'2020-01-02T17:49:18.6239765' AS DateTime2), 231, 2, N'Mr Michael Raikkonin Verstaphen', N'234-2432-324', N'34-234-234', 1)
GO
INSERT [dbo].[Credits] ([Id], [InvoiceId], [CreditDate], [CreditAmount], [ProductCredit], [CustomerName], [AccountNo], [SortCode], [HasCreditAgreement]) VALUES (2, 21324, CAST(N'2020-01-02T17:49:18.6242225' AS DateTime2), 231, 1, N'Mr Timi Schumacher Alfonso', N'234-212-676', N'567-345-234', 1)
GO
INSERT [dbo].[Credits] ([Id], [InvoiceId], [CreditDate], [CreditAmount], [ProductCredit], [CustomerName], [AccountNo], [SortCode], [HasCreditAgreement]) VALUES (3, 32312, CAST(N'2020-01-02T17:49:18.6242279' AS DateTime2), 231, 4, N'Mr Lewis Rosbert Sutton', N'546-456-345', N'435-456-123', 1)
GO
SET IDENTITY_INSERT [dbo].[Credits] OFF
GO
SET IDENTITY_INSERT [dbo].[DelayedChargeLines] ON 
GO
INSERT [dbo].[DelayedChargeLines] ([Id], [DelayedChargeId], [Service], [Quantity], [Rate], [VAT]) VALUES (1, 1, 1, 21, 23, 17.5)
GO
INSERT [dbo].[DelayedChargeLines] ([Id], [DelayedChargeId], [Service], [Quantity], [Rate], [VAT]) VALUES (2, 2, 1, 21, 23, 17.5)
GO
INSERT [dbo].[DelayedChargeLines] ([Id], [DelayedChargeId], [Service], [Quantity], [Rate], [VAT]) VALUES (3, 3, 1, 21, 23, 17.5)
GO
SET IDENTITY_INSERT [dbo].[DelayedChargeLines] OFF
GO
SET IDENTITY_INSERT [dbo].[DelayedCharges] ON 
GO
INSERT [dbo].[DelayedCharges] ([Id], [InvoiceId], [CustomerId], [DelayedChargeDate], [Message]) VALUES (1, 32312, 1123, CAST(N'2020-01-24T17:49:18.6154726' AS DateTime2), N'Delayed charge')
GO
INSERT [dbo].[DelayedCharges] ([Id], [InvoiceId], [CustomerId], [DelayedChargeDate], [Message]) VALUES (2, 32312, 2123, CAST(N'2020-01-24T17:49:18.6155754' AS DateTime2), N'Delayed charge')
GO
INSERT [dbo].[DelayedCharges] ([Id], [InvoiceId], [CustomerId], [DelayedChargeDate], [Message]) VALUES (3, 32312, 2123, CAST(N'2020-01-04T17:49:18.6155788' AS DateTime2), N'Delayed charge')
GO
INSERT [dbo].[DelayedCharges] ([Id], [InvoiceId], [CustomerId], [DelayedChargeDate], [Message]) VALUES (4, 21324, 3123, CAST(N'2020-08-01T17:49:18.6155793' AS DateTime2), N'Delayed charge')
GO
INSERT [dbo].[DelayedCharges] ([Id], [InvoiceId], [CustomerId], [DelayedChargeDate], [Message]) VALUES (5, 21324, 3123, CAST(N'2020-01-05T17:49:18.6155797' AS DateTime2), N'Delayed charge')
GO
INSERT [dbo].[DelayedCharges] ([Id], [InvoiceId], [CustomerId], [DelayedChargeDate], [Message]) VALUES (6, 21324, 3123, CAST(N'2020-01-21T17:49:18.6155801' AS DateTime2), N'Delayed charge')
GO
INSERT [dbo].[DelayedCharges] ([Id], [InvoiceId], [CustomerId], [DelayedChargeDate], [Message]) VALUES (7, 32312, 3123, CAST(N'2020-08-01T17:49:18.6155804' AS DateTime2), N'Delayed charge')
GO
INSERT [dbo].[DelayedCharges] ([Id], [InvoiceId], [CustomerId], [DelayedChargeDate], [Message]) VALUES (8, 32312, 3123, CAST(N'2020-01-05T17:49:18.6155808' AS DateTime2), N'Delayed charge')
GO
INSERT [dbo].[DelayedCharges] ([Id], [InvoiceId], [CustomerId], [DelayedChargeDate], [Message]) VALUES (9, 32312, 3123, CAST(N'2020-01-21T17:49:18.6155812' AS DateTime2), N'Delayed charge')
GO
SET IDENTITY_INSERT [dbo].[DelayedCharges] OFF
GO
SET IDENTITY_INSERT [dbo].[EstimateLines] ON 
GO
INSERT [dbo].[EstimateLines] ([Id], [EstimateId], [Service], [Quantity], [Rate], [VAT]) VALUES (1, 1, 1, 12, 133, 17.5)
GO
INSERT [dbo].[EstimateLines] ([Id], [EstimateId], [Service], [Quantity], [Rate], [VAT]) VALUES (2, 2, 1, 12, 133, 17.5)
GO
INSERT [dbo].[EstimateLines] ([Id], [EstimateId], [Service], [Quantity], [Rate], [VAT]) VALUES (3, 3, 1, 12, 133, 17.5)
GO
SET IDENTITY_INSERT [dbo].[EstimateLines] OFF
GO
SET IDENTITY_INSERT [dbo].[Estimates] ON 
GO
INSERT [dbo].[Estimates] ([Id], [InvoiceId], [CustomerId], [EstimateDate], [ExpirationDate], [Message]) VALUES (1, 11232, 1123, CAST(N'2020-01-04T17:49:18.6170453' AS DateTime2), CAST(N'2020-01-24T17:49:18.6170964' AS DateTime2), N'20% Discount included!')
GO
INSERT [dbo].[Estimates] ([Id], [InvoiceId], [CustomerId], [EstimateDate], [ExpirationDate], [Message]) VALUES (2, 11232, 2123, CAST(N'2020-01-04T17:49:18.6172094' AS DateTime2), CAST(N'2020-01-24T17:49:18.6172116' AS DateTime2), N'20% Discount included!')
GO
INSERT [dbo].[Estimates] ([Id], [InvoiceId], [CustomerId], [EstimateDate], [ExpirationDate], [Message]) VALUES (3, 21324, 3123, CAST(N'2020-01-04T17:49:18.6172137' AS DateTime2), CAST(N'2020-01-24T17:49:18.6172140' AS DateTime2), N'20% Discount included!')
GO
INSERT [dbo].[Estimates] ([Id], [InvoiceId], [CustomerId], [EstimateDate], [ExpirationDate], [Message]) VALUES (4, 21324, 1123, CAST(N'2020-01-04T17:49:18.6172144' AS DateTime2), CAST(N'2020-01-24T17:49:18.6172148' AS DateTime2), N'20% Discount included!')
GO
INSERT [dbo].[Estimates] ([Id], [InvoiceId], [CustomerId], [EstimateDate], [ExpirationDate], [Message]) VALUES (5, 21324, 2123, CAST(N'2020-01-04T17:49:18.6172151' AS DateTime2), CAST(N'2020-01-24T17:49:18.6172155' AS DateTime2), N'20% Discount included!')
GO
INSERT [dbo].[Estimates] ([Id], [InvoiceId], [CustomerId], [EstimateDate], [ExpirationDate], [Message]) VALUES (6, 11232, 3123, CAST(N'2020-01-04T17:49:18.6172159' AS DateTime2), CAST(N'2020-01-24T17:49:18.6172162' AS DateTime2), N'20% Discount included!')
GO
INSERT [dbo].[Estimates] ([Id], [InvoiceId], [CustomerId], [EstimateDate], [ExpirationDate], [Message]) VALUES (7, 32312, 1123, CAST(N'2020-01-04T17:49:18.6172167' AS DateTime2), CAST(N'2020-01-24T17:49:18.6172170' AS DateTime2), N'20% Discount included!')
GO
INSERT [dbo].[Estimates] ([Id], [InvoiceId], [CustomerId], [EstimateDate], [ExpirationDate], [Message]) VALUES (8, 32312, 2123, CAST(N'2020-01-04T17:49:18.6172174' AS DateTime2), CAST(N'2020-01-24T17:49:18.6172177' AS DateTime2), N'20% Discount included!')
GO
INSERT [dbo].[Estimates] ([Id], [InvoiceId], [CustomerId], [EstimateDate], [ExpirationDate], [Message]) VALUES (9, 32312, 3123, CAST(N'2020-01-04T17:49:18.6172181' AS DateTime2), CAST(N'2020-01-24T17:49:18.6172184' AS DateTime2), N'20% Discount included!')
GO
SET IDENTITY_INSERT [dbo].[Estimates] OFF
GO
SET IDENTITY_INSERT [dbo].[InvoiceLines] ON 
GO
INSERT [dbo].[InvoiceLines] ([Id], [InvoiceId], [Service], [Quantity], [Rate], [VAT]) VALUES (1, 11232, 0, 232, 24.5, 0)
GO
INSERT [dbo].[InvoiceLines] ([Id], [InvoiceId], [Service], [Quantity], [Rate], [VAT]) VALUES (2, 11232, 0, 22, 11, 0)
GO
INSERT [dbo].[InvoiceLines] ([Id], [InvoiceId], [Service], [Quantity], [Rate], [VAT]) VALUES (3, 11232, 0, 23, 11, 0)
GO
INSERT [dbo].[InvoiceLines] ([Id], [InvoiceId], [Service], [Quantity], [Rate], [VAT]) VALUES (4, 11232, 0, 122, 223, 0)
GO
INSERT [dbo].[InvoiceLines] ([Id], [InvoiceId], [Service], [Quantity], [Rate], [VAT]) VALUES (5, 21324, 0, 23, 11, 0)
GO
INSERT [dbo].[InvoiceLines] ([Id], [InvoiceId], [Service], [Quantity], [Rate], [VAT]) VALUES (6, 21324, 0, 23, 11, 0)
GO
INSERT [dbo].[InvoiceLines] ([Id], [InvoiceId], [Service], [Quantity], [Rate], [VAT]) VALUES (7, 21324, 0, 23, 11, 0)
GO
INSERT [dbo].[InvoiceLines] ([Id], [InvoiceId], [Service], [Quantity], [Rate], [VAT]) VALUES (8, 21324, 0, 23, 11, 0)
GO
INSERT [dbo].[InvoiceLines] ([Id], [InvoiceId], [Service], [Quantity], [Rate], [VAT]) VALUES (9, 21324, 0, 23, 11, 0)
GO
INSERT [dbo].[InvoiceLines] ([Id], [InvoiceId], [Service], [Quantity], [Rate], [VAT]) VALUES (10, 32312, 0, 23, 11, 0)
GO
INSERT [dbo].[InvoiceLines] ([Id], [InvoiceId], [Service], [Quantity], [Rate], [VAT]) VALUES (11, 32312, 0, 23, 11, 0)
GO
INSERT [dbo].[InvoiceLines] ([Id], [InvoiceId], [Service], [Quantity], [Rate], [VAT]) VALUES (12, 32312, 0, 23, 11, 0)
GO
INSERT [dbo].[InvoiceLines] ([Id], [InvoiceId], [Service], [Quantity], [Rate], [VAT]) VALUES (13, 32312, 0, 23, 11, 0)
GO
INSERT [dbo].[InvoiceLines] ([Id], [InvoiceId], [Service], [Quantity], [Rate], [VAT]) VALUES (14, 32312, 0, 23, 11, 0)
GO
INSERT [dbo].[InvoiceLines] ([Id], [InvoiceId], [Service], [Quantity], [Rate], [VAT]) VALUES (15, 32312, 0, 23, 11, 0)
GO
INSERT [dbo].[InvoiceLines] ([Id], [InvoiceId], [Service], [Quantity], [Rate], [VAT]) VALUES (16, 32312, 0, 23, 11, 0)
GO
SET IDENTITY_INSERT [dbo].[InvoiceLines] OFF
GO
SET IDENTITY_INSERT [dbo].[Invoices] ON 
GO
INSERT [dbo].[Invoices] ([Id], [CustomerId], [InvoiceDate], [DueDate], [Message]) VALUES (11232, 1123, CAST(N'2020-01-06T17:49:18.6183577' AS DateTime2), CAST(N'2020-04-12T17:49:18.6183038' AS DateTime2), N'Email invoice.')
GO
INSERT [dbo].[Invoices] ([Id], [CustomerId], [InvoiceDate], [DueDate], [Message]) VALUES (21324, 2123, CAST(N'2020-01-06T17:49:18.6184601' AS DateTime2), CAST(N'2020-04-12T17:49:18.6184580' AS DateTime2), N'Email invoice.')
GO
INSERT [dbo].[Invoices] ([Id], [CustomerId], [InvoiceDate], [DueDate], [Message]) VALUES (32312, 3123, CAST(N'2020-01-06T17:49:18.6184621' AS DateTime2), CAST(N'2020-04-12T17:49:18.6184618' AS DateTime2), N'Email invoice.')
GO
SET IDENTITY_INSERT [dbo].[Invoices] OFF
GO
SET IDENTITY_INSERT [dbo].[Payments] ON 
GO
INSERT [dbo].[Payments] ([Id], [InvoiceId], [CustomerId], [PaymentDate], [PaymentMethod], [Memo], [AmountReceived]) VALUES (1, 11232, 1123, CAST(N'2020-01-24T17:49:18.6202802' AS DateTime2), 1, N'Thanks!', 200)
GO
INSERT [dbo].[Payments] ([Id], [InvoiceId], [CustomerId], [PaymentDate], [PaymentMethod], [Memo], [AmountReceived]) VALUES (2, 11232, 2123, CAST(N'2020-01-24T17:49:18.6204757' AS DateTime2), 1, N'Thanks!', 200)
GO
INSERT [dbo].[Payments] ([Id], [InvoiceId], [CustomerId], [PaymentDate], [PaymentMethod], [Memo], [AmountReceived]) VALUES (3, 11232, 3123, CAST(N'2020-01-24T17:49:18.6204804' AS DateTime2), 1, N'Thanks!', 200)
GO
INSERT [dbo].[Payments] ([Id], [InvoiceId], [CustomerId], [PaymentDate], [PaymentMethod], [Memo], [AmountReceived]) VALUES (4, 21324, 1123, CAST(N'2020-01-24T17:49:18.6204809' AS DateTime2), 1, N'Thanks!', 200)
GO
INSERT [dbo].[Payments] ([Id], [InvoiceId], [CustomerId], [PaymentDate], [PaymentMethod], [Memo], [AmountReceived]) VALUES (5, 21324, 2123, CAST(N'2020-01-24T17:49:18.6204813' AS DateTime2), 1, N'Thanks!', 200)
GO
INSERT [dbo].[Payments] ([Id], [InvoiceId], [CustomerId], [PaymentDate], [PaymentMethod], [Memo], [AmountReceived]) VALUES (6, 21324, 3123, CAST(N'2020-01-24T17:49:18.6204817' AS DateTime2), 1, N'Thanks!', 200)
GO
INSERT [dbo].[Payments] ([Id], [InvoiceId], [CustomerId], [PaymentDate], [PaymentMethod], [Memo], [AmountReceived]) VALUES (7, 32312, 1123, CAST(N'2020-01-24T17:49:18.6204820' AS DateTime2), 1, N'Thanks!', 200)
GO
INSERT [dbo].[Payments] ([Id], [InvoiceId], [CustomerId], [PaymentDate], [PaymentMethod], [Memo], [AmountReceived]) VALUES (8, 32312, 2123, CAST(N'2020-01-24T17:49:18.6204824' AS DateTime2), 1, N'Thanks!', 200)
GO
INSERT [dbo].[Payments] ([Id], [InvoiceId], [CustomerId], [PaymentDate], [PaymentMethod], [Memo], [AmountReceived]) VALUES (9, 32312, 3123, CAST(N'2020-01-24T17:49:18.6204828' AS DateTime2), 1, N'Thanks!', 200)
GO
SET IDENTITY_INSERT [dbo].[Payments] OFF
GO
SET IDENTITY_INSERT [dbo].[SalesReceiptLines] ON 
GO
INSERT [dbo].[SalesReceiptLines] ([Id], [SalesReceiptId], [Service], [Quantity], [Rate], [VAT]) VALUES (1, 1, 0, 0, 23, 0)
GO
INSERT [dbo].[SalesReceiptLines] ([Id], [SalesReceiptId], [Service], [Quantity], [Rate], [VAT]) VALUES (2, 2, 0, 0, 23, 0)
GO
INSERT [dbo].[SalesReceiptLines] ([Id], [SalesReceiptId], [Service], [Quantity], [Rate], [VAT]) VALUES (3, 3, 0, 0, 23, 0)
GO
SET IDENTITY_INSERT [dbo].[SalesReceiptLines] OFF
GO
SET IDENTITY_INSERT [dbo].[SalesReceipts] ON 
GO
INSERT [dbo].[SalesReceipts] ([Id], [InvoiceId], [BankAccountId], [CustomerId], [SalesReceiptDate], [PaymentMethod], [ReferenceNo], [Message]) VALUES (1, 11232, 1987, 1123, CAST(N'2020-01-26T17:49:18.6214146' AS DateTime2), 3, N'2342', N'New customer!')
GO
INSERT [dbo].[SalesReceipts] ([Id], [InvoiceId], [BankAccountId], [CustomerId], [SalesReceiptDate], [PaymentMethod], [ReferenceNo], [Message]) VALUES (2, 11232, 1987, 2123, CAST(N'2020-01-26T17:49:18.6216046' AS DateTime2), 3, N'2342', N'New customer!')
GO
INSERT [dbo].[SalesReceipts] ([Id], [InvoiceId], [BankAccountId], [CustomerId], [SalesReceiptDate], [PaymentMethod], [ReferenceNo], [Message]) VALUES (3, 11232, 1987, 3123, CAST(N'2020-01-26T17:49:18.6216090' AS DateTime2), 3, N'2342', N'New customer!')
GO
INSERT [dbo].[SalesReceipts] ([Id], [InvoiceId], [BankAccountId], [CustomerId], [SalesReceiptDate], [PaymentMethod], [ReferenceNo], [Message]) VALUES (4, 21324, 1987, 1123, CAST(N'2020-01-26T17:49:18.6216095' AS DateTime2), 3, N'2342', N'New customer!')
GO
INSERT [dbo].[SalesReceipts] ([Id], [InvoiceId], [BankAccountId], [CustomerId], [SalesReceiptDate], [PaymentMethod], [ReferenceNo], [Message]) VALUES (5, 21324, 1987, 2123, CAST(N'2020-01-26T17:49:18.6216099' AS DateTime2), 3, N'2342', N'New customer!')
GO
INSERT [dbo].[SalesReceipts] ([Id], [InvoiceId], [BankAccountId], [CustomerId], [SalesReceiptDate], [PaymentMethod], [ReferenceNo], [Message]) VALUES (6, 21324, 1987, 3123, CAST(N'2020-01-26T17:49:18.6216102' AS DateTime2), 3, N'2342', N'New customer!')
GO
INSERT [dbo].[SalesReceipts] ([Id], [InvoiceId], [BankAccountId], [CustomerId], [SalesReceiptDate], [PaymentMethod], [ReferenceNo], [Message]) VALUES (7, 32312, 1987, 1123, CAST(N'2020-01-26T17:49:18.6216106' AS DateTime2), 3, N'2342', N'New customer!')
GO
INSERT [dbo].[SalesReceipts] ([Id], [InvoiceId], [BankAccountId], [CustomerId], [SalesReceiptDate], [PaymentMethod], [ReferenceNo], [Message]) VALUES (8, 32312, 1987, 2123, CAST(N'2020-01-26T17:49:18.6216110' AS DateTime2), 3, N'2342', N'New customer!')
GO
INSERT [dbo].[SalesReceipts] ([Id], [InvoiceId], [BankAccountId], [CustomerId], [SalesReceiptDate], [PaymentMethod], [ReferenceNo], [Message]) VALUES (9, 32312, 1987, 3123, CAST(N'2020-01-26T17:49:18.6216114' AS DateTime2), 3, N'2342', N'New customer!')
GO
SET IDENTITY_INSERT [dbo].[SalesReceipts] OFF
GO
ALTER TABLE [dbo].[Credits]  WITH CHECK ADD  CONSTRAINT [FK_Credits_Invoices_InvoiceId] FOREIGN KEY([InvoiceId])
REFERENCES [dbo].[Invoices] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Credits] CHECK CONSTRAINT [FK_Credits_Invoices_InvoiceId]
GO
ALTER TABLE [dbo].[DelayedChargeLines]  WITH CHECK ADD  CONSTRAINT [FK_DelayedChargeLines_DelayedCharges_DelayedChargeId] FOREIGN KEY([DelayedChargeId])
REFERENCES [dbo].[DelayedCharges] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[DelayedChargeLines] CHECK CONSTRAINT [FK_DelayedChargeLines_DelayedCharges_DelayedChargeId]
GO
ALTER TABLE [dbo].[DelayedCharges]  WITH CHECK ADD  CONSTRAINT [FK_DelayedCharges_Invoices_InvoiceId] FOREIGN KEY([InvoiceId])
REFERENCES [dbo].[Invoices] ([Id])
GO
ALTER TABLE [dbo].[DelayedCharges] CHECK CONSTRAINT [FK_DelayedCharges_Invoices_InvoiceId]
GO
ALTER TABLE [dbo].[EstimateLines]  WITH CHECK ADD  CONSTRAINT [FK_EstimateLines_Estimates_EstimateId] FOREIGN KEY([EstimateId])
REFERENCES [dbo].[Estimates] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[EstimateLines] CHECK CONSTRAINT [FK_EstimateLines_Estimates_EstimateId]
GO
ALTER TABLE [dbo].[Estimates]  WITH CHECK ADD  CONSTRAINT [FK_Estimates_Invoices_InvoiceId] FOREIGN KEY([InvoiceId])
REFERENCES [dbo].[Invoices] ([Id])
GO
ALTER TABLE [dbo].[Estimates] CHECK CONSTRAINT [FK_Estimates_Invoices_InvoiceId]
GO
ALTER TABLE [dbo].[InvoiceLines]  WITH CHECK ADD  CONSTRAINT [FK_InvoiceLines_Invoices_InvoiceId] FOREIGN KEY([InvoiceId])
REFERENCES [dbo].[Invoices] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[InvoiceLines] CHECK CONSTRAINT [FK_InvoiceLines_Invoices_InvoiceId]
GO
ALTER TABLE [dbo].[Payments]  WITH CHECK ADD  CONSTRAINT [FK_Payments_Invoices_InvoiceId] FOREIGN KEY([InvoiceId])
REFERENCES [dbo].[Invoices] ([Id])
GO
ALTER TABLE [dbo].[Payments] CHECK CONSTRAINT [FK_Payments_Invoices_InvoiceId]
GO
ALTER TABLE [dbo].[SalesReceiptLines]  WITH CHECK ADD  CONSTRAINT [FK_SalesReceiptLines_SalesReceipts_SalesReceiptId] FOREIGN KEY([SalesReceiptId])
REFERENCES [dbo].[SalesReceipts] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[SalesReceiptLines] CHECK CONSTRAINT [FK_SalesReceiptLines_SalesReceipts_SalesReceiptId]
GO
ALTER TABLE [dbo].[SalesReceipts]  WITH CHECK ADD  CONSTRAINT [FK_SalesReceipts_Invoices_InvoiceId] FOREIGN KEY([InvoiceId])
REFERENCES [dbo].[Invoices] ([Id])
GO
ALTER TABLE [dbo].[SalesReceipts] CHECK CONSTRAINT [FK_SalesReceipts_Invoices_InvoiceId]
GO
