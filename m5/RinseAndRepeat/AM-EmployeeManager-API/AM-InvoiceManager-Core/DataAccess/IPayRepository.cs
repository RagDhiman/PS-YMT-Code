using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AM_EmployeeManager_Core.DataAccess
{
    public interface IPayRepository
    {
        Task<Pay[]> GetAllPaysAsync(int CustomerId);
        Task<Pay> GetPayAsync(int? CustomerId, int? id);
        Task<bool> StoreNewPayAsync(int CustomerId, Pay Pay);
        Task<bool> UpdatePayAsync(int CustomerId, Pay Pay);
        Task<bool> DeletePay(Pay Pay);
    }
}
