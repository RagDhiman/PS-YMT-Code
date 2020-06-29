using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountsManager_Domain.DataAccess
{
    public interface ISalesReceiptLineRepository
    {
        Task<SalesReceiptLine[]> GetAllSalesReceiptLinesAsync(int SalesReceiptId);
        Task<SalesReceiptLine> GetSalesReceiptLineAsync(int SalesReceiptId, int id);
        Task<bool> StoreNewSalesReceiptLineAsync(int SalesReceiptId, SalesReceiptLine SalesReceiptLine);
        Task<bool> UpdateSalesReceiptLineAsync(int SalesReceiptId, SalesReceiptLine SalesReceiptLine);
        Task<bool> DeleteSalesReceiptLine(SalesReceiptLine SalesReceiptLine);
    }
}
