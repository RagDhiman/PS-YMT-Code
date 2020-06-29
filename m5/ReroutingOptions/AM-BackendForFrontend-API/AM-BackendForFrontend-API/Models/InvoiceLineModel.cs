using AM_BackendForFrontend_Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace AM_BackendForFrontend_API.Models
{
    public class InvoiceLineModel
    {
        public int Id { get; set; }
        public int InvoiceId { get; set; }
        public ServiceType Service { get; set; }
        public double Quantity { get; set; }
        public double Rate { get; set; }
        public double VAT { get; set; }

    }
}
