Alter table Customers
Add ParentCompany nvarchar(MAX) null

GO

ALTER VIEW [dbo].[vwCMCustomer]
AS
SELECT   Id, AccountId
,Title
,FirstName
,MiddleName
,LastName
,Suffix
,Company
,ParentCompany
,DisplayNameAs
,Email
,Phone
,Mobile
,Fax
,Website
,CreditAgreement
FROM         dbo.Customers
GO




ALTER PROCEDURE [dbo].[spCMCustomerUpdate] 
	-- Add the parameters for the stored procedure here
@Id	int
,@AccountId int
,@Title nvarchar(max)
,@FirstName nvarchar(max)
,@MiddleName nvarchar(max)
,@LastName	nvarchar(max)
,@Suffix	nvarchar(max)
,@Company	nvarchar(max)
,@ParentCompany	nvarchar(max)
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
	,ParentCompany = @ParentCompany
	,DisplayNameAs = @DisplayNameAs
	,Email = @Email
	,Phone = @Phone
	,Mobile = @Mobile
	,Fax = @Fax
	,Website = @Website
	,CreditAgreement = @CreditAgreement
	where Id = @Id


GO

ALTER PROCEDURE [dbo].[spCMCustomerInsert] 
	-- Add the parameters for the stored procedure here
@AccountId int
,@Title nvarchar(max)
,@FirstName nvarchar(max)
,@MiddleName nvarchar(max)
,@LastName	nvarchar(max)
,@Suffix	nvarchar(max)
,@Company	nvarchar(max)
,@ParentCompany	nvarchar(max)
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
,ParentCompany
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
,@ParentCompany
,@DisplayNameAs
,@Email
,@Phone
,@Mobile
,@Fax
,@Website
,@CreditAgreement)

Select * from [Customers] where Id = @@IDENTITY

GO