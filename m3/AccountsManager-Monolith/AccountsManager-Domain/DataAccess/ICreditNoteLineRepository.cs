using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountsManager_Domain.DataAccess
{
    public interface ICreditNoteLineRepository
    {
        Task<CreditNoteLine[]> GetAllCreditNoteLinesAsync(int AccountId);
        Task<CreditNoteLine> GetCreditNoteLineAsync(int AccountId, int id);
        Task<bool> StoreNewCreditNoteLineAsync(int AccountId, CreditNoteLine CreditNoteLine);
        Task<bool> UpdateCreditNoteLineAsync(int AccountId, CreditNoteLine CreditNoteLine);
        Task<bool> DeleteCreditNoteLine(CreditNoteLine CreditNoteLine);
    }
}
