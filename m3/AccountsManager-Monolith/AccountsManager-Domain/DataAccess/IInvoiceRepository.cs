using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountsManager_Domain.DataAccess
{
    public interface IInvoiceRepository
    {
        Task<Invoice[]> GetAllInvoicesAsync();
        Task<Invoice> GetInvoiceAsync(int id);
        Task<bool> StoreNewInvoiceAsync(Invoice Invoice);
        Task<bool> UpdateInvoiceAsync(Invoice Invoice);
        Task<bool> DeleteInvoice(Invoice Invoice);
    }
}
