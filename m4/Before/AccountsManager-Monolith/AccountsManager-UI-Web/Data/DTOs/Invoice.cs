using System;
using System.Collections.Generic;
using System.Text;

namespace AccountsManager_UI_Web.Data.DTOs
{
    public class Invoice : IEntity
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
        public DateTime InvoiceDate { get; set; }
        public DateTime DueDate { get; set; }
        public string Message { get; set; }
        public List<InvoiceLine> InvoiceLines { get; set; }
    }
}
