using AM_BackendForFrontend_Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AM_BackendForFrontend_API.Models
{
    public class EstimateLineModel
    {
        public int Id { get; set; }
        public int EstimateId { get; set; }
        public ServiceType Service { get; set; }
        public double Quantity { get; set; }
        public double Rate { get; set; }
        public double VAT { get; set; }
    }
}
