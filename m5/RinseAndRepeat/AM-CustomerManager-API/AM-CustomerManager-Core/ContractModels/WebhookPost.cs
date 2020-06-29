using System;
using System.Collections.Generic;
using System.Text;

namespace AM_CustomerManager_Core.ContractModels
{
    public class WebhookPost
    {
        public int Id { get; set; }
        public String URL { get; set; }
        public String Sender { get; set; }
        public String Body { get; set; }
        public DateTime PostDateTime { get; set; }
    }
}
