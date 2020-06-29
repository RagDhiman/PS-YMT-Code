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
    public class TaxInformationRepository: ITaxInformationRepository
    {
        private readonly AccountManagerContext _context;

        public TaxInformationRepository(AccountManagerContext context)
        {
            _context = context;
        }
        public async Task<TaxInformation[]> GetAllTaxInformationsAsync(int EmployeeId)
        {
            IQueryable<TaxInformation> query = _context.TaxInformations;

            query = query
                .Where(a => a.EmployeeId == EmployeeId);

            return await query.ToArrayAsync();
        }

        public async Task<TaxInformation> GetTaxInformationAsync(int EmployeeId, int id)
        {
            IQueryable<TaxInformation> query = _context.TaxInformations;

            return await query.Where(a => a.EmployeeId == EmployeeId && a.Id == id).FirstOrDefaultAsync();
        }

        public async Task<bool> StoreNewTaxInformationAsync(int EmployeeId, TaxInformation TaxInformation)
        {
            TaxInformation.EmployeeId = EmployeeId;

            _context.TaxInformations.Add(TaxInformation);
            return (await _context.SaveChangesAsync()) > 0;
        }

        public async Task<bool> UpdateTaxInformationAsync(int EmployeeId, TaxInformation TaxInformation)
        {
            TaxInformation.EmployeeId = EmployeeId;

            return (await _context.SaveChangesAsync()) > 0;
        }

        public async Task<bool> DeleteTaxInformation(TaxInformation TaxInformation)
        {
            _context.Remove(TaxInformation);
            return (await _context.SaveChangesAsync()) > 0;
        }

    }
}
