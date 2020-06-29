using System;
using System.Collections.Generic;

namespace AM_InvoiceManager_Core
{
    public class CreditNote
    {
        public int Id { get; set; }
        public int? InvoiceId { get; set; }
        public Invoice Invoice { get; set; }
        public int? CustomerId { get; set; }
        public DateTime CreditNoteDate { get; set; }
        public string Message { get; set; }
        public List<CreditNoteLine> CreditNoteLines { get; set; }

    }
}
