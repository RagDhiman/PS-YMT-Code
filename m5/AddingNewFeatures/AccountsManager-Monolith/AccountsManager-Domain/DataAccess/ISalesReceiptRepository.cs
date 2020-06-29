using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountsManager_Domain.DataAccess
{
    public interface ISalesReceiptRepository
    {
        Task<SalesReceipt[]> GetAllSalesReceiptsAsync(int InvoiceId);
        Task<SalesReceipt> GetSalesReceiptAsync(int InvoiceId, int id);
        Task<bool> StoreNewSalesReceiptAsync(int InvoiceId, SalesReceipt SalesReceipt);
        Task<bool> UpdateSalesReceiptAsync(int InvoiceId, SalesReceipt SalesReceipt);
        Task<bool> DeleteSalesReceipt(SalesReceipt SalesReceipt);
    }
}
