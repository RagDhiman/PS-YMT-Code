using AM_InvoiceManager_Core;
using AM_InvoiceManager_Core.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AM_InvoiceManager_API.Models
{
    public class SalesReceiptLineModel
    {
        public int Id { get; set; }
        public int SalesReceiptId { get; set; }
        public ServiceType Service { get; set; }
        public double Quantity { get; set; }
        public double Rate { get; set; }
        public double VAT { get; set; }
    }
}
