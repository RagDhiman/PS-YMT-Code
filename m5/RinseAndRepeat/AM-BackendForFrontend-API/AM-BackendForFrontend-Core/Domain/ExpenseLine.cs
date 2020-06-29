using System;
using System.Collections.Generic;
using System.Text;
using AM_BackendForFrontend_Data.Generic;

namespace AM_BackendForFrontend_Core
{
    public class ExpenseLine : IEntity
    {
        public int Id { get; set; }
        public int ExpenseId { get; set; }
        public ServiceType ServiceType { get; set; }
        public String Description { get; set; }
        public double Amount { get; set; }
        public double VAT { get; set; }
    }
}
