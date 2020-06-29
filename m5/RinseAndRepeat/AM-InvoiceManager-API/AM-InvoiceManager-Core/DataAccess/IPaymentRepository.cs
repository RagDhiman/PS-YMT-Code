using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AM_InvoiceManager_Core.DataAccess
{
    public interface IPaymentRepository
    {
        Task<Payment[]> GetAllPaymentesAsync(int CustomerId);
        Task<Payment> GetPaymentAsync(int? CustomerId, int? id);
        Task<bool> StoreNewPaymentAsync(int CustomerId, Payment Payment);
        Task<bool> UpdatePaymentAsync(int CustomerId, Payment Payment);
        Task<bool> DeletePayment(Payment Payment);
    }
}
