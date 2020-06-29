using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AM_SupplierManager_Core.DataAccess
{
    public interface IAttachmentRepository
    {
        Task<Attachment[]> GetAllAttachmentsAsync(int CustomerId);
        Task<Attachment> GetAttachmentAsync(int? CustomerId, int? id);
        Task<bool> StoreNewAttachmentAsync(int CustomerId, Attachment Attachment);
        Task<bool> UpdateAttachmentAsync(int CustomerId, Attachment Attachment);
        Task<bool> DeleteAttachment(Attachment Attachment);
    }
}
