using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AM_CustomerManager_Core.DataAccess
{
    public interface INoteRepository
    {
        Task<Note[]> GetAllNotesAsync(int CustomerId);
        Task<Note> GetNoteAsync(int? CustomerId, int? id);
        Task<bool> StoreNewNoteAsync(int CustomerId, Note Note);
        Task<bool> UpdateNoteAsync(int CustomerId, Note Note);
        Task<bool> DeleteNote(Note Note);
    }
}
