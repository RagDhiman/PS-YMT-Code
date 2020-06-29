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
    public class SMSRepository : ISMSRepository
    {
        private readonly AccountManagerContext _context;

        public SMSRepository(AccountManagerContext context)
        {
            _context = context;
        }
        public async Task<SMS[]> GetAllSMSsAsync()
        {
            IQueryable<SMS> query = _context.SMSs;

            query = query.OrderByDescending(c => c.SentDateTime);
            
            return await query.ToArrayAsync();
        }

        public async Task<SMS> GetSMSAsync(int id)
        {
            IQueryable<SMS> query = _context.SMSs;

            return await query.Where(c => c.Id == id).FirstOrDefaultAsync();
        }

        public async Task<bool> StoreNewSMSAsync(SMS SMS) {
            _context.SMSs.Add(SMS);
            return (await _context.SaveChangesAsync())>0;
        }

        public async Task<bool> UpdateSMSAsync(SMS SMS)
        {
            return (await _context.SaveChangesAsync()) > 0;
        }

        public async Task<bool> DeleteSMS(SMS SMS) {
            _context.Remove(SMS);
            return (await _context.SaveChangesAsync())>0;
        }

    }
}
