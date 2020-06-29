using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AM_CustomerManager_Core.Services
{
    public interface ICustomerService
    {
        Task<bool> ProcessNewCustomer(Customer newCustomer);
    }
}
