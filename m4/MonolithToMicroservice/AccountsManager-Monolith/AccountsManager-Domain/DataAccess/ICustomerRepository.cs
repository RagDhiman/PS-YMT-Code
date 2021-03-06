﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountsManager_Domain.DataAccess
{
    public interface ICustomerRepository
    {
        Task<Customer[]> GetAllCustomersAsync();
        Task<Customer> GetCustomerAsync(int id);
        Task<bool> StoreNewCustomerAsync(Customer Customer);
        Task<bool> UpdateCustomerAsync(Customer Customer);
        Task<bool> DeleteCustomer(Customer Customer);
    }
}
