using AM_CustomerManager_Core;

namespace AM_CustomerManager_Core
{
    public class TaxInfo
    {
        public int Id { get; set; }
        public string TaxRegNo { get; set; }
        public string UTRNo { get; set; }
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
    }
}
