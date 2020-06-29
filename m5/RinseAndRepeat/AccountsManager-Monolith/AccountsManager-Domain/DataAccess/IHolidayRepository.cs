using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountsManager_Domain.DataAccess
{
    public interface IHolidayRepository
    {
        Task<Holiday[]> GetAllHolidaysAsync(int AccountId);
        Task<Holiday> GetHolidayAsync(int AccountId, int id);
        Task<bool> StoreNewHolidayAsync(int AccountId, Holiday Holiday);
        Task<bool> UpdateHolidayAsync(int AccountId, Holiday Holiday);
        Task<bool> DeleteHoliday(Holiday Holiday);
    }
}
