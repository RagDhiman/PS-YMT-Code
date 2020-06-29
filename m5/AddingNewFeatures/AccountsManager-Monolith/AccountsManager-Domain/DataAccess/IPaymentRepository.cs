using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountsManager_Domain.DataAccess
{
    public interface IPaymentRepository
    {
        Task<Payment[]> GetAllPaymentsAsync(int InvoiceId);
        Task<Payment> GetPaymentAsync(int InvoiceId, int id);
        Task<bool> StoreNewPaymentAsync(int InvoiceId, Payment Payment);
        Task<bool> UpdatePaymentAsync(int InvoiceId, Payment Payment);
        Task<bool> DeletePayment(Payment Payment);
    }
}
