--==========================================

Create View vwExMExpenses
as

Select *
from Expenses

Go

--==========================================

Create View vwExMExpenseLines
as

Select *
from ExpenseLines

Go

--==========================================
Create Procedure spExMExpensesInsert
@PayeeName	nvarchar,
@CustomerId	int,
@SupplierId	int,
@EmployeeId	int,
@PaymentDate	datetime2,
@PaymentMethod	int,
@BankAccountId	int,
@Reference	nvarchar,
@Notes	nvarchar
as

SET NOCOUNT ON;

Insert into Expenses(PayeeName,
CustomerId,
SupplierId,
EmployeeId,
PaymentDate,
PaymentMethod,
BankAccountId,
Reference,
Notes)
VALUES (@PayeeName,
@CustomerId,
@SupplierId,
@EmployeeId,
@PaymentDate,
@PaymentMethod,
@BankAccountId,
@Reference,
@Notes)

Select * from [Expenses] where Id = @@IDENTITY

GO

--==========================================

Create Procedure spExMExpensesUpdate
@Id	int,
@PayeeName	nvarchar,
@CustomerId	int,
@SupplierId	int,
@EmployeeId	int,
@PaymentDate	datetime2,
@PaymentMethod	int,
@BankAccountId	int,
@Reference	nvarchar,
@Notes	nvarchar
as

SET NOCOUNT ON;

Update Expenses
Set PayeeName = @PayeeName,
CustomerId = @CustomerId,
SupplierId = @SupplierId,
EmployeeId = @EmployeeId,
PaymentDate = @PaymentDate,
PaymentMethod = @PaymentMethod,
BankAccountId = @BankAccountId,
Reference = @Reference,
Notes = @Notes
where Id = @Id

GO

--==========================================

Create procedure spExMExpensesDelete
@Id int
as
Delete from Expenses where Id = @Id

GO

--========================================

Create Procedure spExMExpenseLinesInsert
@ExpenseId	int,
@ServiceType	int,
@Description	nvarchar,
@Amount	float,
@VAT	float
as

SET NOCOUNT ON;

Insert into ExpenseLines(ExpenseId,
ServiceType,
Description,
Amount,
VAT)
VALUES (@ExpenseId,
@ServiceType,
@Description,
@Amount,
@VAT)

Select * from [ExpenseLines] where Id = @@IDENTITY

GO

--==========================================

Create Procedure spExMExpenseLinesUpdate
@Id	int,
@ExpenseId	int,
@ServiceType	int,
@Description	nvarchar,
@Amount	float,
@VAT	float
as

SET NOCOUNT ON;

Update ExpenseLines
Set ExpenseId = @ExpenseId,
ServiceType = @ServiceType,
Description = @Description,
Amount = @Amount,
VAT = @VAT
where Id = @Id

GO

--==========================================

Create procedure spExMExpenseLinesDelete
@Id int
as
Delete from ExpenseLines where Id = @Id

GO

