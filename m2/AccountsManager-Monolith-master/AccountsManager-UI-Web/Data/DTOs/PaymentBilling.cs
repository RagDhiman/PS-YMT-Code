using System;
using System.Collections.Generic;
using System.Text;

namespace AccountsManager_UI_Web.Data.DTOs
{
    public class PaymentBilling : IEntity
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
        public PaymentMethod PrefferedMethod { get; set; }
        public string Terms { set; get; }
        public double OpeningBalance { set; get; }
    }
}
