using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AccountsManager_UI_Web.Models
{
    public class AccountDetailModel
    {
        [Display(Name = "Account Reference")]
        public int Id { get; set; }
        [Display(Name = "Your Company")]
        public string CompanyName { get; set; }
        [Display(Name = "No. Licences")]
        public int NoOfUserLicences { get; set; }
        [Display(Name = "Renewal Date")]
        public DateTime RenewalDate { get; set; }
        [Display(Name = "Your company email")]
        public string CompanyEmail { get; set; }
        [Display(Name = "SMS Sender No.")]
        public string CompanySMSSender { get; set; }
        [Display(Name = "Customer CRM Webhook")]
        public string NewCustomerCRMWebhook { get; set; }
    }
}
