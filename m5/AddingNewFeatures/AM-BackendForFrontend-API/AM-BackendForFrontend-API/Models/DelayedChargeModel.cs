using System;
using System.Collections.Generic;

namespace AM_BackendForFrontend_API.Models
{
    public class DelayedChargeModel
    {
        public int Id { get; set; }
        public int InvoiceId { get; set; }
        public int CustomerId { get; set; }
        public DateTime DelayedChargeDate { get; set; }
        public string Message { get; set; }
    }
}
