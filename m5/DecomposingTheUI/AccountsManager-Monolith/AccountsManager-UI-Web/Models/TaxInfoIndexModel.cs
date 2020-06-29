using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AccountsManager_UI_Web.Models
{
    public class TaxInfoIndexModel
    {
        public int Id { get; set; }
        [Display(Name = "Tax Reg No.")]
        public string TaxRegNo { get; set; }
        [Display(Name = "UTR No.")]
        public string UTRNo { get; set; }
        [Display(Name = "Customer Id")]
        public int CustomerId { get; set; }
    }
}
