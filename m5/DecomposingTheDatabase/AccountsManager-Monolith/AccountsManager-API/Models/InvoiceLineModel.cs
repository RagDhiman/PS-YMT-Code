﻿using AccountsManager_Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace AccountsManager_API.Models
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
