using AM_CustomerManager_Core.ContractModels;
using System.Threading.Tasks;

namespace AM_CustomerManager_Core.Services
{
    public interface IAccountService
    {
        Task<Account> GetAccountAsync(int id);
    }
}