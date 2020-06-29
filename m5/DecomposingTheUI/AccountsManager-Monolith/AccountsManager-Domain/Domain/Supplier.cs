﻿using System;
using System.Collections.Generic;
using System.Text;

namespace AccountsManager_Domain
{
    public class Supplier
    {
        public int Id { get; set; }
        public int AccountId { get; set; }
        public Account Account { get; set; }
        public string ContactName { get; set; }
        public string Company { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Mobile { get; set; }
        public string Fax { get; set; }
        public string Website { get; set; }
        public List<Attachment> Attachments { get; set; }
        public List<Expense> Expenses { get; set; }
        public List<SupplierNote> SupplierNotes { get; set; }

    }
}
