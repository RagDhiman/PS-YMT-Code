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
    public class PaymentRepository : IPaymentRepository
    {
        private readonly AccountManagerContext _context;

        public PaymentRepository(AccountManagerContext context)
        {
            _context = context;
        }
        public async Task<Payment[]> GetAllPaymentsAsync(int InvoiceId)
        {
            IQueryable<Payment> query = _context.Payments;

            query = query
                .Where(a => a.InvoiceId == InvoiceId);

            return await query.ToArrayAsync();
        }

        public async Task<Payment> GetPaymentAsync(int InvoiceId, int id)
        {
            IQueryable<Payment> query = _context.Payments;

            return await query.Where(a => a.InvoiceId == InvoiceId && a.Id == id).FirstOrDefaultAsync();
        }

        public async Task<bool> StoreNewPaymentAsync(int InvoiceId, Payment Payment)
        {
            Payment.InvoiceId = InvoiceId;

            _context.Payments.Add(Payment);
            return (await _context.SaveChangesAsync()) > 0;
        }

        public async Task<bool> UpdatePaymentAsync(int InvoiceId, Payment Payment)
        {
            Payment.InvoiceId = InvoiceId;

            return (await _context.SaveChangesAsync()) > 0;
        }

        public async Task<bool> DeletePayment(Payment Payment)
        {
            _context.Remove(Payment);
            return (await _context.SaveChangesAsync()) > 0;
        }

    }
}
