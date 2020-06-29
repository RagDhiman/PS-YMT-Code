using AM_CustomerManager_Core;
using AM_CustomerManager_Core.DataAccess;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace AM_CustomerManager_Data_EFC.Repositories
{
    public class PaymentBillingRepository : IPaymentBillingRepository
    {
        private readonly AccountManagerContext _context;

        public PaymentBillingRepository(AccountManagerContext context)
        {
            _context = context;
        }
        public async Task<PaymentBilling[]> GetAllPaymentBillingesAsync(int CustomerId)
        {
            IQueryable<PaymentBilling> query = _context.PaymentBillings;

            query = query
                .Where(a => a.CustomerId == CustomerId);

            return await query.ToArrayAsync();
        }

        public async Task<PaymentBilling> GetPaymentBillingAsync(int? CustomerId, int? id)
        {
            IQueryable<PaymentBilling> query = _context.PaymentBillings;

            return await query.Where(a => a.CustomerId == CustomerId && a.Id == id).FirstOrDefaultAsync();
        }

        public async Task<bool> StoreNewPaymentBillingAsync(int CustomerId, PaymentBilling PaymentBilling)
        {
            PaymentBilling.CustomerId = CustomerId;

            _context.PaymentBillings.Add(PaymentBilling);
            return (await _context.SaveChangesAsync()) > 0;
        }

        public async Task<bool> UpdatePaymentBillingAsync(int CustomerId, PaymentBilling PaymentBilling)
        {
            PaymentBilling.CustomerId = CustomerId;

            return (await _context.SaveChangesAsync()) > 0;
        }

        public async Task<bool> DeletePaymentBilling(PaymentBilling PaymentBilling)
        {
            _context.Remove(PaymentBilling);
            return (await _context.SaveChangesAsync()) > 0;
        }

    }
}
