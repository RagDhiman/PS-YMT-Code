using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AccountsManager_Domain.Services
{
    public interface ICustomerService
    {
        Task<bool> ProcessNewCustomer(Customer newCustomer);
    }
}
