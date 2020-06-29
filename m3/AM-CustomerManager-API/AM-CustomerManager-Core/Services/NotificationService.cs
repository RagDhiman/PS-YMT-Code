using System;
using System.Threading.Tasks;

namespace AM_CustomerManager_Core.Services
{
    public class NotificationService : INotificationService
    {
        public Task<bool> PostToWebhookAsync(object webpost)
        {
            throw new NotImplementedException();
        }

        public Task<bool> SendEmailAsync(object email)
        {
            throw new NotImplementedException();
        }

        public Task<bool> SendSMSAsync(object sms)
        {
            throw new NotImplementedException();
        }
    }
}
