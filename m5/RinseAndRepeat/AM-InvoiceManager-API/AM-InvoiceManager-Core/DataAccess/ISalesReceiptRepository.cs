using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AM_InvoiceManager_Core.DataAccess
{
    public interface ISalesReceiptRepository
    {
        Task<SalesReceipt[]> GetAllSalesReceiptesAsync(int CustomerId);
        Task<SalesReceipt> GetSalesReceiptAsync(int? CustomerId, int? id);
        Task<bool> StoreNewSalesReceiptAsync(int CustomerId, SalesReceipt SalesReceipt);
        Task<bool> UpdateSalesReceiptAsync(int CustomerId, SalesReceipt SalesReceipt);
        Task<bool> DeleteSalesReceipt(SalesReceipt SalesReceipt);
    }
}
