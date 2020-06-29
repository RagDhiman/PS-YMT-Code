using AM_SupplierManager_Core.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace AM_SupplierManager_Core
{
    public class SupplierNote
    {
        public int Id { get; set; }
        public string NoteText { get; set; }
        public int SupplierId { get; set; }
    }
}
