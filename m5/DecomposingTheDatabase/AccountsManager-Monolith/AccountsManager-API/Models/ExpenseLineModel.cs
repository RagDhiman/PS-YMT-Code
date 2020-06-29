using AccountsManager_Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace AccountsManager_API.Models
{
    public class ExpenseLineModel
    {
        public int Id { get; set; }
        public int ExpenseId { get; set; }
        public ServiceType ServiceType { get; set; }
        public String Description { get; set; }
        public double Amount { get; set; }
        public double VAT { get; set; }
    }
}
