using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AM_BackendForFrontend_Data.Generic;

namespace AM_BackendForFrontend_Core
{
    public class BankAccount : IEntity
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public string AccountNo { get; set; }
        public string SortCode { get; set; }
    }
}
