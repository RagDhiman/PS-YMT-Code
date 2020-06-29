using AccountsManager_Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AccountsManager_API.Models
{
    public class CreditModel
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
