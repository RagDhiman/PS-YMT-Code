using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AccountsManager_Domain;

namespace AccountsManager_Domain
{
    public class Payment
    {
        public int Id { get; set; }
        public int InvoiceId { get; set; }
        public Invoice Invoice { get; set; }
        public int? CustomerId { get; set; }
        public Customer Customer { get; set; }
        public DateTime PaymentDate { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        public string Memo { get; set; }
        public double AmountReceived { get; set; }
    }
}
