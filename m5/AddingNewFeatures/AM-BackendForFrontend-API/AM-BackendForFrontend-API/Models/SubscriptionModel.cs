using AM_BackendForFrontend_Core.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AM_BackendForFrontend_API.Models
{
    public class SubscriptionModel
    {
        public int Id { get; set; }
        public int AccountId { get; set; }
        public DateTime StardDate { get; set; }
        public DateTime EndDate { get; set; }
        public Product ProductSubscription { get; set; }
    }
}
