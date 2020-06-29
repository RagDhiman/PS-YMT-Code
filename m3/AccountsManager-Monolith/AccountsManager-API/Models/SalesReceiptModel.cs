using AccountsManager_Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountsManager_API.Models
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
