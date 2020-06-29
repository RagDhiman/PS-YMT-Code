using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace AccountsManager_Domain
{
    public class Customer
    {
        public int Id { get; set; }
        public int AccountId { get; set; }
        public Account Account { get; set; }
        public string Title { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Suffix { get; set; }
        public string Company { get; set; }
        public string DisplayNameAs { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Mobile { get; set; }
        public string Fax { get; set; }
        public string Website { get; set; }
        public bool CreditAgreement { get; set; } = false;
        public List<Address> Addresses { get; set; }
        public List<TaxInfo> TaxInfos { get; set; }
        public List<PaymentBilling> PaymentBillings { get; set; }
        public List<Note> Notes { get; set; }
        public List<BankAccount> BankAccounts { get; set; }
        public List<CreditNote> CreditNotes { get; set; }
        public List<DelayedCharge> DelayedCharges { get; set; }
        public List<Estimate> Estimates { get; set; }
        public List<Payment> Payments { get; set; }
        public List<SalesReceipt> SalesReceipts { get; set; }
        public List<Expense> Expenses { get; set; }
    }
}
