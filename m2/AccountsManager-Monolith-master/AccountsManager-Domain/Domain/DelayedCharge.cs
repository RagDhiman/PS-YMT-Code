using System;
using System.Collections.Generic;

namespace AccountsManager_Domain
{
    public class DelayedCharge
    {
        public int Id { get; set; }
        public int InvoiceId { get; set; }
        public int CustomerId { get; set; }
        public DateTime DelayedChargeDate { get; set; }
        public string Message { get; set; }
    }
}
