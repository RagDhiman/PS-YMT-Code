using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AM_EmployeeManager_Core;

namespace AM_EmployeeManager_Core.DataAccess
{
    public interface IAbsenceRepository
    {
        Task<Absence[]> GetAllAbsenceesAsync(int EmployeeId);
        Task<Absence> GetAbsenceAsync(int? EmployeeId, int? id);
        Task<bool> StoreNewAbsenceAsync(int EmployeeId, Absence Absence);
        Task<bool> UpdateAbsenceAsync(int EmployeeId, Absence Absence);
        Task<bool> DeleteAbsence(Absence Absence);
    }
}
