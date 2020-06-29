using System;
using System.Collections.Generic;
using System.Text;

namespace AM_EmployeeManager_API.Models
{
    public class TaxInformationModel
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public String TaxCode { get; set; }
        public bool VAT { get; set; }
        public String VATRef { get; set; }
    }
}
