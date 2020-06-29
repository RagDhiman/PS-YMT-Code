using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AM_SupplierManager_Core.DataAccess
{
    public interface ISupplierNoteRepository
    {
        Task<SupplierNote[]> GetAllSupplierNotesAsync(int CustomerId);
        Task<SupplierNote> GetSupplierNoteAsync(int? CustomerId, int? id);
        Task<bool> StoreNewSupplierNoteAsync(int CustomerId, SupplierNote SupplierNote);
        Task<bool> UpdateSupplierNoteAsync(int CustomerId, SupplierNote SupplierNote);
        Task<bool> DeleteSupplierNote(SupplierNote SupplierNote);
    }
}
