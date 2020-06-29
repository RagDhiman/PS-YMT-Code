using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountsManager_Domain.DataAccess
{
    public interface IPaymentDetailsRepository
    {
        Task<PaymentDetails[]> GetAllPaymentDetailsAsync(int AccountId);
        Task<PaymentDetails> GetPaymentDetailsAsync(int AccountId, int id);
        Task<bool> StoreNewPaymentDetailsAsync(int AccountId, PaymentDetails PaymentDetails);
        Task<bool> UpdatePaymentDetailsAsync(int AccountId, PaymentDetails PaymentDetails);
        Task<bool> DeletePaymentDetails(PaymentDetails PaymentDetails);
    }
}
