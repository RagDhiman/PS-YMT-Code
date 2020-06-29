using AM_BackendForFrontend_Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace AM_BackendForFrontend_API.Models
{
    public class ExpenseModel
    {
        public int Id { get; set; }
        public string PayeeName { get; set; }
        public int CustomerId { get; set; }
        public int SupplierId { get; set; }
        public int EmployeeId { get; set; }
        public DateTime PaymentDate { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        public int BankAccountId { get; set; }
        public String Reference { get; set; }
        public String Notes { get; set; }
    }
}
