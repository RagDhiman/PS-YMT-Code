using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AM_CustomerManager_Core.DataAccess
{
    public interface IAddressRepository
    {
        Task<Address[]> GetAllAddressesAsync(int CustomerId);
        Task<Address> GetAddressAsync(int? CustomerId, int? id);
        Task<bool> StoreNewAddressAsync(int CustomerId, Address Address);
        Task<bool> UpdateAddressAsync(int CustomerId, Address Address);
        Task<bool> DeleteAddress(Address Address);
    }
}
