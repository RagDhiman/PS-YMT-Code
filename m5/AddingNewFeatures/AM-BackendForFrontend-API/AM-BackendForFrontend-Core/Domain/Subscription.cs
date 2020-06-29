using AM_BackendForFrontend_Core.Common;
using System;
using AM_BackendForFrontend_Data.Generic;

namespace AM_BackendForFrontend_Core
{
    public class Subscription : IEntity
    {
        public int Id { get; set; }
        public int AccountId { get; set; }
        public DateTime StardDate { get; set; }
        public DateTime EndDate { get; set; }
        public Product ProductSubscription { get; set; }

    }
}
