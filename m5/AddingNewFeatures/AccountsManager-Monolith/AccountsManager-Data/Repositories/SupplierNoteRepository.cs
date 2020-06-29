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
    public class SupplierNoteRepository: ISupplierNoteRepository
    {
        private readonly AccountManagerContext _context;

        public SupplierNoteRepository(AccountManagerContext context)
        {
            _context = context;
        }
        public async Task<SupplierNote[]> GetAllSupplierNotesAsync(int SupplierId)
        {
            IQueryable<SupplierNote> query = _context.SupplierNotes;

            query = query
                .Where(a => a.SupplierId == SupplierId);

            return await query.ToArrayAsync();
        }

        public async Task<SupplierNote> GetSupplierNoteAsync(int SupplierId, int id)
        {
            IQueryable<SupplierNote> query = _context.SupplierNotes;

            return await query.Where(a => a.SupplierId == SupplierId && a.Id == id).FirstOrDefaultAsync();
        }

        public async Task<bool> StoreNewSupplierNoteAsync(int SupplierId, SupplierNote SupplierNote)
        {
            SupplierNote.SupplierId = SupplierId;

            _context.SupplierNotes.Add(SupplierNote);
            return (await _context.SaveChangesAsync()) > 0;
        }

        public async Task<bool> UpdateSupplierNoteAsync(int SupplierId, SupplierNote SupplierNote)
        {
            SupplierNote.SupplierId = SupplierId;

            return (await _context.SaveChangesAsync()) > 0;
        }

        public async Task<bool> DeleteSupplierNote(SupplierNote SupplierNote)
        {
            _context.Remove(SupplierNote);
            return (await _context.SaveChangesAsync()) > 0;
        }

    }
}
