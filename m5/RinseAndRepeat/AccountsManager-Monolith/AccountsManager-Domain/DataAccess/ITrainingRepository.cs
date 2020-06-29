using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountsManager_Domain.DataAccess
{
    public interface ITrainingRepository
    {
        Task<Training[]> GetAllTrainingsAsync(int AccountId);
        Task<Training> GetTrainingAsync(int AccountId, int id);
        Task<bool> StoreNewTrainingAsync(int AccountId, Training Training);
        Task<bool> UpdateTrainingAsync(int AccountId, Training Training);
        Task<bool> DeleteTraining(Training Training);
    }
}
