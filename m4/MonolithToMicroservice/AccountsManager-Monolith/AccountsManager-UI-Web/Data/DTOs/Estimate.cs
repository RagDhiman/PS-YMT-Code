using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountsManager_UI_Web.Data.DTOs
{
    public class Estimate : IEntity
    {
        public int Id { get; set; }
        public int InvoiceId { get; set; }
        public int CustomerId { get; set; }
        public DateTime EstimateDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        public string Message { get; set; }
    }
}
