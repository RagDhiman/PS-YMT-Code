using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountsManager_Domain.DataAccess
{
    public interface IPayRepository
    {
        Task<Pay[]> GetAllPaysAsync(int AccountId);
        Task<Pay> GetPayAsync(int AccountId, int id);
        Task<bool> StoreNewPayAsync(int AccountId, Pay Pay);
        Task<bool> UpdatePayAsync(int AccountId, Pay Pay);
        Task<bool> DeletePay(Pay Pay);
    }
}
