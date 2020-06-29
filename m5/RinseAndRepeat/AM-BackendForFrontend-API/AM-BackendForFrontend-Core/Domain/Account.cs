using System;
using AM_BackendForFrontend_Data.Generic;

namespace AM_BackendForFrontend_Core
{
    public class Account : IEntity
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
