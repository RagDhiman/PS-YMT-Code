using AM_BackendForFrontend_Core.Common;
using System;
using AM_BackendForFrontend_Data.Generic;

namespace AM_BackendForFrontend_Core
{
    public class Voucher : IEntity
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
