using AccountsManager_UI_Web.Data.DTOs;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountsManager_UI_Web.Models
{
    public class SalesReceiptIndexModel
    {
        public int Id { get; set; }
        [Display(Name = "Invoice Ref.")]
        public int InvoiceId { get; set; }
        [Display(Name = "Account Ref.")]
        public int BankAccountId { get; set; }
        [Display(Name = "Customer Ref.")]
        public int CustomerId { get; set; }
        [Display(Name = "Sale Date")]
        public DateTime SalesReceiptDate { get; set; }
        [Display(Name = "Payment Method")]
        public PaymentMethod PaymentMethod { get; set; }
        [Display(Name = "Reference No")]
        public string ReferenceNo { get; set; }
        public string Message { get; set; }
    }
}
