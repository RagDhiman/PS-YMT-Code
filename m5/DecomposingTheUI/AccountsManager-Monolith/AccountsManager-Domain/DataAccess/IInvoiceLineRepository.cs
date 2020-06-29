using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountsManager_Domain.DataAccess
{
    public interface IInvoiceLineRepository
    {
        Task<InvoiceLine[]> GetAllInvoiceLinesAsync(int InvoiceId);
        Task<InvoiceLine> GetInvoiceLineAsync(int InvoiceId, int id);
        Task<bool> StoreNewInvoiceLineAsync(int InvoiceId, InvoiceLine InvoiceLine);
        Task<bool> UpdateInvoiceLineAsync(int InvoiceId, InvoiceLine InvoiceLine);
        Task<bool> DeleteInvoiceLine(InvoiceLine InvoiceLine);
    }
}
