using AM_ExpensesManager_Core.Common;
using System;
using System.Collections.Generic;

namespace AM_ExpensesManager_Core
{
    public class Expense
    {
        public int Id { get; set; }
        public string PayeeName { get; set; }
        public int CustomerId { get; set; }
        public int SupplierId { get; set; }
        public int ExpenseId { get; set; }
        public DateTime PaymentDate { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        public int EmployeeId { get; set; }

        public int BankAccountId { get; set; }
        public String Reference { get; set; }
        public String Notes { get; set; }

    }
}
