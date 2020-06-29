using System;
using System.Collections.Generic;

namespace AccountsManager_UI_Web.Data.DTOs
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
