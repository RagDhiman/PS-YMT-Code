using AM_BackendForFrontend_Data.Generic;

namespace AM_BackendForFrontend_Core
{
    public class PaymentDetails : IEntity
    {
        public int Id { get; set; }
        public int AccountId { get; set; }
        public string AccountNo { get; set; }
        public string SortCode { get; set; }
    }
}
