using AM_InvoiceManager_Core.Common;
using System;

namespace AM_InvoiceManager_API.Models
{
    public class SalesReceiptModel
    {
        public int Id { get; set; }
        public int InvoiceId { get; set; }

        public int BankAccountId { get; set; }
        public int CustomerId { get; set; }
        public DateTime SalesReceiptDate { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        public string ReferenceNo { get; set; }
        public string Message { get; set; }
    }
}
