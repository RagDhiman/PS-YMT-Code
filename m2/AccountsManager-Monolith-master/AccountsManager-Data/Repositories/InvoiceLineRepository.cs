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
    public class InvoiceLineRepository : IInvoiceLineRepository
    {
        private readonly AccountManagerContext _context;

        public InvoiceLineRepository(AccountManagerContext context)
        {
            _context = context;
        }
        public async Task<InvoiceLine[]> GetAllInvoiceLinesAsync(int InvoiceId)
        {
            IQueryable<InvoiceLine> query = _context.InvoiceLines;

            query = query
                .Where(a => a.InvoiceId == InvoiceId);

            return await query.ToArrayAsync();
        }

        public async Task<InvoiceLine> GetInvoiceLineAsync(int InvoiceId, int id)
        {
            IQueryable<InvoiceLine> query = _context.InvoiceLines;

            return await query.Where(a => a.InvoiceId == InvoiceId && a.Id == id).FirstOrDefaultAsync();
        }

        public async Task<bool> StoreNewInvoiceLineAsync(int InvoiceId, InvoiceLine InvoiceLine)
        {
            InvoiceLine.InvoiceId = InvoiceId;

            _context.InvoiceLines.Add(InvoiceLine);
            return (await _context.SaveChangesAsync()) > 0;
        }

        public async Task<bool> UpdateInvoiceLineAsync(int InvoiceId, InvoiceLine InvoiceLine)
        {
            InvoiceLine.InvoiceId = InvoiceId;

            return (await _context.SaveChangesAsync()) > 0;
        }

        public async Task<bool> DeleteInvoiceLine(InvoiceLine InvoiceLine)
        {
            _context.Remove(InvoiceLine);
            return (await _context.SaveChangesAsync()) > 0;
        }

    }
}
