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
    public class InvoiceRepository : IInvoiceRepository
    {
        private readonly AccountManagerContext _context;

        public InvoiceRepository(AccountManagerContext context)
        {
            _context = context;
        }
        public async Task<Invoice[]> GetAllInvoicesAsync()
        {
            IQueryable<Invoice> query = _context.Invoices;

            query = query.OrderByDescending(c => c.DueDate).ThenByDescending(c => c.InvoiceDate);

            return await query.ToArrayAsync();
        }

        public async Task<Invoice> GetInvoiceAsync(int id)
        {
            IQueryable<Invoice> query = _context.Invoices;

            return await query.Where(c => c.Id == id).FirstOrDefaultAsync();
        }

        public async Task<bool> StoreNewInvoiceAsync(Invoice Invoice)
        {
            _context.Invoices.Add(Invoice);
            return (await _context.SaveChangesAsync()) > 0;
        }

        public async Task<bool> UpdateInvoiceAsync(Invoice Invoice)
        {
            return (await _context.SaveChangesAsync()) > 0;
        }

        public async Task<bool> DeleteInvoice(Invoice Invoice)
        {
            _context.Remove(Invoice);
            return (await _context.SaveChangesAsync()) > 0;
        }

    }
}
