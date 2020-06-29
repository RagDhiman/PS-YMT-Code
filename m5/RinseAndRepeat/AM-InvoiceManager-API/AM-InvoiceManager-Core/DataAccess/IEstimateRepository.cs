using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AM_InvoiceManager_Core.DataAccess
{
    public interface IEstimateRepository
    {
        Task<Estimate[]> GetAllEstimateesAsync(int CustomerId);
        Task<Estimate> GetEstimateAsync(int? CustomerId, int? id);
        Task<bool> StoreNewEstimateAsync(int CustomerId, Estimate Estimate);
        Task<bool> UpdateEstimateAsync(int CustomerId, Estimate Estimate);
        Task<bool> DeleteEstimate(Estimate Estimate);
    }
}
