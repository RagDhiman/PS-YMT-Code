using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountsManager_Domain.DataAccess
{
    public interface IDelayedChargeLineRepository
    {
        Task<DelayedChargeLine[]> GetAllDelayedChargeLinesAsync(int DelayedChargeId);
        Task<DelayedChargeLine> GetDelayedChargeLineAsync(int DelayedChargeId, int id);
        Task<bool> StoreNewDelayedChargeLineAsync(int DelayedChargeId, DelayedChargeLine DelayedChargeLine);
        Task<bool> UpdateDelayedChargeLineAsync(int DelayedChargeId, DelayedChargeLine DelayedChargeLine);
        Task<bool> DeleteDelayedChargeLine(DelayedChargeLine DelayedChargeLine);
    }
}
