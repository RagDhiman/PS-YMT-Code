--==========================================

Create View vwEMAbsences
as

Select *
from [Absences]

Go

--==========================================

Create View vwEMEmployees
as

Select *
from [Employees]

Go

--==========================================

Create View vwEMEquipments
as

Select *
from [Equipments]

Go

--==========================================

Create View vwHolidays
as

Select *
from [Holidays]

Go

--==========================================

Create View vwPays
as

Select *
from [Pays]

Go

--==========================================

Create View vwTaxInformations
as

Select *
from [TaxInformations]

Go

--==========================================
Create View vwTrainings
as

Select *
from [Trainings]

Go

--==========================================

Create Procedure spEMAbsencesInsert
@EmployeeId	int,
@StartDateTime	datetime2,
@EndDateTime	datetime2,
@Description	nvarchar,
@Notes	nvarchar,
@Paid	bit
as

SET NOCOUNT ON;

Insert into Absencess (EmployeeId,
			StartDateTime,
			EndDateTime,
			Description,
			Notes,
			Paid)
VALUES (@EmployeeId,
			@StartDateTime,
			@EndDateTime,
			@Description,
			@Notes,
			@Paid)

Select * from [Absences] where Id = @@IDENTITY

GO

--==========================================

Create Procedure spEMAbsencesUpdate
@Id	int,
@EmployeeId	int,
@StartDateTime	datetime2,
@EndDateTime	datetime2,
@Description	nvarchar,
@Notes	nvarchar,
@Paid	bit
as

SET NOCOUNT ON;

Update Absences
Set EmployeeId = @EmployeeId,
StartDateTime = @StartDateTime,
EndDateTime = @EndDateTime,
Description = @Description,
Notes = @Notes,
Paid = @Paid
where Id = @Id

GO

--==========================================

Create procedure spEMAbsencesDelete
@Id int
as
Delete from Absences where Id = @Id

GO


--==========================================

Create Procedure spEMEmployeesInsert
@AccountId	int,
@Title	nvarchar,
@FirstName	nvarchar,
@LastName	nvarchar,
@DisplayNameAs	nvarchar,
@Address	nvarchar,
@Notes	nvarchar,
@Email	nvarchar,
@Phone	nvarchar,
@Mobile	nvarchar,
@DOB	datetime2
as

SET NOCOUNT ON;

Insert into Employeess (AccountId,
Title,
FirstName,
LastName,
DisplayNameAs,
Address,
Notes,
Email,
Phone,
Mobile,
DOB)
VALUES (@AccountId,
@Title,
@FirstName,
@LastName,
@DisplayNameAs,
@Address,
@Notes,
@Email,
@Phone,
@Mobile,
@DOB)

Select * from [Employees] where Id = @@IDENTITY

GO

--==========================================

Create Procedure spEMEmployeesUpdate
@Id	int,
@AccountId	int,
@Title	nvarchar,
@FirstName	nvarchar,
@LastName	nvarchar,
@DisplayNameAs	nvarchar,
@Address	nvarchar,
@Notes	nvarchar,
@Email	nvarchar,
@Phone	nvarchar,
@Mobile	nvarchar,
@DOB	datetime2
as

SET NOCOUNT ON;

Update Employees
Set AccountId = @AccountId,
Title = @Title,
FirstName = @FirstName,
LastName = @LastName,
DisplayNameAs = @DisplayNameAs,
Address = @Address,
Notes = @Notes,
Email = @Email,
Phone = @Phone,
Mobile = @Mobile,
DOB = @DOB
where Id = @Id

GO

--==========================================

Create procedure spEMEmployeesDelete
@Id int
as
Delete from Employees where Id = @Id

GO

--==========================================

Create Procedure spEMEquipmentsInsert
@EmployeeId	int,
@LoanStartDateTime	datetime2,
@LoanEndDateTime	datetime2,
@Reference	nvarchar,
@Name	nvarchar,
@ExpectedReturnDate	datetime2
as

SET NOCOUNT ON;

Insert into Equipments (EmployeeId,
LoanStartDateTime,
LoanEndDateTime,
Reference,
Name,
ExpectedReturnDate)
VALUES (@EmployeeId,
@LoanStartDateTime,
@LoanEndDateTime,
@Reference,
@Name,
@ExpectedReturnDate)

Select * from [Equipments] where Id = @@IDENTITY

GO

--==========================================

Create Procedure spEMEquipmentsUpdate
@Id	int,
@EmployeeId	int,
@LoanStartDateTime	datetime2,
@LoanEndDateTime	datetime2,
@Reference	nvarchar,
@Name	nvarchar,
@ExpectedReturnDate	datetime2
as

SET NOCOUNT ON;

Update Equipments
Set EmployeeId = @EmployeeId,
LoanStartDateTime = @LoanStartDateTime,
LoanEndDateTime = @LoanEndDateTime,
Reference = @Reference,
Name = @Name,
ExpectedReturnDate = @ExpectedReturnDate
where Id = @Id

GO

--==========================================

Create procedure spEMEquipmentsDelete
@Id int
as
Delete from Equipments where Id = @Id

GO

--==========================================

create Procedure spEMHolidaysInsert
@EmployeeId	int,
@StartDateTime	datetime2,
@EndDateTime	datetime2,
@OnCall	bit,
@OnCallRateMultiplier	int,
@Paid	bit
as

SET NOCOUNT ON;

Insert into Holidays (EmployeeId,
StartDateTime,
EndDateTime,
OnCall,
OnCallRateMultiplier,
Paid)
VALUES (@EmployeeId,
@StartDateTime,
@EndDateTime,
@OnCall,
@OnCallRateMultiplier,
@Paid)

Select * from [Holidays] where Id = @@IDENTITY

GO

--==========================================

Create Procedure spEMHolidaysUpdate
@Id	int,
@EmployeeId	int,
@StartDateTime	datetime2,
@EndDateTime	datetime2,
@OnCall	bit,
@OnCallRateMultiplier	int,
@Paid	bit
as

SET NOCOUNT ON;

Update Holidays
Set EmployeeId = @EmployeeId,
StartDateTime = @StartDateTime,
EndDateTime = @EndDateTime,
OnCall = @OnCall,
OnCallRateMultiplier = @OnCallRateMultiplier,
Paid = Paid
where Id = @Id

GO

--==========================================

Create procedure spEMHolidaysDelete
@Id int
as
Delete from Holidays where Id = @Id

GO


--==========================================

create Procedure spEMPaysInsert
@HourlyRate	float,
@EmployeeId	int,
@DefaultRate	bit,
@StartTime	datetime2,
@EndTime	datetime2
as

SET NOCOUNT ON;

Insert into Pays (HourlyRate,
EmployeeId,
DefaultRate,
StartTime,
EndTime)
VALUES (@HourlyRate,
@EmployeeId,
@DefaultRate,
@StartTime,
@EndTime)

Select * from [Pays] where Id = @@IDENTITY

GO

--==========================================

Create Procedure spEMPaysUpdate
@Id	int,
@HourlyRate	float,
@EmployeeId	int,
@DefaultRate	bit,
@StartTime	datetime2,
@EndTime	datetime2
as

SET NOCOUNT ON;

Update Pays
Set HourlyRate = @HourlyRate,
EmployeeId = @EmployeeId,
DefaultRate = @DefaultRate,
StartTime = @StartTime,
EndTime = @EndTime
where Id = @Id

GO

--==========================================

Create procedure spEMPaysDelete
@Id int
as
Delete from Pays where Id = @Id

GO

--==========================================

create Procedure spEMTaxInformationsInsert
@EmployeeId	int,
@TaxCode	nvarchar,
@VAT	bit,
@VATRef	nvarchar
as

SET NOCOUNT ON;

Insert into TaxInformations (EmployeeId,
TaxCode,
VAT,
VATRef)
VALUES (@EmployeeId,
@TaxCode,
@VAT,
@VATRef)

Select * from [TaxInformations] where Id = @@IDENTITY

GO

--==========================================

Create Procedure spEMTaxInformationsUpdate
@Id	int,
@EmployeeId	int,
@TaxCode	nvarchar,
@VAT	bit,
@VATRef	nvarchar
as

SET NOCOUNT ON;

Update TaxInformations
Set EmployeeId = @EmployeeId,
TaxCode = @TaxCode,
VAT = @VAT,
VATRef = @VATRef
where Id = @Id

GO

--==========================================

Create procedure spEMTaxInformationsDelete
@Id int
as
Delete from TaxInformations where Id = @Id

GO

--==========================================

create Procedure spEMTrainingsInsert
@EmployeeId	int,
@StartDateTime	datetime2,
@EndDateTime	datetime2,
@Description	nvarchar,
@Name	nvarchar,
@Certification	bit,
@CertificationName	nvarchar
as

SET NOCOUNT ON;

Insert into Trainings (EmployeeId,
StartDateTime,
EndDateTime,
Description,
Name,
Certification,
CertificationName)
VALUES (@EmployeeId,
@StartDateTime,
@EndDateTime,
@Description,
@Name,
@Certification,
@CertificationName)

Select * from [Trainings] where Id = @@IDENTITY

GO

--==========================================

Create Procedure spEMTrainingsUpdate
@Id	int,
@EmployeeId	int,
@StartDateTime	datetime2,
@EndDateTime	datetime2,
@Description	nvarchar,
@Name	nvarchar,
@Certification	bit,
@CertificationName	nvarchar
as

SET NOCOUNT ON;

Update Trainings
Set EmployeeId = @EmployeeId,
StartDateTime = @StartDateTime,
EndDateTime = @EndDateTime,
Description = @Description,
Name = @Name,
Certification = @Certification,
CertificationName = @CertificationName
where Id = @Id

GO

--==========================================

Create procedure spEMTrainingsDelete
@Id int
as
Delete from Trainings where Id = @Id

GO



