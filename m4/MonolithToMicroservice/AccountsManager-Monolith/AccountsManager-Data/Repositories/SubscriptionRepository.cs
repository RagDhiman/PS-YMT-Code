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
    public class SubscriptionRepository: ISubscriptionRepository
    {
        private readonly AccountManagerContext _context;

        public SubscriptionRepository(AccountManagerContext context)
        {
            _context = context;
        }
        public async Task<Subscription[]> GetAllSubscriptionsAsync(int AccountId)
        {
            IQueryable<Subscription> query = _context.Subscriptions;

            query = query
                .Where(a => a.AccountId == AccountId);

            return await query.ToArrayAsync();
        }

        public async Task<Subscription> GetSubscriptionAsync(int AccountId, int id)
        {
            IQueryable<Subscription> query = _context.Subscriptions;

            return await query.Where(a => a.AccountId == AccountId && a.Id == id).FirstOrDefaultAsync();
        }

        public async Task<bool> StoreNewSubscriptionAsync(int AccountId, Subscription Subscription)
        {
            Subscription.AccountId = AccountId;

            _context.Subscriptions.Add(Subscription);
            return (await _context.SaveChangesAsync()) > 0;
        }

        public async Task<bool> UpdateSubscriptionAsync(int AccountId, Subscription Subscription)
        {
            Subscription.AccountId = AccountId;

            return (await _context.SaveChangesAsync()) > 0;
        }

        public async Task<bool> DeleteSubscription(Subscription Subscription)
        {
            _context.Remove(Subscription);
            return (await _context.SaveChangesAsync()) > 0;
        }

    }
}
