using AccountsManager_UI_Web.Data.DTOs;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AccountsManager_UI_Web.Models
{
    public class PaymentBillingIndexModel
    {
        public int Id { get; set; }
        [Display(Name = "Customer Id")]
        public int CustomerId { get; set; }
        [Display(Name = "Preffered Method")]
        public PaymentMethod PrefferedMethod { get; set; }
        public string Terms { set; get; }
        [Display(Name = "Opening Balance")]
        public double OpeningBalance { set; get; }
    }
}
