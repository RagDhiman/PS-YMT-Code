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
    public class AttachmentRepository: IAttachmentRepository
    {
        private readonly AccountManagerContext _context;

        public AttachmentRepository(AccountManagerContext context)
        {
            _context = context;
        }
        public async Task<Attachment[]> GetAllAttachmentsAsync(int SupplierId)
        {
            IQueryable<Attachment> query = _context.Attachments;

            query = query
                .Where(a => a.SupplierId == SupplierId);

            return await query.ToArrayAsync();
        }

        public async Task<Attachment> GetAttachmentAsync(int SupplierId, int id)
        {
            IQueryable<Attachment> query = _context.Attachments;

            return await query.Where(a => a.SupplierId == SupplierId && a.Id == id).FirstOrDefaultAsync();
        }

        public async Task<bool> StoreNewAttachmentAsync(int SupplierId, Attachment Attachment)
        {
            Attachment.SupplierId = SupplierId;

            _context.Attachments.Add(Attachment);
            return (await _context.SaveChangesAsync()) > 0;
        }

        public async Task<bool> UpdateAttachmentAsync(int SupplierId, Attachment Attachment)
        {
            Attachment.SupplierId = SupplierId;

            return (await _context.SaveChangesAsync()) > 0;
        }

        public async Task<bool> DeleteAttachment(Attachment Attachment)
        {
            _context.Remove(Attachment);
            return (await _context.SaveChangesAsync()) > 0;
        }

    }
}
