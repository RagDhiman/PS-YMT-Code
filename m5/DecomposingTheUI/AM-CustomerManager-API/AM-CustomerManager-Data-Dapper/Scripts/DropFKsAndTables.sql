select 'ALTER TABLE ' + p.name + ' DROP CONSTRAINT ' + t.name from sys.objects t
inner join sys.objects p on t.parent_object_id = p.object_id 
where t.name like 'FK%_Customers_%'
or t.name like 'FK%_Addresses_%'
or t.name like 'FK%_BankAccounts_%'
or t.name like 'FK%_Notes_%'
or t.name like 'FK%_PaymentBillings_%'
or t.name like 'FK%_TaxInfos_%'




ALTER TABLE BankAccounts DROP CONSTRAINT FK_BankAccounts_Customers_CustomerId
ALTER TABLE CreditNoteLines DROP CONSTRAINT FK_CreditNoteLines_CreditNotes_CreditNoteId
ALTER TABLE CreditNotes DROP CONSTRAINT FK_CreditNotes_Customers_CustomerId
ALTER TABLE CreditNotes DROP CONSTRAINT FK_CreditNotes_Invoices_InvoiceId
ALTER TABLE Customers DROP CONSTRAINT FK_Customers_Accounts_AccountId
ALTER TABLE DelayedCharges DROP CONSTRAINT FK_DelayedCharges_Customers_CustomerId
ALTER TABLE Estimates DROP CONSTRAINT FK_Estimates_Customers_CustomerId
ALTER TABLE Expenses DROP CONSTRAINT FK_Expenses_Customers_CustomerId
ALTER TABLE Invoices DROP CONSTRAINT FK_Invoices_Customers_CustomerId
ALTER TABLE Notes DROP CONSTRAINT FK_Notes_Customers_CustomerId
ALTER TABLE PaymentBillings DROP CONSTRAINT FK_PaymentBillings_Customers_CustomerId
ALTER TABLE Payments DROP CONSTRAINT FK_Payments_Customers_CustomerId
ALTER TABLE SalesReceipts DROP CONSTRAINT FK_SalesReceipts_Customers_CustomerId
ALTER TABLE SupplierNotes DROP CONSTRAINT FK_SupplierNotes_Suppliers_SupplierId
ALTER TABLE TaxInfos DROP CONSTRAINT FK_TaxInfos_Customers_CustomerId

Drop table BankAccounts
Drop table Customers
Drop table Notes
Drop table PaymentBillings
Drop table TaxInfos
Drop table Addresses

--select 'Drop view ' + s.name from sys.views s

DROP VIEW [dbo].[vwCMAddress]
DROP VIEW [dbo].[vwCMBankAccount]
DROP VIEW [dbo].[vwCMCustomer]
DROP VIEW [dbo].[vwCMNote]
DROP VIEW [dbo].[vwCMPaymentBilling]
DROP VIEW [dbo].[vwCMTaxInfo]

--select 'Drop procedure ' + s.name from sys.procedures s

Drop procedure spCMCustomerDelete
Drop procedure spCMCustomerInsert
Drop procedure spCMCustomerUpdate
Drop procedure spCMBankAccountDelete
Drop procedure spCMBankAccountInsert
Drop procedure spCMBankAccountUpdate
Drop procedure spCMAddressDelete
Drop procedure spCMAddressInsert
Drop procedure spCMAddressUpdate
Drop procedure spCMTaxInfoDelete
Drop procedure spCMTaxInfoInsert
Drop procedure spCMTaxInfoUpdate
Drop procedure spCMPaymentBillingDelete
Drop procedure spCMPaymentBillingInsert
Drop procedure spCMPaymentBillingUpdate
Drop procedure spCMNoteDelete
Drop procedure spCMNoteInsert
Drop procedure spCMNoteUpdate