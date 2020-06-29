using System;
using System.Collections.Generic;
using System.Text;

namespace AM_CustomerManager_API.Models
{
    public class TaxInfoModel
    {
        public int Id { get; set; }
        public string TaxRegNo { get; set; }
        public string UTRNo { get; set; }
        public int CustomerId { get; set; }
    }
}
