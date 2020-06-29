using AM_BackendForFrontend_Core.Common;
using System;
using AM_BackendForFrontend_Data.Generic;

namespace AM_BackendForFrontend_Core
{

    public class Credit : IEntity
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
