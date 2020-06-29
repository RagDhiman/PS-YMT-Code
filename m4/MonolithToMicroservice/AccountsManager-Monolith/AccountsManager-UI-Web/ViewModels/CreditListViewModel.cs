using AccountsManager_UI_Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AccountsManager_UI_Web.ViewModels
{
    public class CreditListViewModel
    {
        public int InvoiceId { get; set; }

        public CreditIndexModel CreditIndexModel { get;}
        public IEnumerable<CreditIndexModel> Credits { get; set; }
    }
}
