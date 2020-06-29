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
    public class WebhookPostRepository : IWebhookPostRepository
    {
        private readonly AccountManagerContext _context;

        public WebhookPostRepository(AccountManagerContext context)
        {
            _context = context;
        }
        public async Task<WebhookPost[]> GetAllWebhookPostsAsync()
        {
            IQueryable<WebhookPost> query = _context.WebhookPosts;

            query = query.OrderByDescending(c => c.PostDateTime).ThenByDescending(c => c.Sender);
            
            return await query.ToArrayAsync();
        }

        public async Task<WebhookPost> GetWebhookPostAsync(int id)
        {
            IQueryable<WebhookPost> query = _context.WebhookPosts;

            return await query.Where(c => c.Id == id).FirstOrDefaultAsync();
        }

        public async Task<bool> StoreNewWebhookPostAsync(WebhookPost WebhookPost) {
            _context.WebhookPosts.Add(WebhookPost);
            return (await _context.SaveChangesAsync())>0;
        }

        public async Task<bool> UpdateWebhookPostAsync(WebhookPost WebhookPost)
        {
            return (await _context.SaveChangesAsync()) > 0;
        }

        public async Task<bool> DeleteWebhookPost(WebhookPost WebhookPost) {
            _context.Remove(WebhookPost);
            return (await _context.SaveChangesAsync())>0;
        }

    }
}
