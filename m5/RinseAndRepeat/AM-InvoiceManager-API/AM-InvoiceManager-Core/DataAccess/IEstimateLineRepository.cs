using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AM_InvoiceManager_Core.DataAccess
{
    public interface IEstimateLineRepository
    {
        Task<EstimateLine[]> GetAllEstimateLineesAsync(int CustomerId);
        Task<EstimateLine> GetEstimateLineAsync(int? CustomerId, int? id);
        Task<bool> StoreNewEstimateLineAsync(int CustomerId, EstimateLine EstimateLine);
        Task<bool> UpdateEstimateLineAsync(int CustomerId, EstimateLine EstimateLine);
        Task<bool> DeleteEstimateLine(EstimateLine EstimateLine);
    }
}
