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
    public class VoucherRepository: IVoucherRepository
    {
        private readonly AccountManagerContext _context;

        public VoucherRepository(AccountManagerContext context)
        {
            _context = context;
        }
        public async Task<Voucher[]> GetAllVouchersAsync(int AccountId)
        {
            IQueryable<Voucher> query = _context.Vouchers;

            query = query
                .Where(a => a.AccountId == AccountId);

            return await query.ToArrayAsync();
        }

        public async Task<Voucher> GetVoucherAsync(int AccountId, int id)
        {
            IQueryable<Voucher> query = _context.Vouchers;

            return await query.Where(a => a.AccountId == AccountId && a.Id == id).FirstOrDefaultAsync();
        }

        public async Task<bool> StoreNewVoucherAsync(int AccountId, Voucher Voucher)
        {
            Voucher.AccountId = AccountId;

            _context.Vouchers.Add(Voucher);
            return (await _context.SaveChangesAsync()) > 0;
        }

        public async Task<bool> UpdateVoucherAsync(int AccountId, Voucher Voucher)
        {
            Voucher.AccountId = AccountId;

            return (await _context.SaveChangesAsync()) > 0;
        }

        public async Task<bool> DeleteVoucher(Voucher Voucher)
        {
            _context.Remove(Voucher);
            return (await _context.SaveChangesAsync()) > 0;
        }

    }
}
