using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AM_BackendForFrontend_API.Models
{
    public class AccountModel
    {
        public int Id { get; set; }
        public string CompanyName { get; set; }
        public string CompanyEmail { get; set; }
        public string CompanySMSSender { get; set; }
        public string NewCustomerCRMWebhook { get; set; }
        public int NoOfUserLicences { get; set; }
        public DateTime RenewalDate { get; set; }
    }
}
