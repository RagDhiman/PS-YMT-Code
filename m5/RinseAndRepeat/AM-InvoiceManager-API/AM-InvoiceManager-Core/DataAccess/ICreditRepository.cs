using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AM_InvoiceManager_Core;

namespace AM_InvoiceManager_Core.DataAccess
{
    public interface ICreditRepository
    {
        Task<Credit[]> GetAllCreditesAsync(int CustomerId);
        Task<Credit> GetCreditAsync(int? CustomerId, int? id);
        Task<bool> StoreNewCreditAsync(int CustomerId, Credit Credit);
        Task<bool> UpdateCreditAsync(int CustomerId, Credit Credit);
        Task<bool> DeleteCredit(Credit Credit);
    }
}
