using AM_CustomerManager_Core;

namespace AM_CustomerManager_API.Models
{
    public class PaymentBillingModel
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public PaymentMethod PrefferedMethod { get; set; }
        public string Terms { set; get; }
        public double OpeningBalance { set; get; }
    }
}
