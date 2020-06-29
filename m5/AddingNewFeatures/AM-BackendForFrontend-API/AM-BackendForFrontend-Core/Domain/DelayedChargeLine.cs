using AM_BackendForFrontend_Data.Generic;

namespace AM_BackendForFrontend_Core
{
    public class DelayedChargeLine : IEntity
    {
        public int Id { get; set; }
        public int DelayedChargeId { get; set; }
        public ServiceType Service { get; set; }
        public double Quantity { get; set; }
        public double Rate { get; set; }
        public double VAT { get; set; }
    }
}
