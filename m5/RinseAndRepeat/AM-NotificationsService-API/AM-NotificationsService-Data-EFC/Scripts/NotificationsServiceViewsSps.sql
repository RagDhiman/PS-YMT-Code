Create View vwNSEmails
as

Select *
from Emails

Go

--==========================================

Create View vwNSSMSs
as

Select *
from SMSs

Go

--==========================================

Create View vwNSWebhookPosts
as

Select *
from WebhookPosts

Go

--==========================================
Create Procedure spNSEmailInsert
@SendTo	nvarchar,
@Sender	nvarchar,
@Subject	nvarchar,
@Message	nvarchar,
@SentDateTime	datetime2,
@DeliveredDateTime	datetime2
as

SET NOCOUNT ON;

Insert into Emails(SendTo,
Sender,
Subject,
Message,
SentDateTime,
DeliveredDateTime)
VALUES (@SendTo,
@Sender,
@Subject,
@Message,
@SentDateTime,
@DeliveredDateTime)

Select * from [Emails] where Id = @@IDENTITY

GO

--==========================================

Create Procedure spNSEmailUpdate
@Id	int,
@SendTo	nvarchar,
@Sender	nvarchar,
@Subject	nvarchar,
@Message	nvarchar,
@SentDateTime	datetime2,
@DeliveredDateTime	datetime2
as

SET NOCOUNT ON;

Update Emails
Set SendTo = @SendTo,
Sender = @Sender,
Subject = @Subject,
Message = @Message,
SentDateTime = @SentDateTime,
DeliveredDateTime = @DeliveredDateTime
where Id = @Id

GO

--==========================================

Create procedure spNSEmailDelete
@Id int
as
Delete from Emails where Id = @Id

GO

--========================================

Create Procedure spNSSMSInsert
@SendTo	nvarchar,
@Sender	nvarchar,
@Message	nvarchar,
@SentDateTime	datetime2,
@DeliveredDateTime	datetime2
as

SET NOCOUNT ON;

Insert into SMSs(SendTo,
Sender,
Message,
SentDateTime,
DeliveredDateTime)
VALUES (@SendTo,
@Sender,
@Message,
@SentDateTime,
@DeliveredDateTime)

Select * from [SMSs] where Id = @@IDENTITY

GO

--==========================================

Create Procedure spNSSMSUpdate
@Id	int,
@SendTo	nvarchar,
@Sender	nvarchar,
@Message	nvarchar,
@SentDateTime	datetime2,
@DeliveredDateTime	datetime2
as

SET NOCOUNT ON;

Update SMSs
Set SendTo = @SendTo,
Sender = @Sender,
Message = @Message,
SentDateTime = @SentDateTime,
DeliveredDateTime = @DeliveredDateTime
where Id = @Id

GO

--==========================================

Create procedure spNSSMSDelete
@Id int
as
Delete from SMSs where Id = @Id

GO

--========================================

Create Procedure spNSWebhookPostInsert
@URL	nvarchar,
@Sender	nvarchar,
@Body	nvarchar,
@PostDateTime	datetime2
as

SET NOCOUNT ON;

Insert into WebhookPosts(URL,
Sender,
Body,
PostDateTime)
VALUES (@URL,
@Sender,
@Body,
@PostDateTime)

Select * from [WebhookPosts] where Id = @@IDENTITY

GO

--==========================================

Create Procedure spNSWebhookPostUpdate
@Id	int,
@URL	nvarchar,
@Sender	nvarchar,
@Body	nvarchar,
@PostDateTime	datetime2
as

SET NOCOUNT ON;

Update WebhookPosts
Set [URL] = @URL,
Sender = @Sender,
Body = @Body,
PostDateTime = @PostDateTime
where Id = @Id

GO

--==========================================

Create procedure spNSWebhookPostDelete
@Id int
as
Delete from WebhookPosts where Id = @Id

GO

--========================================






