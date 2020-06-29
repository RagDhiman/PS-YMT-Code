using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AM_BackendForFrontend_Core;

namespace AM_BackendForFrontend_API.Models
{
    public class PaymentModel
    {
        public int Id { get; set; }
        public int InvoiceId { get; set; }

        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
        public DateTime PaymentDate { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        public string Memo { get; set; }
        public double AmountReceived { get; set; }
    }
}
