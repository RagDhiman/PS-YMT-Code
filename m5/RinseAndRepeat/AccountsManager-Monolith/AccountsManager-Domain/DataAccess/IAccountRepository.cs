using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountsManager_Domain.DataAccess
{
    public interface IAccountRepository
    {
        Task<Account[]> GetAllAccountsAsync();
        Task<Account> GetAccountAsync(int id);
        Task<bool> StoreNewAccountAsync(Account Account);
        Task<bool> UpdateAccountAsync(Account Account);
        Task<bool> DeleteAccount(Account Account);
    }
}
