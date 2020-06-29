using AM_CustomerManager_Core;

namespace AM_CustomerManager_Core
{
    public class PaymentBilling
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
        public PaymentMethod PrefferedMethod { get; set; }
        public string Terms { set; get; }
        public double OpeningBalance { set; get; }
    }
}
