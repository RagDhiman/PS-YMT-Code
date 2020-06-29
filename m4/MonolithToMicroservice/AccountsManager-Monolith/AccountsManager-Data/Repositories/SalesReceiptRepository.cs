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
    public class SalesReceiptRepository : ISalesReceiptRepository
    {
        private readonly AccountManagerContext _context;

        public SalesReceiptRepository(AccountManagerContext context)
        {
            _context = context;
        }
        public async Task<SalesReceipt[]> GetAllSalesReceiptsAsync(int InvoiceId)
        {
            IQueryable<SalesReceipt> query = _context.SalesReceipts;

            query = query
                .Where(a => a.InvoiceId == InvoiceId);

            return await query.ToArrayAsync();
        }

        public async Task<SalesReceipt> GetSalesReceiptAsync(int InvoiceId, int id)
        {
            IQueryable<SalesReceipt> query = _context.SalesReceipts;

            return await query.Where(a => a.InvoiceId == InvoiceId && a.Id == id).FirstOrDefaultAsync();
        }

        public async Task<bool> StoreNewSalesReceiptAsync(int InvoiceId, SalesReceipt SalesReceipt)
        {
            SalesReceipt.InvoiceId = InvoiceId;

            _context.SalesReceipts.Add(SalesReceipt);
            return (await _context.SaveChangesAsync()) > 0;
        }

        public async Task<bool> UpdateSalesReceiptAsync(int InvoiceId, SalesReceipt SalesReceipt)
        {
            SalesReceipt.InvoiceId = InvoiceId;

            return (await _context.SaveChangesAsync()) > 0;
        }

        public async Task<bool> DeleteSalesReceipt(SalesReceipt SalesReceipt)
        {
            _context.Remove(SalesReceipt);
            return (await _context.SaveChangesAsync()) > 0;
        }

    }
}
