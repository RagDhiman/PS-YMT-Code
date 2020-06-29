using AccountsManager_Domain.Common;
using EmployeesManager_Domain.DataAccess;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace AccountsManager_Domain
{

    public class Credit
    {
        public int Id { get; set; }
        public int InvoiceId { get; set; }
        public DateTime CreditDate { get; set; }
        public double CreditAmount { get; set; }
        public Product ProductCredit { get; set; }
        public string CustomerName { get; set; }
        public string AccountNo { get; set; }
        public string SortCode { get; set; }
        public bool HasCreditAgreement { get; set; }
    }
}
