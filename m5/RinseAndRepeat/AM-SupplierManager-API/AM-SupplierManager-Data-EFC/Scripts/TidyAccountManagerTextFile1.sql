select 'ALTER TABLE ' + p.name + ' DROP CONSTRAINT ' + t.name from sys.objects t
inner join sys.objects p on t.parent_object_id = p.object_id 
where t.name like 'FK%_Suppliers_%'
or t.name like 'FK%_SupplierNotes_%'
or t.name like 'FK%_Attachments_%'

ALTER TABLE SupplierNotes DROP CONSTRAINT FK_SupplierNotes_Suppliers_SupplierId
ALTER TABLE Suppliers DROP CONSTRAINT FK_Suppliers_Employees_EmployeeId
ALTER TABLE Suppliers DROP CONSTRAINT FK_Suppliers_Suppliers_SupplierId
ALTER TABLE Attachments DROP CONSTRAINT FK_Attachments_Suppliers_SupplierId
ALTER TABLE Suppliers DROP CONSTRAINT FK_Suppliers_Accounts_AccountId


Drop table SupplierNotes
Drop table Suppliers
Drop table Attachments