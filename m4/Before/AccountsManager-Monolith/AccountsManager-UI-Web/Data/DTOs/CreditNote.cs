﻿using System;
using System.Collections.Generic;

namespace AccountsManager_UI_Web.Data.DTOs
{
    public class CreditNote : IEntity
    {
        public int Id { get; set; }
        public int InvoiceId { get; set; }
        public int CustomerId { get; set; }
        public DateTime CreditNoteDate { get; set; }
        public string Message { get; set; }
    }
}
