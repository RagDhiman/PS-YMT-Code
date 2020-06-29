using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AM_InvoiceManager_Core.DataAccess
{
    public interface ISalesReceiptLineRepository
    {
        Task<SalesReceiptLine[]> GetAllSalesReceiptLineesAsync(int CustomerId);
        Task<SalesReceiptLine> GetSalesReceiptLineAsync(int? CustomerId, int? id);
        Task<bool> StoreNewSalesReceiptLineAsync(int CustomerId, SalesReceiptLine SalesReceiptLine);
        Task<bool> UpdateSalesReceiptLineAsync(int CustomerId, SalesReceiptLine SalesReceiptLine);
        Task<bool> DeleteSalesReceiptLine(SalesReceiptLine SalesReceiptLine);
    }
}
