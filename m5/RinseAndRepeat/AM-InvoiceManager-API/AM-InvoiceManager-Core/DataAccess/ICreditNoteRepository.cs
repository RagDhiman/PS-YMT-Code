using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AM_InvoiceManager_Core.DataAccess
{
    public interface ICreditNoteRepository
    {
        Task<CreditNote[]> GetAllCreditNoteesAsync(int CustomerId);
        Task<CreditNote> GetCreditNoteAsync(int? CustomerId, int? id);
        Task<bool> StoreNewCreditNoteAsync(int CustomerId, CreditNote CreditNote);
        Task<bool> UpdateCreditNoteAsync(int CustomerId, CreditNote CreditNote);
        Task<bool> DeleteCreditNote(CreditNote CreditNote);
    }
}
