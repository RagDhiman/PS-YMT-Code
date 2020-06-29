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
    public class DelayedChargeLineRepository : IDelayedChargeLineRepository
    {
        private readonly AccountManagerContext _context;

        public DelayedChargeLineRepository(AccountManagerContext context)
        {
            _context = context;
        }
        public async Task<DelayedChargeLine[]> GetAllDelayedChargeLinesAsync(int DelayedChargeId)
        {
            IQueryable<DelayedChargeLine> query = _context.DelayedChargeLines;

            query = query
                .Where(a => a.DelayedChargeId == DelayedChargeId);

            return await query.ToArrayAsync();
        }

        public async Task<DelayedChargeLine> GetDelayedChargeLineAsync(int DelayedChargeId, int id)
        {
            IQueryable<DelayedChargeLine> query = _context.DelayedChargeLines;

            return await query.Where(a => a.DelayedChargeId == DelayedChargeId && a.Id == id).FirstOrDefaultAsync();
        }

        public async Task<bool> StoreNewDelayedChargeLineAsync(int DelayedChargeId, DelayedChargeLine DelayedChargeLine)
        {
            DelayedChargeLine.DelayedChargeId = DelayedChargeId;

            _context.DelayedChargeLines.Add(DelayedChargeLine);
            return (await _context.SaveChangesAsync()) > 0;
        }

        public async Task<bool> UpdateDelayedChargeLineAsync(int DelayedChargeId, DelayedChargeLine DelayedChargeLine)
        {
            DelayedChargeLine.DelayedChargeId = DelayedChargeId;

            return (await _context.SaveChangesAsync()) > 0;
        }

        public async Task<bool> DeleteDelayedChargeLine(DelayedChargeLine DelayedChargeLine)
        {
            _context.Remove(DelayedChargeLine);
            return (await _context.SaveChangesAsync()) > 0;
        }

    }
}
