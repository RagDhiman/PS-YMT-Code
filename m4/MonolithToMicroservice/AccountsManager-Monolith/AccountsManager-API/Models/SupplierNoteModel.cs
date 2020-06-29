using System;
using System.Collections.Generic;
using System.Text;

namespace AccountsManager_API.Models
{
    public class SupplierNoteModel
    {
        public int Id { get; set; }
        public string NoteText { get; set; }
        public int SupplierId { get; set; }
    }
}
