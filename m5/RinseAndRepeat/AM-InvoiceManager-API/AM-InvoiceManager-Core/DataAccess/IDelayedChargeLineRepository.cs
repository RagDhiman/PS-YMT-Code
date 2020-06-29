using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AM_InvoiceManager_Core.DataAccess
{
    public interface IDelayedChargeLineRepository
    {
        Task<DelayedChargeLine[]> GetAllDelayedChargeLineesAsync(int CustomerId);
        Task<DelayedChargeLine> GetDelayedChargeLineAsync(int? CustomerId, int? id);
        Task<bool> StoreNewDelayedChargeLineAsync(int CustomerId, DelayedChargeLine DelayedChargeLine);
        Task<bool> UpdateDelayedChargeLineAsync(int CustomerId, DelayedChargeLine DelayedChargeLine);
        Task<bool> DeleteDelayedChargeLine(DelayedChargeLine DelayedChargeLine);
    }
}
