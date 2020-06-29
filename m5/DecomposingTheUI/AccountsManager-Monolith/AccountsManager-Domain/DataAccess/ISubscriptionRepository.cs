using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountsManager_Domain.DataAccess
{
    public interface ISubscriptionRepository
    {
        Task<Subscription[]> GetAllSubscriptionsAsync(int AccountId);
        Task<Subscription> GetSubscriptionAsync(int AccountId, int id);
        Task<bool> StoreNewSubscriptionAsync(int AccountId, Subscription Subscription);
        Task<bool> UpdateSubscriptionAsync(int AccountId, Subscription Subscription);
        Task<bool> DeleteSubscription(Subscription Subscription);
    }
}
