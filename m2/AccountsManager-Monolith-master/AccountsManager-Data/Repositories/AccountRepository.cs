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
    public class AccountRepository : IAccountRepository
    {
        private readonly AccountManagerContext _context;

        public AccountRepository(AccountManagerContext context)
        {
            _context = context;
        }
        public async Task<Account[]> GetAllAccountsAsync()
        {
            IQueryable<Account> query = _context.Accounts;

            query = query.OrderByDescending(c => c.Id);

            return await query.ToArrayAsync();
        }

        public async Task<Account> GetAccountAsync(int id)
        {
            IQueryable<Account> query = _context.Accounts;

            return await query.Where(c => c.Id == id).FirstOrDefaultAsync();
        }

        public async Task<bool> StoreNewAccountAsync(Account Account)
        {
            _context.Accounts.Add(Account);
            return (await _context.SaveChangesAsync()) > 0;
        }

        public async Task<bool> UpdateAccountAsync(Account Account)
        {
            return (await _context.SaveChangesAsync()) > 0;
        }

        public async Task<bool> DeleteAccount(Account Account)
        {
            _context.Remove(Account);
            return (await _context.SaveChangesAsync()) > 0;
        }

    }
}
