using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountsManager_UI_Web.Data.DTOs
{
    public class BankAccount : IEntity
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public string AccountNo { get; set; }
        public string SortCode { get; set; }
    }
}
