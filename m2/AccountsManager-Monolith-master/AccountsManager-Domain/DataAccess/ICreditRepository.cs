using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountsManager_Domain.DataAccess
{
    public interface ICreditRepository
    {
        Task<Credit[]> GetAllCreditsAsync(int InvoiceId);
        Task<Credit> GetCreditAsync(int InvoiceId, int id);
        Task<bool> StoreNewCreditAsync(int InvoiceId, Credit Credit);
        Task<bool> UpdateCreditAsync(int InvoiceId, Credit Credit);
        Task<bool> DeleteCredit(Credit Credit);
    }
}
