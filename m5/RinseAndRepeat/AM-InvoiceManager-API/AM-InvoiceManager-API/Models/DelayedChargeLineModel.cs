using AM_InvoiceManager_Core;
using AM_InvoiceManager_Core.Common;

namespace AM_InvoiceManager_API.Models
{
    public class DelayedChargeLineModel
    {
        public int Id { get; set; }
        public int DelayedChargeId { get; set; }
        public ServiceType Service { get; set; }
        public double Quantity { get; set; }
        public double Rate { get; set; }
        public double VAT { get; set; }
    }
}
