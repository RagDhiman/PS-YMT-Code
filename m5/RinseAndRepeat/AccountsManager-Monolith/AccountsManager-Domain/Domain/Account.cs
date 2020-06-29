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
        public List<Customer> Customers { get; set; }
        public List<PaymentDetails> PaymentDetails { get; set; }
        public List<Employee> Employees { get; set; }
        public List<Subscription> Subscriptions { get; set; }
        public List<Supplier> Suppliers { get; set; }
        public List<Voucher> Vouchers { get; set; }


    }
}
