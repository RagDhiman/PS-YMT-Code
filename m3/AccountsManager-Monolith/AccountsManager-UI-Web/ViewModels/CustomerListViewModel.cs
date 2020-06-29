using AccountsManager_UI_Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AccountsManager_UI_Web.ViewModels
{
    public class CustomerListViewModel
    {
        public IEnumerable<CustomerIndexModel> Customers { get; set; }
        public string Company { get; set; }
    }
}
