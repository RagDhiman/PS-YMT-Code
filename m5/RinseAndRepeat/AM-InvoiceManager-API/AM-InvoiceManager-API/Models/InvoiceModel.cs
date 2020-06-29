using System;
using System.Collections.Generic;
using System.Text;

namespace AM_InvoiceManager_API.Models
{
    public class InvoiceModel
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public DateTime InvoiceDate { get; set; }
        public DateTime DueDate { get; set; }
        public string Message { get; set; }
    }
}
