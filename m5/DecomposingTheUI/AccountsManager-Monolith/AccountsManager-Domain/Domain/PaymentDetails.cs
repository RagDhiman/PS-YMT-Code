using AccountsManager_Domain.Common;
using EmployeesManager_Domain.DataAccess;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace AccountsManager_Domain
{
    public class PaymentDetails
    {
        public int Id { get; set; }
        public int AccountId { get; set; }
        public Account Account { get; set; }
        public string AccountNo { get; set; }
        public string SortCode { get; set; }
    }
}
