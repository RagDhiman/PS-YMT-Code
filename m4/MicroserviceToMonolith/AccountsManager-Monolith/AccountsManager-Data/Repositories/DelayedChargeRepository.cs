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
    public class DelayedChargeRepository : IDelayedChargeRepository
    {
        private readonly AccountManagerContext _context;

        public DelayedChargeRepository(AccountManagerContext context)
        {
            _context = context;
        }
        public async Task<DelayedCharge[]> GetAllDelayedChargesAsync(int InvoiceId)
        {
            IQueryable<DelayedCharge> query = _context.DelayedCharges;

            query = query
                .Where(a => a.InvoiceId == InvoiceId);

            return await query.ToArrayAsync();
        }

        public async Task<DelayedCharge> GetDelayedChargeAsync(int InvoiceId, int id)
        {
            IQueryable<DelayedCharge> query = _context.DelayedCharges;

            return await query.Where(a => a.InvoiceId == InvoiceId && a.Id == id).FirstOrDefaultAsync();
        }

        public async Task<bool> StoreNewDelayedChargeAsync(int InvoiceId, DelayedCharge DelayedCharge)
        {
            DelayedCharge.InvoiceId = InvoiceId;

            _context.DelayedCharges.Add(DelayedCharge);
            return (await _context.SaveChangesAsync()) > 0;
        }

        public async Task<bool> UpdateDelayedChargeAsync(int InvoiceId, DelayedCharge DelayedCharge)
        {
            DelayedCharge.InvoiceId = InvoiceId;

            return (await _context.SaveChangesAsync()) > 0;
        }

        public async Task<bool> DeleteDelayedCharge(DelayedCharge DelayedCharge)
        {
            _context.Remove(DelayedCharge);
            return (await _context.SaveChangesAsync()) > 0;
        }

    }
}
