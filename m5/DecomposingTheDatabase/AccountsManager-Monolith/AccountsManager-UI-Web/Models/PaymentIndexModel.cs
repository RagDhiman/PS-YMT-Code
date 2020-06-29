using AccountsManager_UI_Web.Data.DTOs;
using System;
using System.ComponentModel.DataAnnotations;

namespace AccountsManager_UI_Web.Models
{
    public class PaymentIndexModel
    {
        public int Id { get; set; }
        [Display(Name = "Invoice Ref.")]
        public int InvoiceId { get; set; }
        [Display(Name = "Customer Ref.")]
        public int CustomerId { get; set; }
        [Display(Name = "Payment Date")]
        public DateTime PaymentDate { get; set; }
        [Display(Name = "Payment Method")]
        public PaymentMethod PaymentMethod { get; set; }
        public string Memo { get; set; }
        [Display(Name = "Amount Received")]
        public double AmountReceived { get; set; }
    }
}
