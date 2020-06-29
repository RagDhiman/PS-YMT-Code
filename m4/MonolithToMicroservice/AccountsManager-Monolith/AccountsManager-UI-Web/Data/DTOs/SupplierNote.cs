using System;
using System.Collections.Generic;
using System.Text;

namespace AccountsManager_UI_Web.Data.DTOs
{
    public class SupplierNote : IEntity
    {
        public int Id { get; set; }
        public string NoteText { get; set; }
        public int SupplierId { get; set; }
    }
}
