using System;
using System.Collections.Generic;
using System.Text;

namespace AM_SupplierManager_API.Models
{
    public class AttachmentModel
    {
        public int Id { get; set; }
        public string FilePath { get; set; }
        public int SupplierId { get; set; }
    }
}
