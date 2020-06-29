using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountsManager_Domain.DataAccess
{
    public interface ICreditNoteRepository
    {
        Task<CreditNote[]> GetAllCreditNotesAsync(int AccountId);
        Task<CreditNote> GetCreditNoteAsync(int AccountId, int id);
        Task<bool> StoreNewCreditNoteAsync(int AccountId, CreditNote CreditNote);
        Task<bool> UpdateCreditNoteAsync(int AccountId, CreditNote CreditNote);
        Task<bool> DeleteCreditNote(CreditNote CreditNote);
    }
}
