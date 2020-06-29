using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AM_EmployeeManager_Core.DataAccess
{
    public interface IHolidayRepository
    {
        Task<Holiday[]> GetAllHolidayesAsync(int CustomerId);
        Task<Holiday> GetHolidayAsync(int? CustomerId, int? id);
        Task<bool> StoreNewHolidayAsync(int CustomerId, Holiday Holiday);
        Task<bool> UpdateHolidayAsync(int CustomerId, Holiday Holiday);
        Task<bool> DeleteHoliday(Holiday Holiday);
    }
}
