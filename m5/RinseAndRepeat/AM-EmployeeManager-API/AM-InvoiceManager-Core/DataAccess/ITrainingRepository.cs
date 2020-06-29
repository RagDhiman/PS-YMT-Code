using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AM_EmployeeManager_Core.DataAccess
{
    public interface ITrainingRepository
    {
        Task<Training[]> GetAllTrainingesAsync(int EmployeeId);
        Task<Training> GetTrainingAsync(int? EmployeeId, int? id);
        Task<bool> StoreNewTrainingAsync(int EmployeeId, Training Training);
        Task<bool> UpdateTrainingAsync(int EmployeeId, Training Training);
        Task<bool> DeleteTraining(Training Training);
    }
}
