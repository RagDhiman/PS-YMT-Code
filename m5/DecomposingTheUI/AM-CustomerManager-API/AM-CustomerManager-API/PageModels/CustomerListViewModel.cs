using System.Collections.Generic;

namespace AM_CustomerManager_API.PageModels
{
    public class CustomerListViewModel
    {
        public IEnumerable<CustomerIndexModel> Customers { get; set; }
        public string Company { get; set; }
    }
}
