using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AM_InvoiceManager_Core.DataAccess
{
    public interface IDelayedChargeRepository
    {
        Task<DelayedCharge[]> GetAllDelayedChargeesAsync(int CustomerId);
        Task<DelayedCharge> GetDelayedChargeAsync(int? CustomerId, int? id);
        Task<bool> StoreNewDelayedChargeAsync(int CustomerId, DelayedCharge DelayedCharge);
        Task<bool> UpdateDelayedChargeAsync(int CustomerId, DelayedCharge DelayedCharge);
        Task<bool> DeleteDelayedCharge(DelayedCharge DelayedCharge);
    }
}
