using AccountsManager_WebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountsManager_WebUI.Data
{
    public interface ICustomerRepository
    {
        Task<IEnumerable<CustomerModel>> GetAllCustomersAsync();
        Task<CustomerModel> GetCustomerAsync(int id);
        //Task<bool> StoreNewCustomerAsync(CustomerModel customer);
        //Task<bool> UpdateCustomerAsync(CustomerModel customer);
        //Task<bool> DeleteCustomer(CustomerModel customer);
    }
}
