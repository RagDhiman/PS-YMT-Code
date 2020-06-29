using System;

namespace AM_CustomerManager_Core.ContractModels
{
    public class Account
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
