using System;
using System.Collections.Generic;
using System.Text;

namespace AM_InvoiceManager_Core
{
    public class Invoice
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public DateTime InvoiceDate { get; set; }
        public DateTime DueDate { get; set; }
        public string Message { get; set; }
        public List<InvoiceLine> InvoiceLines { get; set; }
        public List<Credit> Credits { get; set; }
        public List<CreditNote> CreditNotes { get; set; }
        public List<DelayedCharge> DelayedCharges { get; set; }
        public List<Payment> Payments { get; set; }
        public List<SalesReceipt> SalesReceipts { get; set; }
        public List<Estimate> Estimates { get; set; }

    }
}
