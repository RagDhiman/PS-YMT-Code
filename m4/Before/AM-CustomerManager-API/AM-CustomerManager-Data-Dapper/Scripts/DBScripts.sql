CREATE VIEW [dbo].[vwCMCustomer]
AS
SELECT   Id, AccountId
,Title
,FirstName
,MiddleName
,LastName
,Suffix
,Company
,DisplayNameAs
,Email
,Phone
,Mobile
,Fax
,Website
,CreditAgreement
FROM         dbo.Customers
GO

Create procedure spCMCustomerDelete
@Id int
as
Delete from Customers where Id = @Id

GO

Create PROCEDURE [dbo].[spCMCustomerInsert] 
	-- Add the parameters for the stored procedure here
@AccountId int
,@Title nvarchar(max)
,@FirstName nvarchar(max)
,@MiddleName nvarchar(max)
,@LastName	nvarchar(max)
,@Suffix	nvarchar(max)
,@Company	nvarchar(max)
,@DisplayNameAs nvarchar(max)
,@Email nvarchar(max)
,@Phone nvarchar(max)
,@Mobile nvarchar(max)
,@Fax nvarchar(max)
,@Website nvarchar(max)
,@CreditAgreement bit

AS
	SET NOCOUNT ON;

	INSERT INTO [dbo].[Customers]
			   (AccountId
,Title
,FirstName
,MiddleName
,LastName
,Suffix
,Company
,DisplayNameAs
,Email
,Phone
,Mobile
,Fax
,Website
,CreditAgreement)
		 VALUES
			   (@AccountId
,@Title
,@FirstName
,@MiddleName
,@LastName
,@Suffix
,@Company
,@DisplayNameAs
,@Email
,@Phone
,@Mobile
,@Fax
,@Website
,@CreditAgreement)

Select * from [Customers] where Id = @@IDENTITY

GO

CREATE PROCEDURE spCMCustomerUpdate 
	-- Add the parameters for the stored procedure here
@Id	int
,@AccountId int
,@Title nvarchar(max)
,@FirstName nvarchar(max)
,@MiddleName nvarchar(max)
,@LastName	nvarchar(max)
,@Suffix	nvarchar(max)
,@Company	nvarchar(max)
,@DisplayNameAs nvarchar(max)
,@Email nvarchar(max)
,@Phone nvarchar(max)
,@Mobile nvarchar(max)
,@Fax nvarchar(max)
,@Website nvarchar(max)
,@CreditAgreement bit
AS
	SET NOCOUNT ON;

	Update Customers
	set AccountId = @AccountId
	,Title = @Title
	,FirstName = @FirstName
	,MiddleName = @MiddleName
	,LastName = @LastName
	,Suffix = @Suffix
	,Company = @Company
	,DisplayNameAs = @DisplayNameAs
	,Email = @Email
	,Phone = @Phone
	,Mobile = @Mobile
	,Fax = @Fax
	,Website = @Website
	,CreditAgreement = @CreditAgreement
	where Id = @Id
go


CREATE VIEW [dbo].[vwCMBankAccount]
AS
SELECT   Id, CustomerId, AccountNo, SortCode
FROM         dbo.BankAccounts
GO

Create procedure spCMBankAccountDelete
@Id int
as
Delete from BankAccounts where Id = @Id

GO

Create PROCEDURE [dbo].[spCMBankAccountInsert] 
	-- Add the parameters for the stored procedure here
	@CustomerId int
    ,@AccountNo varchar(200)
    ,@SortCode varchar(200)

AS
	SET NOCOUNT ON;

	INSERT INTO [dbo].[BankAccounts]
			   ([CustomerId]
			   ,[AccountNo]
			   ,[SortCode])
		 VALUES
			   (@CustomerId
			   ,@AccountNo
			   ,@SortCode)

Select * from [BankAccounts] where Id = @@IDENTITY

GO

CREATE PROCEDURE spCMBankAccountUpdate 
	-- Add the parameters for the stored procedure here
	@Id int
	,@CustomerId int
    ,@AccountNo varchar(200)
    ,@SortCode varchar(200)

AS
	SET NOCOUNT ON;

	Update BankAccounts
	set CustomerId = @CustomerId
	,AccountNo = @AccountNo
	,SortCode = @SortCode
	where Id = @Id
go


CREATE VIEW [dbo].[vwCMAddress]
AS
SELECT   Id
,CustomerId
,Street
,CityTown
,County
,PostCode
,Country
FROM         dbo.Addresses
GO

Create procedure spCMAddressDelete
@Id int
as
Delete from Addresses where Id = @Id

GO

Create PROCEDURE [dbo].[spCMAddressInsert] 
	-- Add the parameters for the stored procedure here
@CustomerId int
,@Street nvarchar(max)
,@CityTown  nvarchar(max)
,@County  nvarchar(max)
,@PostCode  nvarchar(max)
,@Country  nvarchar(max)

AS
	SET NOCOUNT ON;

	INSERT INTO [dbo].[Addresses]
			   (CustomerId
,Street
,CityTown
,County
,PostCode
,Country)
		 VALUES
			   (@CustomerId
				,@Street
				,@CityTown
				,@County
				,@PostCode
				,@Country)

Select * from [Addresses] where Id = @@IDENTITY

GO

CREATE PROCEDURE spCMAddressUpdate 
	-- Add the parameters for the stored procedure here
@Id int
,@CustomerId int
,@Street nvarchar(max)
,@CityTown  nvarchar(max)
,@County  nvarchar(max)
,@PostCode  nvarchar(max)
,@Country  nvarchar(max)

AS
	SET NOCOUNT ON;

	Update Addresses
	set CustomerId = @CustomerId
,Street = @Street
,CityTown = @CityTown
,County = @County
,PostCode = @PostCode
,Country = @Country
	where Id = @Id
go


CREATE VIEW [dbo].[vwCMTaxInfo]
AS
SELECT   Id
,TaxRegNo
,UTRNo
,CustomerId
FROM         dbo.TaxInfos
GO

Create procedure spCMTaxInfoDelete
@Id int
as
Delete from TaxInfos where Id = @Id

GO

Create PROCEDURE [dbo].[spCMTaxInfoInsert] 
	-- Add the parameters for the stored procedure here
@TaxRegNo nvarchar(max)
,@UTRNo nvarchar(max)
,@CustomerId int

AS
	SET NOCOUNT ON;

	INSERT INTO [dbo].[TaxInfos]
			   (TaxRegNo
,UTRNo
,CustomerId)
		 VALUES
			   (@TaxRegNo
,@UTRNo
,@CustomerId)

Select * from [TaxInfos] where Id = @@IDENTITY

GO

CREATE PROCEDURE spCMTaxInfoUpdate 
	-- Add the parameters for the stored procedure here
@Id int
,@TaxRegNo nvarchar(max)
,@UTRNo nvarchar(max)
,@CustomerId int

AS
	SET NOCOUNT ON;

	Update TaxInfos
	set TaxRegNo = @TaxRegNo
		,UTRNo = @UTRNo
		,CustomerId = @CustomerId
	where Id = @Id
go


CREATE VIEW [dbo].[vwCMPaymentBilling]
AS
SELECT   Id
,CustomerId
,PrefferedMethod
,Terms
,OpeningBalance
FROM         dbo.PaymentBillings
GO

Create procedure spCMPaymentBillingDelete
@Id int
as
Delete from PaymentBillings where Id = @Id

GO

Create PROCEDURE [dbo].[spCMPaymentBillingInsert] 
	-- Add the parameters for the stored procedure here
@CustomerId int
,@PrefferedMethod int
,@Terms nvarchar(max)
,@OpeningBalance float

AS
	SET NOCOUNT ON;

	INSERT INTO [dbo].[PaymentBillings]
			   (CustomerId
,PrefferedMethod
,Terms
,OpeningBalance)
		 VALUES
			   (@CustomerId
				,@PrefferedMethod
				,@Terms
				,@OpeningBalance)

Select * from [PaymentBillings] where Id = @@IDENTITY

GO

CREATE PROCEDURE spCMPaymentBillingUpdate 
	-- Add the parameters for the stored procedure here
@Id int
,@CustomerId int
,@PrefferedMethod int
,@Terms nvarchar(max)
,@OpeningBalance float

AS
	SET NOCOUNT ON;

	Update PaymentBillings
	set CustomerId = @CustomerId
,PrefferedMethod = @PrefferedMethod
,Terms = @Terms
,OpeningBalance = @OpeningBalance
	where Id = @Id
go


CREATE VIEW [dbo].[vwCMNote]
AS
SELECT   Id
,NoteText
,CustomerId
FROM         dbo.Notes
GO

Create procedure spCMNoteDelete
@Id int
as
Delete from Notes where Id = @Id

GO

Create PROCEDURE [dbo].[spCMNoteInsert] 
	-- Add the parameters for the stored procedure here
@NoteText varchar(max)
,@CustomerId int

AS
	SET NOCOUNT ON;

	INSERT INTO [dbo].[Notes]
			   (NoteText
			,CustomerId)
		 VALUES
			   (@NoteText
			,@CustomerId)

Select * from [Notes] where Id = @@IDENTITY

GO

CREATE PROCEDURE spCMNoteUpdate 
	-- Add the parameters for the stored procedure here
@Id int
,@NoteText varchar(max)
,@CustomerId int

AS
	SET NOCOUNT ON;

	Update Notes
	set NoteText = @NoteText
	,CustomerId = @CustomerId 
	where Id = @Id
go


