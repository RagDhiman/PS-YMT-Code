select 'ALTER TABLE ' + p.name + ' DROP CONSTRAINT ' + t.name from sys.objects t
inner join sys.objects p on t.parent_object_id = p.object_id 
where t.name like 'FK%_Expenses_%'
or t.name like 'FK%_ExpenseLines_%'

ALTER TABLE ExpenseLines DROP CONSTRAINT FK_ExpenseLines_Expenses_ExpenseId
ALTER TABLE Expenses DROP CONSTRAINT FK_Expenses_Employees_EmployeeId
ALTER TABLE Expenses DROP CONSTRAINT FK_Expenses_Suppliers_SupplierId

Drop table ExpenseLines
Drop table Expenses