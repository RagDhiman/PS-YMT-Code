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
    public class PayRepository: IPayRepository
    {
        private readonly AccountManagerContext _context;

        public PayRepository(AccountManagerContext context)
        {
            _context = context;
        }
        public async Task<Pay[]> GetAllPaysAsync(int EmployeeId)
        {
            IQueryable<Pay> query = _context.Pays;

            query = query
                .Where(a => a.EmployeeId == EmployeeId);

            return await query.ToArrayAsync();
        }

        public async Task<Pay> GetPayAsync(int EmployeeId, int id)
        {
            IQueryable<Pay> query = _context.Pays;

            return await query.Where(a => a.EmployeeId == EmployeeId && a.Id == id).FirstOrDefaultAsync();
        }

        public async Task<bool> StoreNewPayAsync(int EmployeeId, Pay Pay)
        {
            Pay.EmployeeId = EmployeeId;

            _context.Pays.Add(Pay);
            return (await _context.SaveChangesAsync()) > 0;
        }

        public async Task<bool> UpdatePayAsync(int EmployeeId, Pay Pay)
        {
            Pay.EmployeeId = EmployeeId;

            return (await _context.SaveChangesAsync()) > 0;
        }

        public async Task<bool> DeletePay(Pay Pay)
        {
            _context.Remove(Pay);
            return (await _context.SaveChangesAsync()) > 0;
        }

    }
}
