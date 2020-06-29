using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountsManager_Domain.DataAccess
{
    public interface IEmailRepository
    {
        Task<Email[]> GetAllEmailsAsync();
        Task<Email> GetEmailAsync(int id);
        Task<bool> StoreNewEmailAsync(Email Email);
        Task<bool> UpdateEmailAsync(Email Email);
        Task<bool> DeleteEmail(Email Email);
    }
}
