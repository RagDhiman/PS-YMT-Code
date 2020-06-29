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
    public class TrainingRepository: ITrainingRepository
    {
        private readonly AccountManagerContext _context;

        public TrainingRepository(AccountManagerContext context)
        {
            _context = context;
        }
        public async Task<Training[]> GetAllTrainingsAsync(int EmployeeId)
        {
            IQueryable<Training> query = _context.Trainings;

            query = query
                .Where(a => a.EmployeeId == EmployeeId);

            return await query.ToArrayAsync();
        }

        public async Task<Training> GetTrainingAsync(int EmployeeId, int id)
        {
            IQueryable<Training> query = _context.Trainings;

            return await query.Where(a => a.EmployeeId == EmployeeId && a.Id == id).FirstOrDefaultAsync();
        }

        public async Task<bool> StoreNewTrainingAsync(int EmployeeId, Training Training)
        {
            Training.EmployeeId = EmployeeId;

            _context.Trainings.Add(Training);
            return (await _context.SaveChangesAsync()) > 0;
        }

        public async Task<bool> UpdateTrainingAsync(int EmployeeId, Training Training)
        {
            Training.EmployeeId = EmployeeId;

            return (await _context.SaveChangesAsync()) > 0;
        }

        public async Task<bool> DeleteTraining(Training Training)
        {
            _context.Remove(Training);
            return (await _context.SaveChangesAsync()) > 0;
        }

    }
}
