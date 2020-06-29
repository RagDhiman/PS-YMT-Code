using System;
using System.Collections.Generic;
using System.Text;

namespace AccountsManager_Domain
{
    public class Attachment
    {
        public int Id { get; set; }
        public string FilePath { get; set; }
        public int SupplierId { get; set; }
        public Supplier Supplier { get; set; }
    }
}
