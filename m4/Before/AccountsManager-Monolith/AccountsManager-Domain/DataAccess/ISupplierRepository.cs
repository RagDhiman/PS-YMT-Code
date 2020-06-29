using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountsManager_Domain.DataAccess
{
    public interface ISupplierRepository
    {
        Task<Supplier[]> GetAllSuppliersAsync();
        Task<Supplier> GetSupplierAsync(int id);
        Task<bool> StoreNewSupplierAsync(Supplier Supplier);
        Task<bool> UpdateSupplierAsync(Supplier Supplier);
        Task<bool> DeleteSupplier(Supplier Supplier);
    }
}
