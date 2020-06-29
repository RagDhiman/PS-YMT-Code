using AccountsManager_Domain;
using AccountsManager_Domain.DataAccess;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountsManager_Data.Repositories
{
    public class CreditNoteRepository : ICreditNoteRepository
    {
        private readonly AccountManagerContext _context;

        public CreditNoteRepository(AccountManagerContext context)
        {
            _context = context;
        }
        public async Task<CreditNote[]> GetAllCreditNotesAsync(int InvoiceId)
        {
            IQueryable<CreditNote> query = _context.CreditNotes;

            query = query
                .Where(a => a.InvoiceId == InvoiceId);

            return await query.ToArrayAsync();
        }

        public async Task<CreditNote> GetCreditNoteAsync(int InvoiceId, int id)
        {
            IQueryable<CreditNote> query = _context.CreditNotes;

            return await query.Where(a => a.InvoiceId == InvoiceId && a.Id == id).FirstOrDefaultAsync();
        }

        public async Task<bool> StoreNewCreditNoteAsync(int InvoiceId, CreditNote CreditNote)
        {
            CreditNote.InvoiceId = InvoiceId;

            _context.CreditNotes.Add(CreditNote);
            return (await _context.SaveChangesAsync()) > 0;
        }

        public async Task<bool> UpdateCreditNoteAsync(int InvoiceId, CreditNote CreditNote)
        {
            CreditNote.InvoiceId = InvoiceId;

            return (await _context.SaveChangesAsync()) > 0;
        }

        public async Task<bool> DeleteCreditNote(CreditNote CreditNote)
        {
            _context.Remove(CreditNote);
            return (await _context.SaveChangesAsync()) > 0;
        }

    }
}
