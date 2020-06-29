using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountsManager_Domain.DataAccess
{
    public interface IDelayedChargeRepository
    {
        Task<DelayedCharge[]> GetAllDelayedChargesAsync(int InvoiceId);
        Task<DelayedCharge> GetDelayedChargeAsync(int InvoiceId, int id);
        Task<bool> StoreNewDelayedChargeAsync(int InvoiceId, DelayedCharge DelayedCharge);
        Task<bool> UpdateDelayedChargeAsync(int InvoiceId, DelayedCharge DelayedCharge);
        Task<bool> DeleteDelayedCharge(DelayedCharge DelayedCharge);
    }
}
