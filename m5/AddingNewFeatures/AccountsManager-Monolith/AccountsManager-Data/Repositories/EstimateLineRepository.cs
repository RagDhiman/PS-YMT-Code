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
    public class EstimateLineRepository : IEstimateLineRepository
    {
        private readonly AccountManagerContext _context;

        public EstimateLineRepository(AccountManagerContext context)
        {
            _context = context;
        }
        public async Task<EstimateLine[]> GetAllEstimateLinesAsync(int EstimateId)
        {
            IQueryable<EstimateLine> query = _context.EstimateLines;

            query = query
                .Where(a => a.EstimateId == EstimateId);

            return await query.ToArrayAsync();
        }

        public async Task<EstimateLine> GetEstimateLineAsync(int EstimateId, int id)
        {
            IQueryable<EstimateLine> query = _context.EstimateLines;

            return await query.Where(a => a.EstimateId == EstimateId && a.Id == id).FirstOrDefaultAsync();
        }

        public async Task<bool> StoreNewEstimateLineAsync(int EstimateId, EstimateLine EstimateLine)
        {
            EstimateLine.EstimateId = EstimateId;

            _context.EstimateLines.Add(EstimateLine);
            return (await _context.SaveChangesAsync()) > 0;
        }

        public async Task<bool> UpdateEstimateLineAsync(int EstimateId, EstimateLine EstimateLine)
        {
            EstimateLine.EstimateId = EstimateId;

            return (await _context.SaveChangesAsync()) > 0;
        }

        public async Task<bool> DeleteEstimateLine(EstimateLine EstimateLine)
        {
            _context.Remove(EstimateLine);
            return (await _context.SaveChangesAsync()) > 0;
        }

    }
}
