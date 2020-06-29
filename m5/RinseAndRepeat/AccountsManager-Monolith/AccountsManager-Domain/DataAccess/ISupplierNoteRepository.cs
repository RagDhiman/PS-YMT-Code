using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountsManager_Domain.DataAccess
{
    public interface ISupplierNoteRepository
    {
        Task<SupplierNote[]> GetAllSupplierNotesAsync(int SupplierId);
        Task<SupplierNote> GetSupplierNoteAsync(int SupplierId, int id);
        Task<bool> StoreNewSupplierNoteAsync(int SupplierId, SupplierNote SupplierNote);
        Task<bool> UpdateSupplierNoteAsync(int SupplierId, SupplierNote SupplierNote);
        Task<bool> DeleteSupplierNote(SupplierNote SupplierNote);
    }
}
