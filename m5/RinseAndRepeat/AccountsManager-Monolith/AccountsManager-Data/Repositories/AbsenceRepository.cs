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
    public class AbsenceRepository: IAbsenceRepository
    {
        private readonly AccountManagerContext _context;

        public AbsenceRepository(AccountManagerContext context)
        {
            _context = context;
        }
        public async Task<Absence[]> GetAllAbsencesAsync(int EmployeeId)
        {
            IQueryable<Absence> query = _context.Absences;

            query = query
                .Where(a => a.EmployeeId == EmployeeId);

            return await query.ToArrayAsync();
        }

        public async Task<Absence> GetAbsenceAsync(int EmployeeId, int id)
        {
            IQueryable<Absence> query = _context.Absences;

            return await query.Where(a => a.EmployeeId == EmployeeId && a.Id == id).FirstOrDefaultAsync();
        }

        public async Task<bool> StoreNewAbsenceAsync(int EmployeeId, Absence Absence)
        {
            Absence.EmployeeId = EmployeeId;

            _context.Absences.Add(Absence);
            return (await _context.SaveChangesAsync()) > 0;
        }

        public async Task<bool> UpdateAbsenceAsync(int EmployeeId, Absence Absence)
        {
            Absence.EmployeeId = EmployeeId;

            return (await _context.SaveChangesAsync()) > 0;
        }

        public async Task<bool> DeleteAbsence(Absence Absence)
        {
            _context.Remove(Absence);
            return (await _context.SaveChangesAsync()) > 0;
        }

    }
}
