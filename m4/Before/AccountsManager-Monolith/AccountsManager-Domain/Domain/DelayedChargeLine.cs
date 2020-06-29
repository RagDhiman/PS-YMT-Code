namespace AccountsManager_Domain
{
    public class DelayedChargeLine
    {
        public int Id { get; set; }
        public int DelayedChargeId { get; set; }
        public ServiceType Service { get; set; }
        public double Quantity { get; set; }
        public double Rate { get; set; }
        public double VAT { get; set; }
    }
}
