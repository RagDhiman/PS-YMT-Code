using AM_InvoiceManager_Core.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AM_InvoiceManager_Core
{
    public class EstimateLine
    {
        public int Id { get; set; }
        public int EstimateId { get; set; }
        public Estimate Estimate { get; set; }
        public ServiceType Service { get; set; }
        public double Quantity { get; set; }
        public double Rate { get; set; }
        public double VAT { get; set; }
    }
}
