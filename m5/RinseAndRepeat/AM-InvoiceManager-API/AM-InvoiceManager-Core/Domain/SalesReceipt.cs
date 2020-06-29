using AM_InvoiceManager_Core.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AM_InvoiceManager_Core
{
    public class SalesReceipt
    {
        public int Id { get; set; }
        public int InvoiceId { get; set; }
        public Invoice Invoice { get; set; }
        public int BankAccountId { get; set; }
        public int? CustomerId { get; set; }
        public DateTime SalesReceiptDate { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        public string ReferenceNo { get; set; }
        public string Message { get; set; }
    }
}
