using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountsManager_Domain.DataAccess
{
    public interface ISMSRepository
    {
        Task<SMS[]> GetAllSMSsAsync();
        Task<SMS> GetSMSAsync(int id);
        Task<bool> StoreNewSMSAsync(SMS SMS);
        Task<bool> UpdateSMSAsync(SMS SMS);
        Task<bool> DeleteSMS(SMS SMS);
    }
}
