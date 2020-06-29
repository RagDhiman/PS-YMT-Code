using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountsManager_Domain.DataAccess
{
    public interface IBankAccountRepository
    {
        Task<BankAccount[]> GetAllBankAccountsAsync(int CustomerId);
        Task<BankAccount> GetBankAccountAsync(int? CustomerId, int? id);
        Task<bool> StoreNewBankAccountAsync(int CustomerId, BankAccount BankAccount);
        Task<bool> UpdateBankAccountAsync(int CustomerId, BankAccount BankAccount);
        Task<bool> DeleteBankAccount(BankAccount BankAccount);
    }
}
