--==========================================

Create View vwSMSuppliers
as

Select *
from Suppliers

Go

--==========================================

Create View vwSMSupplierNotes
as

Select *
from SupplierNotes

Go

--==========================================

Create View vwSMAttachments
as

Select *
from Attachments

Go
--==========================================

Create Procedure spSMSuppliersInsert
@AccountId	int,
@ContactName	nvarchar,
@Company	nvarchar,
@Email	nvarchar,
@Phone	nvarchar,
@Mobile	nvarchar,
@Fax	nvarchar,
@Website	nvarchar
as

SET NOCOUNT ON;

Insert into Suppliers(AccountId,
ContactName,
Company,
Email,
Phone,
Mobile,
Fax,
Website)
VALUES (@AccountId,
@ContactName,
@Company,
@Email,
@Phone,
@Mobile,
@Fax,
@Website)

Select * from [Suppliers] where Id = @@IDENTITY

GO

--==========================================

Create Procedure spSMSuppliersUpdate
@Id	int,
@AccountId	int,
@ContactName	nvarchar,
@Company	nvarchar,
@Email	nvarchar,
@Phone	nvarchar,
@Mobile	nvarchar,
@Fax	nvarchar,
@Website	nvarchar
as

SET NOCOUNT ON;

Update Suppliers
Set AccountId = @AccountId,
ContactName = @ContactName,
Company = @Company,
Email = @Email,
Phone = @Phone,
Mobile = @Mobile,
Fax = @Fax,
Website = @Website
where Id = @Id

GO

--==========================================

Create procedure spSMSuppliersDelete
@Id int
as
Delete from Suppliers where Id = @Id

GO

--========================================

--==========================================

Create Procedure spSMSupplierNotesInsert
@NoteText	nvarchar,
@SupplierId	int
as

SET NOCOUNT ON;

Insert into SupplierNotes(NoteText,
SupplierId)
VALUES (@NoteText,
@SupplierId)

Select * from [SupplierNotes] where Id = @@IDENTITY

GO

--==========================================

Create Procedure spSMSupplierNotesUpdate
@Id	int,
@NoteText	nvarchar,
@SupplierId	int
as

SET NOCOUNT ON;

Update SupplierNotes
Set NoteText = @NoteText,
SupplierId = @SupplierId
where Id = @Id

GO

--==========================================

Create procedure spSMSupplierNotesDelete
@Id int
as
Delete from SupplierNotes where Id = @Id

GO

--========================================

--==========================================

Create Procedure spSMAttachmentsInsert
@FilePath	nvarchar,
@SupplierId	int
as

SET NOCOUNT ON;

Insert into Attachments(FilePath,
SupplierId)
VALUES (@FilePath,
@SupplierId)

Select * from [Attachments] where Id = @@IDENTITY

GO

--==========================================

Create Procedure spSMAttachmentsUpdate
@Id	int,
@FilePath	nvarchar,
@SupplierId	int
as

SET NOCOUNT ON;

Update Attachments
Set FilePath = @FilePath,
SupplierId = @SupplierId
where Id = @Id

GO

--==========================================

Create procedure spSMAttachmentsDelete
@Id int
as
Delete from Attachments where Id = @Id

GO

--========================================

