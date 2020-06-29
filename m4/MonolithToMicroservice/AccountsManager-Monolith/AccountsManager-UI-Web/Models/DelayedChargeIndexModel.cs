using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AccountsManager_UI_Web.Models
{
    public class DelayedChargeIndexModel
    {
        public int Id { get; set; }
        [Display(Name = "Invoice Ref.")]
        public int InvoiceId { get; set; }
        [Display(Name = "Customer Ref.")]
        public int CustomerId { get; set; }
        [Display(Name = "Charge Date Time")]
        public DateTime DelayedChargeDate { get; set; }
        public string Message { get; set; }
    }
}
