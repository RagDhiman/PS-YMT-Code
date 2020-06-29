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
    public class EstimateRepository : IEstimateRepository
    {
        private readonly AccountManagerContext _context;

        public EstimateRepository(AccountManagerContext context)
        {
            _context = context;
        }
        public async Task<Estimate[]> GetAllEstimatesAsync(int InvoiceId)
        {
            IQueryable<Estimate> query = _context.Estimates;

            query = query
                .Where(a => a.InvoiceId == InvoiceId);

            return await query.ToArrayAsync();
        }

        public async Task<Estimate> GetEstimateAsync(int InvoiceId, int id)
        {
            IQueryable<Estimate> query = _context.Estimates;

            return await query.Where(a => a.InvoiceId == InvoiceId && a.Id == id).FirstOrDefaultAsync();
        }

        public async Task<bool> StoreNewEstimateAsync(int InvoiceId, Estimate Estimate)
        {
            Estimate.InvoiceId = InvoiceId;

            _context.Estimates.Add(Estimate);
            return (await _context.SaveChangesAsync()) > 0;
        }

        public async Task<bool> UpdateEstimateAsync(int InvoiceId, Estimate Estimate)
        {
            Estimate.InvoiceId = InvoiceId;

            return (await _context.SaveChangesAsync()) > 0;
        }

        public async Task<bool> DeleteEstimate(Estimate Estimate)
        {
            _context.Remove(Estimate);
            return (await _context.SaveChangesAsync()) > 0;
        }

    }
}
