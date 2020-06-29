using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountsManager_Domain.DataAccess
{
    public interface IEstimateLineRepository
    {
        Task<EstimateLine[]> GetAllEstimateLinesAsync(int EstimateId);
        Task<EstimateLine> GetEstimateLineAsync(int EstimateId, int id);
        Task<bool> StoreNewEstimateLineAsync(int EstimateId, EstimateLine EstimateLine);
        Task<bool> UpdateEstimateLineAsync(int EstimateId, EstimateLine EstimateLine);
        Task<bool> DeleteEstimateLine(EstimateLine EstimateLine);
    }
}
