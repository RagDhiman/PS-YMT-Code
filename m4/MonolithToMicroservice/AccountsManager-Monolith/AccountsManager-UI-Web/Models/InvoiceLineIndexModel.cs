using AccountsManager_UI_Web.Data.DTOs;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AccountsManager_UI_Web.Models
{
    public class InvoiceLineIndexModel
    {
        public int Id { get; set; }
        [Display(Name = "Invoice Ref.")]
        public int InvoiceId { get; set; }
        [Display(Name = "Service Type")]
        public ServiceType Service { get; set; }
        public double Quantity { get; set; }
        public double Rate { get; set; }
        public double VAT { get; set; }

    }
}
