using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AM_InvoiceManager_Core
{
    public class Estimate
    {
        public int Id { get; set; }
        public int InvoiceId { get; set; }
        public Invoice Invoice { get; set; }
        public int? CustomerId { get; set; }
        public DateTime EstimateDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        public string Message { get; set; }
        public List<EstimateLine> EstimateLines { get; set; }

    }
}
