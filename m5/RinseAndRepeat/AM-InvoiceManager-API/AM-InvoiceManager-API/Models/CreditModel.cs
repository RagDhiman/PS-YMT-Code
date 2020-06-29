using AM_InvoiceManager_Core.Common;
using System;

namespace AM_InvoiceManager_API.Models
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
