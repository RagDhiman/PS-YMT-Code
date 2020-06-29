using AccountsManager_WebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AccountsManager_WebUI.ViewModels
{
    public class CustomerListViewModel
    {
        public IEnumerable<CustomerModel> Customers { get; set; }
        public string Company { get; set; }
    }
}
