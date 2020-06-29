using AccountsManager_Domain;
using AccountsManager_Domain.DataAccess;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountsManager_Data.Repositories
{
    public class BankAccountRepository : IBankAccountRepository
    {
        private readonly AccountManagerContext _context;

        public BankAccountRepository(AccountManagerContext context)
        {
            _context = context;
        }
        public async Task<BankAccount[]> GetAllBankAccountsAsync(int CustomerId)
        {
            IQueryable<BankAccount> query = _context.BankAccounts;

            query = query
                .Where(a => a.CustomerId == CustomerId);

            return await query.ToArrayAsync();
        }

        public async Task<BankAccount> GetBankAccountAsync(int? CustomerId, int? id)
        {
            IQueryable<BankAccount> query = _context.BankAccounts;

            if (CustomerId != null) {
                query = query
                    .Where(a => a.CustomerId == CustomerId);
            }

            if (id != null)
            {
                query = query
                    .Where(a => a.Id == id);
            }

            return await query.FirstOrDefaultAsync();
        }

        public async Task<bool> StoreNewBankAccountAsync(int CustomerId, BankAccount BankAccount)
        {
            BankAccount.CustomerId = CustomerId;

            _context.BankAccounts.Add(BankAccount);
            return (await _context.SaveChangesAsync()) > 0;
        }

        public async Task<bool> UpdateBankAccountAsync(int CustomerId, BankAccount BankAccount)
        {
            BankAccount.CustomerId = CustomerId;

            return (await _context.SaveChangesAsync()) > 0;
        }

        public async Task<bool> DeleteBankAccount(BankAccount BankAccount)
        {
            _context.Remove(BankAccount);
            return (await _context.SaveChangesAsync()) > 0;
        }
    }
}
