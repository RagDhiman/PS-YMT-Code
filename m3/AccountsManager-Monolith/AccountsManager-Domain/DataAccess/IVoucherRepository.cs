using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountsManager_Domain.DataAccess
{
    public interface IVoucherRepository
    {
        Task<Voucher[]> GetAllVouchersAsync(int AccountId);
        Task<Voucher> GetVoucherAsync(int AccountId, int id);
        Task<bool> StoreNewVoucherAsync(int AccountId, Voucher Voucher);
        Task<bool> UpdateVoucherAsync(int AccountId, Voucher Voucher);
        Task<bool> DeleteVoucher(Voucher Voucher);
    }
}
