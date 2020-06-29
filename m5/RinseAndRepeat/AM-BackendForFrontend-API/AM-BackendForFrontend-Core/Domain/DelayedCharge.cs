using System;
using System.Collections.Generic;
using AM_BackendForFrontend_Data.Generic;

namespace AM_BackendForFrontend_Core
{
    public class DelayedCharge : IEntity
    {
        public int Id { get; set; }
        public int InvoiceId { get; set; }
        public int CustomerId { get; set; }
        public DateTime DelayedChargeDate { get; set; }
        public string Message { get; set; }
    }
}
