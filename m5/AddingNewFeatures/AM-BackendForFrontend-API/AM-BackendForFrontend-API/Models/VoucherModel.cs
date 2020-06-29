using AM_BackendForFrontend_Core.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AM_BackendForFrontend_API.Models
{
    public class VoucherModel
    {
        public int Id { get; set; }
        public int AccountId { get; set; }
        public string VoucherCode { get; set; }
        public DateTime AppliedDate { get; set; }
        public DateTime ExpireDate { get; set; }
        public double CreditAmount { get; set; }
        public Product ProductVoucher { get; set; }
    }
}
