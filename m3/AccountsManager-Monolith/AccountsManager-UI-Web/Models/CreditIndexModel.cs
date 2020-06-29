using AccountsManager_UI_Web.Data.DTOs;
using System;
using System.ComponentModel.DataAnnotations;

namespace AccountsManager_UI_Web.Models
{
    public class CreditIndexModel
    {
        public int Id { get; set; }
        [Display(Name = "Invoice Ref.")]
        public int InvoiceId { get; set; }
        [Display(Name = "Credit Date")]
        public DateTime CreditDate { get; set; }
        [Display(Name = "Credit Amount")]
        public double CreditAmount { get; set; }
        [Display(Name = "Product Credit")]
        public Product ProductCredit { get; set; }
        [Display(Name = "Customer Name")]
        public string CustomerName { get; set; }
        [Display(Name = "Account No")]
        public string AccountNo { get; set; }
        [Display(Name = "Sort Code")]
        public string SortCode { get; set; }
        [Display(Name = "CA In Place")]
        public bool HasCreditAgreement { get; set; }
    }
}
