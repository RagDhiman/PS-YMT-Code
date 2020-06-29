using System;

namespace AM_InvoiceManager_API.Models
{
    public class CreditNoteModel
    {
        public int Id { get; set; }
        public int InvoiceId { get; set; }
        public int CustomerId { get; set; }
        public DateTime CreditNoteDate { get; set; }
        public string Message { get; set; }
    }
}
