select 'ALTER TABLE ' + p.name + ' DROP CONSTRAINT ' + t.name from sys.objects t
inner join sys.objects p on t.parent_object_id = p.object_id 
where t.name like 'FK%_CreditNotes_%'
or t.name like 'FK%_Credit_%'
or t.name like 'FK%_DelayedCharge_%'
or t.name like 'FK%_Estimate_%'
or t.name like 'FK%_Invoices_%'
or t.name like 'FK%_Payments_%'
or t.name like 'FK%_SalesReceipts_%'

ALTER TABLE Credits DROP CONSTRAINT FK_Credits_Invoices_InvoiceId
ALTER TABLE DelayedChargeLines DROP CONSTRAINT FK_DelayedChargeLines_DelayedCharges_DelayedChargeId
ALTER TABLE DelayedCharges DROP CONSTRAINT FK_DelayedCharges_Invoices_InvoiceId
ALTER TABLE EstimateLines DROP CONSTRAINT FK_EstimateLines_Estimates_EstimateId
ALTER TABLE Estimates DROP CONSTRAINT FK_Estimates_Invoices_InvoiceId
ALTER TABLE InvoiceLines DROP CONSTRAINT FK_InvoiceLines_Invoices_InvoiceId
ALTER TABLE Payments DROP CONSTRAINT FK_Payments_Invoices_InvoiceId
ALTER TABLE SalesReceiptLines DROP CONSTRAINT FK_SalesReceiptLines_SalesReceipts_SalesReceiptId
ALTER TABLE SalesReceipts DROP CONSTRAINT FK_SalesReceipts_Invoices_InvoiceId


drop table [dbo].[Credits]
drop table [dbo].[CreditNotes]
drop table [dbo].[CreditNoteLines]
drop table [dbo].[DelayedCharges]
drop table [dbo].[DelayedChargeLines]
drop table [dbo].[Estimates]
drop table [dbo].[EstimateLines]
drop table [dbo].[Invoices]
drop table [dbo].[InvoiceLines]
drop table [dbo].[Payments]
drop table [dbo].[SalesReceipts]
drop table [dbo].[SalesReceiptLines]
