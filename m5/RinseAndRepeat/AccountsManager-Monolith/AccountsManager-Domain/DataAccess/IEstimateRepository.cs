using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountsManager_Domain.DataAccess
{
    public interface IEstimateRepository
    {
        Task<Estimate[]> GetAllEstimatesAsync(int InvoiceId);
        Task<Estimate> GetEstimateAsync(int InvoiceId, int id);
        Task<bool> StoreNewEstimateAsync(int InvoiceId, Estimate Estimate);
        Task<bool> UpdateEstimateAsync(int InvoiceId, Estimate Estimate);
        Task<bool> DeleteEstimate(Estimate Estimate);
    }
}
