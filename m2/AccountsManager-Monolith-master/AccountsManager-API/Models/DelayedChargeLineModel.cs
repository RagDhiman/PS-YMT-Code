using AccountsManager_Domain;

namespace AccountsManager_API.Models
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
