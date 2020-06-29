using AccountsManager_Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountsManager_API.Models
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
