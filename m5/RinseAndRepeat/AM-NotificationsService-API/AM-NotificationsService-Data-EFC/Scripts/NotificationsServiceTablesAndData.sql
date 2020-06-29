SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Emails](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[SendTo] [nvarchar](max) NULL,
	[Sender] [nvarchar](max) NULL,
	[Subject] [nvarchar](max) NULL,
	[Message] [nvarchar](max) NULL,
	[SentDateTime] [datetime2](7) NOT NULL,
	[DeliveredDateTime] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_Emails] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SMSs]    Script Date: 17/01/2020 22:27:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SMSs](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[SendTo] [nvarchar](max) NULL,
	[Sender] [nvarchar](max) NULL,
	[Message] [nvarchar](max) NULL,
	[SentDateTime] [datetime2](7) NOT NULL,
	[DeliveredDateTime] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_SMSs] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[WebhookPosts]    Script Date: 17/01/2020 22:27:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[WebhookPosts](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[URL] [nvarchar](max) NULL,
	[Sender] [nvarchar](max) NULL,
	[Body] [nvarchar](max) NULL,
	[PostDateTime] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_WebhookPosts] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Emails] ON 
GO
INSERT [dbo].[Emails] ([Id], [SendTo], [Sender], [Subject], [Message], [SentDateTime], [DeliveredDateTime]) VALUES (1, N'2342352354324', N'Bob Smith', N'Invoice Sent!', N'Invoice sent!', CAST(N'2020-01-03T17:49:18.6325585' AS DateTime2), CAST(N'2020-01-03T18:19:18.6326103' AS DateTime2))
GO
INSERT [dbo].[Emails] ([Id], [SendTo], [Sender], [Subject], [Message], [SentDateTime], [DeliveredDateTime]) VALUES (2, N'2342352354324', N'Bob Smith', N'Invoice Sent!', N'Invoice sent!', CAST(N'2020-01-03T17:49:18.6327494' AS DateTime2), CAST(N'2020-01-03T18:19:18.6327516' AS DateTime2))
GO
INSERT [dbo].[Emails] ([Id], [SendTo], [Sender], [Subject], [Message], [SentDateTime], [DeliveredDateTime]) VALUES (3, N'rag.dhiman@gmail.com', N'rag.dhiman@NehaKD.com', N'Credit agreement link from NehaKD.', N'Welcome Dummy, please find attached credit agreement link from NehaKD.', CAST(N'2020-01-05T15:17:01.1156565' AS DateTime2), CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2))
GO
INSERT [dbo].[Emails] ([Id], [SendTo], [Sender], [Subject], [Message], [SentDateTime], [DeliveredDateTime]) VALUES (4, N'rag.dhiman@gmail.com', N'rag.dhiman@NehaKD.com', N'Credit agreement link from NehaKD.', N'Welcome Dummy, please find attached credit agreement link from NehaKD.', CAST(N'2020-01-05T15:17:25.7806084' AS DateTime2), CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2))
GO
INSERT [dbo].[Emails] ([Id], [SendTo], [Sender], [Subject], [Message], [SentDateTime], [DeliveredDateTime]) VALUES (5, N'rag.dhiman@gmail.com', N'rag.dhiman@NehaKD.com', N'Credit agreement link from NehaKD.', N'Welcome Dummy, please find attached credit agreement link from NehaKD.', CAST(N'2020-01-11T12:06:11.7348930' AS DateTime2), CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2))
GO
SET IDENTITY_INSERT [dbo].[Emails] OFF
GO
SET IDENTITY_INSERT [dbo].[SMSs] ON 
GO
INSERT [dbo].[SMSs] ([Id], [SendTo], [Sender], [Message], [SentDateTime], [DeliveredDateTime]) VALUES (1, N'2342352354324', N'Bob Smith', N'Invoice sent!', CAST(N'2020-01-03T17:49:18.6319251' AS DateTime2), CAST(N'2020-01-03T18:19:18.6319773' AS DateTime2))
GO
INSERT [dbo].[SMSs] ([Id], [SendTo], [Sender], [Message], [SentDateTime], [DeliveredDateTime]) VALUES (2, N'2342352354324', N'Bill Smith', N'Invoice sent!', CAST(N'2020-01-03T17:49:18.6320737' AS DateTime2), CAST(N'2020-01-03T18:19:18.6320757' AS DateTime2))
GO
SET IDENTITY_INSERT [dbo].[SMSs] OFF
GO
SET IDENTITY_INSERT [dbo].[WebhookPosts] ON 
GO
INSERT [dbo].[WebhookPosts] ([Id], [URL], [Sender], [Body], [PostDateTime]) VALUES (1, N'Invoice Sent!', N'Bob Smith', N'Invoice sent!', CAST(N'2020-01-03T17:49:18.6331142' AS DateTime2))
GO
INSERT [dbo].[WebhookPosts] ([Id], [URL], [Sender], [Body], [PostDateTime]) VALUES (2, N'Invoice Sent!', N'Bob Smith', N'Invoice sent!', CAST(N'2020-01-03T17:49:18.6332826' AS DateTime2))
GO
INSERT [dbo].[WebhookPosts] ([Id], [URL], [Sender], [Body], [PostDateTime]) VALUES (3, N'https://nehacrm.api.com/customer', N'NehaKD', N'{"Id":4346,"AccountId":1987,"Title":"Mr","FirstName":"Dummy","MiddleName":"Kumar","LastName":"Test","Suffix":"MCP","Company":null,"DisplayNameAs":null,"Email":"rag.dhiman@gmail.com","Phone":null,"Mobile":null,"Fax":null,"Website":null,"CreditAgreement":false}', CAST(N'2020-01-05T15:17:01.3059713' AS DateTime2))
GO
INSERT [dbo].[WebhookPosts] ([Id], [URL], [Sender], [Body], [PostDateTime]) VALUES (4, N'https://nehacrm.api.com/customer', N'NehaKD', N'{"Id":4347,"AccountId":1987,"Title":"Mr","FirstName":"Dummy","MiddleName":"Kumar","LastName":"Test","Suffix":"MCP","Company":null,"DisplayNameAs":"Rag Dhiman","Email":"rag.dhiman@gmail.com","Phone":null,"Mobile":null,"Fax":null,"Website":null,"CreditAgreement":false}', CAST(N'2020-01-05T15:17:25.7925037' AS DateTime2))
GO
INSERT [dbo].[WebhookPosts] ([Id], [URL], [Sender], [Body], [PostDateTime]) VALUES (5, N'https://nehacrm.api.com/customer', N'NehaKD', N'{"Id":4350,"AccountId":1987,"Title":"Mr","FirstName":"Dummy","MiddleName":"New DB Person","LastName":"Test","Suffix":"MCP","Company":null,"DisplayNameAs":"Rag Dhiman","Email":"rag.dhiman@gmail.com","Phone":null,"Mobile":null,"Fax":null,"Website":null,"CreditAgreement":false}', CAST(N'2020-01-11T12:06:11.9278256' AS DateTime2))
GO
SET IDENTITY_INSERT [dbo].[WebhookPosts] OFF
GO
