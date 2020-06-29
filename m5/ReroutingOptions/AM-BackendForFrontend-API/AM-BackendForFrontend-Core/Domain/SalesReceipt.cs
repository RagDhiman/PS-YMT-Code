using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AM_BackendForFrontend_Data.Generic;

namespace AM_BackendForFrontend_Core
{
    public class SalesReceipt : IEntity
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
