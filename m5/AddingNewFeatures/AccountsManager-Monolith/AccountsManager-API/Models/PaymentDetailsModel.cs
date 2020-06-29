using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AccountsManager_API.Models
{
    public class PaymentDetailsModel
    {
        public int Id { get; set; }
        public int AccountId { get; set; }
        public string AccountNo { get; set; }
        public string SortCode { get; set; }
    }
}
