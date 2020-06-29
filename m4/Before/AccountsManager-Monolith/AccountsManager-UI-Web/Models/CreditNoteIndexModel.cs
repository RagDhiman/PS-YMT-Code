using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AccountsManager_UI_Web.Models
{
    public class CreditNoteIndexModel
    {
        public int Id { get; set; }
        [Display(Name = "Invoice Ref.")]
        public int InvoiceId { get; set; }
        [Display(Name = "Customer Ref.")]
        public int CustomerId { get; set; }
        [Display(Name = "Date Time")]
        public DateTime CreditNoteDate { get; set; }
        public string Message { get; set; }
    }
}
