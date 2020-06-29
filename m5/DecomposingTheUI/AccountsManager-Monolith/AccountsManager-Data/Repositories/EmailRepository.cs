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
    public class EmailRepository : IEmailRepository
    {
        private readonly AccountManagerContext _context;

        public EmailRepository(AccountManagerContext context)
        {
            _context = context;
        }
        public async Task<Email[]> GetAllEmailsAsync()
        {
            IQueryable<Email> query = _context.Emails;

            query = query.OrderByDescending(c => c.SentDateTime).ThenByDescending(c => c.Subject);
            
            return await query.ToArrayAsync();
        }

        public async Task<Email> GetEmailAsync(int id)
        {
            IQueryable<Email> query = _context.Emails;

            return await query.Where(c => c.Id == id).FirstOrDefaultAsync();
        }

        public async Task<bool> StoreNewEmailAsync(Email Email) {
            _context.Emails.Add(Email);
            return (await _context.SaveChangesAsync())>0;
        }

        public async Task<bool> UpdateEmailAsync(Email Email)
        {
            return (await _context.SaveChangesAsync()) > 0;
        }

        public async Task<bool> DeleteEmail(Email Email) {
            _context.Remove(Email);
            return (await _context.SaveChangesAsync())>0;
        }

    }
}
