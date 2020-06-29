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
    public class TaxInfoRepository : ITaxInfoRepository
    {
        private readonly AccountManagerContext _context;

        public TaxInfoRepository(AccountManagerContext context)
        {
            _context = context;
        }
        public async Task<TaxInfo[]> GetAllTaxInfoesAsync(int CustomerId)
        {
            IQueryable<TaxInfo> query = _context.TaxInfos;

            query = query
                .Where(a => a.CustomerId == CustomerId);

            return await query.ToArrayAsync();
        }

        public async Task<TaxInfo> GetTaxInfoAsync(int CustomerId, int id)
        {
            IQueryable<TaxInfo> query = _context.TaxInfos;

            return await query.Where(a => a.CustomerId == CustomerId && a.Id == id).FirstOrDefaultAsync();
        }

        public async Task<bool> StoreNewTaxInfoAsync(int CustomerId, TaxInfo TaxInfo)
        {
            TaxInfo.CustomerId = CustomerId;

            _context.TaxInfos.Add(TaxInfo);
            return (await _context.SaveChangesAsync()) > 0;
        }

        public async Task<bool> UpdateTaxInfoAsync(int CustomerId, TaxInfo TaxInfo)
        {
            TaxInfo.CustomerId = CustomerId;

            return (await _context.SaveChangesAsync()) > 0;
        }

        public async Task<bool> DeleteTaxInfo(TaxInfo TaxInfo)
        {
            _context.Remove(TaxInfo);
            return (await _context.SaveChangesAsync()) > 0;
        }

    }
}
