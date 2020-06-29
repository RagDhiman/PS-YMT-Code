using AccountsManager_Domain;
using AccountsManager_Domain.DataAccess;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountsManager_Data.Repositories
{
    public class PaymentDetailsRepository: IPaymentDetailsRepository
    {
        private readonly AccountManagerContext _context;

        public PaymentDetailsRepository(AccountManagerContext context)
        {
            _context = context;
        }
        public async Task<PaymentDetails[]> GetAllPaymentDetailsAsync(int AccountId)
        {
            IQueryable<PaymentDetails> query = _context.PaymentDetails;

            query = query
                .Where(a => a.AccountId == AccountId);

            return await query.ToArrayAsync();
        }

        public async Task<PaymentDetails> GetPaymentDetailsAsync(int AccountId, int id)
        {
            IQueryable<PaymentDetails> query = _context.PaymentDetails;

            return await query.Where(a => a.AccountId == AccountId && a.Id == id).FirstOrDefaultAsync();
        }

        public async Task<bool> StoreNewPaymentDetailsAsync(int AccountId, PaymentDetails PaymentDetails)
        {
            PaymentDetails.AccountId = AccountId;

            _context.PaymentDetails.Add(PaymentDetails);
            return (await _context.SaveChangesAsync()) > 0;
        }

        public async Task<bool> UpdatePaymentDetailsAsync(int AccountId, PaymentDetails PaymentDetails)
        {
            PaymentDetails.AccountId = AccountId;

            return (await _context.SaveChangesAsync()) > 0;
        }

        public async Task<bool> DeletePaymentDetails(PaymentDetails PaymentDetails)
        {
            _context.Remove(PaymentDetails);
            return (await _context.SaveChangesAsync()) > 0;
        }

    }
}
