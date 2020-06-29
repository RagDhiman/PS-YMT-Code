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
    public class SalesReceiptLineRepository : ISalesReceiptLineRepository
    {
        private readonly AccountManagerContext _context;

        public SalesReceiptLineRepository(AccountManagerContext context)
        {
            _context = context;
        }
        public async Task<SalesReceiptLine[]> GetAllSalesReceiptLinesAsync(int SalesReceiptId)
        {
            IQueryable<SalesReceiptLine> query = _context.SalesReceiptLines;

            query = query
                .Where(a => a.SalesReceiptId == SalesReceiptId);

            return await query.ToArrayAsync();
        }

        public async Task<SalesReceiptLine> GetSalesReceiptLineAsync(int SalesReceiptId, int id)
        {
            IQueryable<SalesReceiptLine> query = _context.SalesReceiptLines;

            return await query.Where(a => a.SalesReceiptId == SalesReceiptId && a.Id == id).FirstOrDefaultAsync();
        }

        public async Task<bool> StoreNewSalesReceiptLineAsync(int SalesReceiptId, SalesReceiptLine SalesReceiptLine)
        {
            SalesReceiptLine.SalesReceiptId = SalesReceiptId;

            _context.SalesReceiptLines.Add(SalesReceiptLine);
            return (await _context.SaveChangesAsync()) > 0;
        }

        public async Task<bool> UpdateSalesReceiptLineAsync(int SalesReceiptId, SalesReceiptLine SalesReceiptLine)
        {
            SalesReceiptLine.SalesReceiptId = SalesReceiptId;

            return (await _context.SaveChangesAsync()) > 0;
        }

        public async Task<bool> DeleteSalesReceiptLine(SalesReceiptLine SalesReceiptLine)
        {
            _context.Remove(SalesReceiptLine);
            return (await _context.SaveChangesAsync()) > 0;
        }

    }
}
