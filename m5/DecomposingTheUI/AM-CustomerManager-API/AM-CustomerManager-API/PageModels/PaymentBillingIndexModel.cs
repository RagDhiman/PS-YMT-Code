using AM_CustomerManager_Core;
using System.ComponentModel.DataAnnotations;

namespace AM_CustomerManager_API.PageModels
{
    public class PaymentBillingIndexModel
    {
        public int Id { get; set; }
        [Display(Name = "Customer Id")]
        public int CustomerId { get; set; }
        [Display(Name = "Preffered Method")]
        public PaymentMethod PrefferedMethod { get; set; }
        public string Terms { set; get; }
        [Display(Name = "Opening Balance")]
        public double OpeningBalance { set; get; }
    }
}
