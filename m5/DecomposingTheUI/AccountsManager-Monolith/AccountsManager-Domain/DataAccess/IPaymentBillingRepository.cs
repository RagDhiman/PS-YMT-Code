using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountsManager_Domain.DataAccess
{
    public interface IPaymentBillingRepository
    {
        Task<PaymentBilling[]> GetAllPaymentBillingesAsync(int CustomerId);
        Task<PaymentBilling> GetPaymentBillingAsync(int CustomerId, int id);
        Task<bool> StoreNewPaymentBillingAsync(int CustomerId, PaymentBilling PaymentBilling);
        Task<bool> UpdatePaymentBillingAsync(int CustomerId, PaymentBilling PaymentBilling);
        Task<bool> DeletePaymentBilling(PaymentBilling PaymentBilling);
    }
}
