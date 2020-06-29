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
    public class CreditNoteLineRepository : ICreditNoteLineRepository
    {
        private readonly AccountManagerContext _context;

        public CreditNoteLineRepository(AccountManagerContext context)
        {
            _context = context;
        }
        public async Task<CreditNoteLine[]> GetAllCreditNoteLinesAsync(int CreditNoteId)
        {
            IQueryable<CreditNoteLine> query = _context.CreditNoteLines;

            query = query
                .Where(a => a.CreditNoteId == CreditNoteId);

            return await query.ToArrayAsync();
        }

        public async Task<CreditNoteLine> GetCreditNoteLineAsync(int CreditNoteId, int id)
        {
            IQueryable<CreditNoteLine> query = _context.CreditNoteLines;

            return await query.Where(a => a.CreditNoteId == CreditNoteId && a.Id == id).FirstOrDefaultAsync();
        }

        public async Task<bool> StoreNewCreditNoteLineAsync(int CreditNoteId, CreditNoteLine CreditNoteLine)
        {
            CreditNoteLine.CreditNoteId = CreditNoteId;

            _context.CreditNoteLines.Add(CreditNoteLine);
            return (await _context.SaveChangesAsync()) > 0;
        }

        public async Task<bool> UpdateCreditNoteLineAsync(int CreditNoteId, CreditNoteLine CreditNoteLine)
        {
            CreditNoteLine.CreditNoteId = CreditNoteId;

            return (await _context.SaveChangesAsync()) > 0;
        }

        public async Task<bool> DeleteCreditNoteLine(CreditNoteLine CreditNoteLine)
        {
            _context.Remove(CreditNoteLine);
            return (await _context.SaveChangesAsync()) > 0;
        }

    }
}
