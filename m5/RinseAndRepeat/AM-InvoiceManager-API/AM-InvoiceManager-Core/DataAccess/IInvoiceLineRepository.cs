using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AM_InvoiceManager_Core.DataAccess
{
    public interface IInvoiceLineRepository
    {
        Task<InvoiceLine[]> GetAllInvoiceLineesAsync(int CustomerId);
        Task<InvoiceLine> GetInvoiceLineAsync(int? CustomerId, int? id);
        Task<bool> StoreNewInvoiceLineAsync(int CustomerId, InvoiceLine InvoiceLine);
        Task<bool> UpdateInvoiceLineAsync(int CustomerId, InvoiceLine InvoiceLine);
        Task<bool> DeleteInvoiceLine(InvoiceLine InvoiceLine);
    }
}
