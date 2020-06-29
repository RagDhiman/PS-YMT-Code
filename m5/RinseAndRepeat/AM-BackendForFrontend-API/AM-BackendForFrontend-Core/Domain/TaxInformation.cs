using System;
using System.Collections.Generic;
using System.Text;
using AM_BackendForFrontend_Data.Generic;

namespace AM_BackendForFrontend_Core
{
    public class TaxInformation : IEntity
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public String TaxCode { get; set; }
        public bool VAT { get; set; }
        public String VATRef { get; set; }
    }
}
