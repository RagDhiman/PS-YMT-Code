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
    public class SupplierRepository : ISupplierRepository
    {
        private readonly AccountManagerContext _context;

        public SupplierRepository(AccountManagerContext context)
        {
            _context = context;
        }
        public async Task<Supplier[]> GetAllSuppliersAsync()
        {
            IQueryable<Supplier> query = _context.Suppliers;

            query = query.OrderByDescending(c => c.Company);
            
            return await query.ToArrayAsync();
        }

        public async Task<Supplier> GetSupplierAsync(int id)
        {
            IQueryable<Supplier> query = _context.Suppliers;

            return await query.Where(c => c.Id == id).FirstOrDefaultAsync();
        }

        public async Task<bool> StoreNewSupplierAsync(Supplier Supplier) {
            _context.Suppliers.Add(Supplier);
            return (await _context.SaveChangesAsync())>0;
        }

        public async Task<bool> UpdateSupplierAsync(Supplier Supplier)
        {
            return (await _context.SaveChangesAsync()) > 0;
        }

        public async Task<bool> DeleteSupplier(Supplier Supplier) {
            _context.Remove(Supplier);
            return (await _context.SaveChangesAsync())>0;
        }

    }
}
