using System;
using System.Collections.Generic;
using System.Text;
using AM_BackendForFrontend_Data.Generic;

namespace AM_BackendForFrontend_Core
{
    public class SMS : IEntity
    {
        public int Id { get; set; }
        public String SendTo { get; set; }
        public String Sender { get; set; }
        public String Message { get; set; }
        public DateTime SentDateTime { get; set; }
        public DateTime DeliveredDateTime { get; set; }
    }
}
