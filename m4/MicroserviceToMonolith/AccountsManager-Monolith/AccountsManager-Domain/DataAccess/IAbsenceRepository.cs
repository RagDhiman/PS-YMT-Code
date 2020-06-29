using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountsManager_Domain.DataAccess
{
    public interface IAbsenceRepository
    {
        Task<Absence[]> GetAllAbsencesAsync(int AccountId);
        Task<Absence> GetAbsenceAsync(int AccountId, int id);
        Task<bool> StoreNewAbsenceAsync(int AccountId, Absence Absence);
        Task<bool> UpdateAbsenceAsync(int AccountId, Absence Absence);
        Task<bool> DeleteAbsence(Absence Absence);
    }
}
