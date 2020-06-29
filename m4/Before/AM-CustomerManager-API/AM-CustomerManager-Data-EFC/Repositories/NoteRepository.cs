using AM_CustomerManager_Core;
using AM_CustomerManager_Core.DataAccess;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace AM_CustomerManager_Data_EFC.Repositories
{
    public class NoteRepository : INoteRepository
    {
        private readonly AccountManagerContext _context;

        public NoteRepository(AccountManagerContext context)
        {
            _context = context;
        }
        public async Task<Note[]> GetAllNotesAsync(int CustomerId)
        {
            IQueryable<Note> query = _context.Notes;

            query = query
                .Where(a => a.CustomerId == CustomerId)
                .OrderByDescending(a => a.Id)
                    .ThenByDescending(a => a.NoteText);

            return await query.ToArrayAsync();
        }

        public async Task<Note> GetNoteAsync(int? CustomerId, int? id)
        {
            IQueryable<Note> query = _context.Notes;

            return await query.Where(a => a.CustomerId == CustomerId && a.Id == id).FirstOrDefaultAsync();
        }

        public async Task<bool> StoreNewNoteAsync(int CustomerId, Note Note)
        {
            Note.CustomerId = CustomerId;

            _context.Notes.Add(Note);
            return (await _context.SaveChangesAsync()) > 0;
        }

        public async Task<bool> UpdateNoteAsync(int CustomerId, Note Note)
        {
            Note.CustomerId = CustomerId;

            return (await _context.SaveChangesAsync()) > 0;
        }

        public async Task<bool> DeleteNote(Note Note)
        {
            _context.Remove(Note);
            return (await _context.SaveChangesAsync()) > 0;
        }
    }
}
