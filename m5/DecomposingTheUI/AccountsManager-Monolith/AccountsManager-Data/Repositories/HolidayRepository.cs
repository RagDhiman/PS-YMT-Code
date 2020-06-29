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
    public class HolidayRepository: IHolidayRepository
    {
        private readonly AccountManagerContext _context;

        public HolidayRepository(AccountManagerContext context)
        {
            _context = context;
        }
        public async Task<Holiday[]> GetAllHolidaysAsync(int EmployeeId)
        {
            IQueryable<Holiday> query = _context.Holidays;

            query = query
                .Where(a => a.EmployeeId == EmployeeId);

            return await query.ToArrayAsync();
        }

        public async Task<Holiday> GetHolidayAsync(int EmployeeId, int id)
        {
            IQueryable<Holiday> query = _context.Holidays;

            return await query.Where(a => a.EmployeeId == EmployeeId && a.Id == id).FirstOrDefaultAsync();
        }

        public async Task<bool> StoreNewHolidayAsync(int EmployeeId, Holiday Holiday)
        {
            Holiday.EmployeeId = EmployeeId;

            _context.Holidays.Add(Holiday);
            return (await _context.SaveChangesAsync()) > 0;
        }

        public async Task<bool> UpdateHolidayAsync(int EmployeeId, Holiday Holiday)
        {
            Holiday.EmployeeId = EmployeeId;

            return (await _context.SaveChangesAsync()) > 0;
        }

        public async Task<bool> DeleteHoliday(Holiday Holiday)
        {
            _context.Remove(Holiday);
            return (await _context.SaveChangesAsync()) > 0;
        }

    }
}
