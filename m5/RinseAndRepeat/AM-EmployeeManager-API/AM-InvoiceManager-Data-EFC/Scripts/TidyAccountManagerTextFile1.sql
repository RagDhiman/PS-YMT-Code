select 'ALTER TABLE ' + p.name + ' DROP CONSTRAINT ' + t.name from sys.objects t
inner join sys.objects p on t.parent_object_id = p.object_id 
where t.name like 'FK%_Absence_%'
or t.name like 'FK%_Employee_%'
or t.name like 'FK%_Equipment_%'
or t.name like 'FK%_Holiday_%'
or t.name like 'FK%_Pay_%'
or t.name like 'FK%_TaxInformation_%'
or t.name like 'FK%_Training_%'

ALTER TABLE Absences DROP CONSTRAINT FK_Absences_Employees_EmployeeId
ALTER TABLE Equipments DROP CONSTRAINT FK_Equipments_Employees_EmployeeId
ALTER TABLE Holidays DROP CONSTRAINT FK_Holidays_Employees_EmployeeId
ALTER TABLE Pays DROP CONSTRAINT FK_Pays_Employees_EmployeeId
ALTER TABLE TaxInformations DROP CONSTRAINT FK_TaxInformations_Employees_EmployeeId
ALTER TABLE Trainings DROP CONSTRAINT FK_Trainings_Employees_EmployeeId

drop table [dbo].[Absences]
drop table [dbo].[Employees]
drop table [dbo].[Equipments]
drop table [dbo].[Holidays]
drop table [dbo].[Pays]
drop table [dbo].[TaxInformations]
drop table [dbo].[Trainings]