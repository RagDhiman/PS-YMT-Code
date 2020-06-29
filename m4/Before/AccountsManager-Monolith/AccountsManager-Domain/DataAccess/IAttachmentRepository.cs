using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountsManager_Domain.DataAccess
{
    public interface IAttachmentRepository
    {
        Task<Attachment[]> GetAllAttachmentsAsync(int SupplierId);
        Task<Attachment> GetAttachmentAsync(int SupplierId, int id);
        Task<bool> StoreNewAttachmentAsync(int SupplierId, Attachment Attachment);
        Task<bool> UpdateAttachmentAsync(int SupplierId, Attachment Attachment);
        Task<bool> DeleteAttachment(Attachment Attachment);
    }
}
