using System;
using System.Collections.Generic;
using System.Text;

namespace AccountsManager_Domain
{
    public class TaxInfo
    {
        public int Id { get; set; }
        public string TaxRegNo { get; set; }
        public string UTRNo { get; set; }
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
    }
}
