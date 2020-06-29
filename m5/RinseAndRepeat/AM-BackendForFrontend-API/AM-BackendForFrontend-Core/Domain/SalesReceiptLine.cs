using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AM_BackendForFrontend_Data.Generic;


namespace AM_BackendForFrontend_Core
{
    public class SalesReceiptLine : IEntity
    {
        public int Id { get; set; }
        public int SalesReceiptId { get; set; }
        public SalesReceipt SalesReceipt { get; set; }
        public ServiceType Service { get; set; }
        public double Quantity { get; set; }
        public double Rate { get; set; }
        public double VAT { get; set; }
    }
}
