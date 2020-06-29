using System;
using System.Collections.Generic;
using System.Text;
using AM_BackendForFrontend_Data.Generic;

namespace AM_BackendForFrontend_Core
{
    public class SupplierNote : IEntity
    {
        public int Id { get; set; }
        public string NoteText { get; set; }
        public int SupplierId { get; set; }
    }
}
