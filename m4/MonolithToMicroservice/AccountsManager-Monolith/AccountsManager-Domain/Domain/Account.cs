using EmployeesManager_Domain.DataAccess;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace AccountsManager_Domain
{
    public class Account: IEntity
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
