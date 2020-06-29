using AccountsManager_Domain.Common;
using EmployeesManager_Domain.DataAccess;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace AccountsManager_Domain
{
    public class Voucher
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
