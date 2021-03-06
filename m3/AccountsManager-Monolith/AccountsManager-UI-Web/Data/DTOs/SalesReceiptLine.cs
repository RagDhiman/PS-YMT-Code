﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountsManager_UI_Web.Data.DTOs
{
    public class SalesReceiptLine : IEntity
    {
        public int Id { get; set; }
        public int SalesReceiptId { get; set; }
        public SalesReceipt SalesReceipt { get; set; }
        public ServiceType Service { get; set; }
        public double Quantity { get; set; }
        public double Rate { get; set; }
        public double VAT { get; set; }
    }
}
