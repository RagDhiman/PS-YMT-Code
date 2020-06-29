Create View vwIMCreditNote
as

Select *
from CreditNotes

Go

--==========================================

Create View vwIMCredits
as

Select *
from Credits

Go

--==========================================

Create View vwIMDelayedChargeLines
as

Select *
from DelayedChargeLines

Go

--==========================================

Create View vwIMDelayedCharges
as

Select *
from DelayedCharges

Go

--==========================================

Create View vwIMEstimateLines
as

Select *
from EstimateLines

Go

--==========================================

Create View vwIMEstimates
as

Select *
from Estimates

Go

--==========================================

Create View vwIMInvoiceLines
as

Select *
from InvoiceLines

Go


--==========================================

Create View vwIMInvoices
as

Select *
from Invoices

Go

--==========================================

Create View vwIMPayments
as

Select *
from Payments

Go

--==========================================

Create View vwIMSalesReceiptLines
as

Select *
from SalesReceiptLines

Go


--==========================================

Create View vwIMSalesReceipts
as

Select *
from SalesReceipts

Go

--==========================================

Create procedure spIMCreditNoteDelete
@Id int
as
Delete from CreditNotes where Id = @Id

GO

--==========================================

Create Procedure spIMCreditNoteInsert
@InvoiceId	int,
@CustomerId	int,
@CreditNoteDate	datetime2,
@Message	nvarchar(max)
as

SET NOCOUNT ON;

Insert into CreditNotes (InvoiceId,
CustomerId,
CreditNoteDate,
Message)
VALUES (@InvoiceId,
		@CustomerId,
		@CreditNoteDate,
		@Message)

Select * from [CreditNotes] where Id = @@IDENTITY

GO

--==========================================

Create Procedure spIMCreditNoteUpdate
@Id	int,
@InvoiceId	int,
@CustomerId	int,
@CreditNoteDate	datetime2,
@Message	nvarchar(max)
as

SET NOCOUNT ON;

Update CreditNotes
Set InvoiceId = @InvoiceId,
CustomerId	= @CustomerId,
CreditNoteDate	= @CreditNoteDate,
Message = @Message
where Id = @Id

GO

--==========================================

Create procedure spIMCreditDelete
@Id int
as
Delete from Credits where Id = @Id

GO

--==========================================

Create Procedure spIMCreditInsert
@InvoiceId	int,
@CreditDate	datetime2,
@CreditAmount	float,
@ProductCredit	int,
@CustomerName	nvarchar,
@AccountNo	nvarchar,
@SortCode	nvarchar,
@HasCreditAgreement	bit
as

SET NOCOUNT ON;

Insert into Credits (InvoiceId,
CreditDate,
CreditAmount,
ProductCredit,
CustomerName,
AccountNo,
SortCode,
HasCreditAgreement)
VALUES (@InvoiceId,
@CreditDate,
@CreditAmount,
@ProductCredit,
@CustomerName,
@AccountNo,
@SortCode,
@HasCreditAgreement)

Select * from [Credits] where Id = @@IDENTITY

GO

--==========================================

Create Procedure spIMCreditUpdate
@Id	int,
@InvoiceId	int,
@CreditDate	datetime2,
@CreditAmount	float,
@ProductCredit	int,
@CustomerName	nvarchar,
@AccountNo	nvarchar,
@SortCode	nvarchar,
@HasCreditAgreement	bit
as

SET NOCOUNT ON;

Update Credits
Set InvoiceId = @InvoiceId,
CreditDate = @CreditDate,
CreditAmount = @CreditAmount,
ProductCredit = @ProductCredit,
CustomerName = @CustomerName,
AccountNo = @AccountNo,
SortCode = @SortCode,
HasCreditAgreement = @HasCreditAgreement
where Id = @Id

GO

--==========================================

--==========================================

Create procedure spIMDelayedChargeDelete
@Id int
as
Delete from DelayedCharges where Id = @Id

GO

--==========================================

Create Procedure spIMDelayedChargeInsert
@InvoiceId	int,
@CustomerId	int,
@DelayedChargeDate	datetime2,
@Message	nvarchar
as

SET NOCOUNT ON;

Insert into DelayedCharges (InvoiceId,
CustomerId,
DelayedChargeDate,
Message)
VALUES (@InvoiceId,
@CustomerId,
@DelayedChargeDate,
@Message)

Select * from [DelayedCharges] where Id = @@IDENTITY

GO

--==========================================

Create Procedure spIMDelayedChargeUpdate
@Id	int,
@InvoiceId	int,
@CustomerId	int,
@DelayedChargeDate	datetime2,
@Message	nvarchar
as

SET NOCOUNT ON;

Update DelayedCharges
Set InvoiceId = @InvoiceId,
CustomerId = @CustomerId,
DelayedChargeDate = @DelayedChargeDate,
Message = @Message
where Id = @Id

GO

--==========================================

--==========================================

Create procedure spIMEstimateDelete
@Id int
as
Delete from Estimates where Id = @Id

GO

--==========================================

Create Procedure spIMEstimateInsert
@InvoiceId	int,
@CustomerId	int,
@EstimateDate	datetime2,
@ExpirationDate	datetime2,
@Message	nvarchar
as

SET NOCOUNT ON;

Insert into Estimates (InvoiceId,
CustomerId,
EstimateDate,
ExpirationDate,
Message)
VALUES (@InvoiceId,
@CustomerId,
@EstimateDate,
@ExpirationDate,
@Message)

Select * from [Estimates] where Id = @@IDENTITY

GO

--==========================================

Create Procedure spIMEstimateUpdate
@Id	int,
@InvoiceId	int,
@CustomerId	int,
@EstimateDate	datetime2,
@ExpirationDate	datetime2,
@Message	nvarchar
as

SET NOCOUNT ON;

Update Estimates
Set InvoiceId = @InvoiceId,
CustomerId = @CustomerId,
EstimateDate = @EstimateDate,
ExpirationDate = @ExpirationDate,
Message = @Message
where Id = @Id

GO

--==========================================

--==========================================

Create procedure spIMInvoiceLineDelete
@Id int
as
Delete from InvoiceLines where Id = @Id

GO

--==========================================

Create Procedure spIMInvoiceLineInsert
@InvoiceId	int,
@Service	int,
@Quantity	float,
@Rate	float,
@VAT	float
as

SET NOCOUNT ON;

Insert into InvoiceLines (InvoiceId,
Service,
Quantity,
Rate,
VAT)
VALUES (@InvoiceId,
@Service,
@Quantity,
@Rate,
@VAT)

Select * from [InvoiceLines] where Id = @@IDENTITY

GO

--==========================================

Create Procedure spIMInvoiceLineUpdate
@Id	int,
@InvoiceId	int,
@Service	int,
@Quantity	float,
@Rate	float,
@VAT	float
as

SET NOCOUNT ON;

Update InvoiceLines
Set InvoiceId = @InvoiceId,
Service = @Service,
Quantity = @Quantity,
Rate = @Rate,
VAT = @VAT
where Id = @Id

GO

--==========================================

--==========================================

Create procedure spIMInvoiceDelete
@Id int
as
Delete from Invoices where Id = @Id

GO

--==========================================

Create Procedure spIMInvoiceInsert
@CustomerId	int,
@InvoiceDate	datetime2,
@DueDate	datetime2,
@Message	nvarchar
as

SET NOCOUNT ON;

Insert into Invoices (CustomerId,
InvoiceDate,
DueDate,
Message)
VALUES (@CustomerId,
@InvoiceDate,
@DueDate,
@Message)

Select * from [Invoices] where Id = @@IDENTITY

GO

--==========================================

Create Procedure spIMInvoiceUpdate
@Id	int,
@CustomerId	int,
@InvoiceDate	datetime2,
@DueDate	datetime2,
@Message	nvarchar
as

SET NOCOUNT ON;

Update Invoices
Set CustomerId = @CustomerId,
InvoiceDate = @InvoiceDate,
DueDate = @DueDate,
Message = @Message
where Id = @Id

GO

--==========================================


--==========================================

Create procedure spIMPaymentDelete
@Id int
as
Delete from Payments where Id = @Id

GO

--==========================================

Create Procedure spIMPaymentInsert
@InvoiceId	int,
@CustomerId	int,
@PaymentDate	datetime2,
@PaymentMethod	int,
@Memo	nvarchar,
@AmountReceived	float
as

SET NOCOUNT ON;

Insert into Payments (InvoiceId,
CustomerId,
PaymentDate,
PaymentMethod,
Memo,
AmountReceived)
VALUES (@InvoiceId,
@CustomerId,
@PaymentDate,
@PaymentMethod,
@Memo,
@AmountReceived)

Select * from [Payments] where Id = @@IDENTITY

GO

--==========================================

Create Procedure spIMPaymentUpdate
@Id	int,
@InvoiceId	int,
@CustomerId	int,
@PaymentDate	datetime2,
@PaymentMethod	int,
@Memo	nvarchar,
@AmountReceived	float
as

SET NOCOUNT ON;

Update Payments
Set InvoiceId = @InvoiceId,
CustomerId = @CustomerId,
PaymentDate = @PaymentDate,
PaymentMethod = @PaymentMethod,
Memo = @Memo,
AmountReceived = @AmountReceived
where Id = @Id

GO

--==========================================


--==========================================

Create procedure spIMSalesReceiptDelete
@Id int
as
Delete from SalesReceipts where Id = @Id

GO

--==========================================

Create Procedure spIMSalesReceiptInsert
@InvoiceId	int,
@BankAccountId	int,
@CustomerId	int,
@SalesReceiptDate	datetime2,
@PaymentMethod	int,
@ReferenceNo	nvarchar,
@Message	nvarchar
as

SET NOCOUNT ON;

Insert into SalesReceipts (InvoiceId,
BankAccountId,
CustomerId,
SalesReceiptDate,
PaymentMethod,
ReferenceNo,
Message)
VALUES (@InvoiceId,
@BankAccountId,
@CustomerId,
@SalesReceiptDate,
@PaymentMethod,
@ReferenceNo,
@Message)

Select * from [SalesReceipts] where Id = @@IDENTITY

GO

--==========================================

Create Procedure spIMSalesReceiptUpdate
@Id	int,
@InvoiceId	int,
@BankAccountId	int,
@CustomerId	int,
@SalesReceiptDate	datetime2,
@PaymentMethod	int,
@ReferenceNo	nvarchar,
@Message	nvarchar
as

SET NOCOUNT ON;

Update SalesReceipts
Set InvoiceId = @InvoiceId,
BankAccountId = @BankAccountId,
CustomerId = @CustomerId,
SalesReceiptDate = @SalesReceiptDate,
PaymentMethod = @PaymentMethod,
ReferenceNo = @ReferenceNo,
Message = @Message
where Id = @Id

GO

--==========================================




