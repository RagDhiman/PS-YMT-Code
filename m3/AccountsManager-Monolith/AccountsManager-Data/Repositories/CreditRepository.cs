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
    public class CreditRepository: ICreditRepository
    {
        private readonly AccountManagerContext _context;

        public CreditRepository(AccountManagerContext context)
        {
            _context = context;
        }
        public async Task<Credit[]> GetAllCreditsAsync(int InvoiceId)
        {
            IQueryable<Credit> query = _context.Credits;

            query = query
                .Where(a => a.InvoiceId == InvoiceId)    
                .OrderByDescending(a => a.CreditAmount)
                    .ThenByDescending(a => a.CreditDate);

            return await query.ToArrayAsync();
        }

        public async Task<Credit> GetCreditAsync(int InvoiceId, int id)
        {
            IQueryable<Credit> query = _context.Credits;

            return await query.Where(a => a.InvoiceId == InvoiceId && a.Id == id).FirstOrDefaultAsync();
        }

        public async Task<bool> StoreNewCreditAsync(int InvoiceId, Credit Credit)
        {
            Credit.InvoiceId = InvoiceId;

            _context.Credits.Add(Credit);
            return (await _context.SaveChangesAsync()) > 0;
        }

        public async Task<bool> UpdateCreditAsync(int InvoiceId, Credit Credit)
        {
            Credit.InvoiceId = InvoiceId;

            return (await _context.SaveChangesAsync()) > 0;
        }

        public async Task<bool> DeleteCredit(Credit Credit)
        {
            _context.Remove(Credit);
            return (await _context.SaveChangesAsync()) > 0;
        }

    }
}
