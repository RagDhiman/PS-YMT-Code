using System;
using System.Collections.Generic;
using System.Text;

namespace AccountsManager_Domain
{
    public class SupplierNote
    {
        public int Id { get; set; }
        public string NoteText { get; set; }
        public int SupplierId { get; set; }
        public Supplier Supplier { get; set; }
    }
}
