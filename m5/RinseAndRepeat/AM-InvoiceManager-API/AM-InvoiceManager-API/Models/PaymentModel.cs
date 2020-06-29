using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AM_InvoiceManager_Core;
using AM_InvoiceManager_Core.Common;

namespace AM_InvoiceManager_API.Models
{
    public class PaymentModel
    {
        public int Id { get; set; }
        public int InvoiceId { get; set; }

        public int CustomerId { get; set; }
        public DateTime PaymentDate { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        public string Memo { get; set; }
        public double AmountReceived { get; set; }
    }
}
