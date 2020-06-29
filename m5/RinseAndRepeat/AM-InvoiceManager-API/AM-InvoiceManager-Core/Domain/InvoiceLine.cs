﻿using AM_InvoiceManager_Core.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace AM_InvoiceManager_Core
{
    public class InvoiceLine
    {
        public int Id { get; set; }
        public int InvoiceId { get; set; }
        public Invoice Invoice { get; set; }
        public ServiceType Service { get; set; }
        public double Quantity { get; set; }
        public double Rate { get; set; }
        public double VAT { get; set; }

    }
}
