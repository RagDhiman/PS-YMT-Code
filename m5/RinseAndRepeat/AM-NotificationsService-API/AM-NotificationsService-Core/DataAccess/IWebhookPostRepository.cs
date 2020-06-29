using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AM_NotificationsService_Core.DataAccess
{
    public interface IWebhookPostRepository
    {
        Task<WebhookPost[]> GetAllWebhookPostsAsync();
        Task<WebhookPost> GetWebhookPostAsync(int id);
        Task<bool> StoreNewWebhookPostAsync(WebhookPost WebhookPost);
        Task<bool> UpdateWebhookPostAsync(WebhookPost WebhookPost);
        Task<bool> DeleteWebhookPost(WebhookPost WebhookPost);
    }
}
