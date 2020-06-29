using System;
using System.Collections.Generic;
using System.Text;
using AM_BackendForFrontend_Data.Generic;

namespace AM_BackendForFrontend_Core
{
    public class TaxInfo : IEntity
    {
        public int Id { get; set; }
        public string TaxRegNo { get; set; }
        public string UTRNo { get; set; }
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
    }
}
