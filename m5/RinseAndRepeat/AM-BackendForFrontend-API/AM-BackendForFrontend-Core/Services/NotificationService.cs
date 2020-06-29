using AM_BackendForFrontend_Data.Generic;
using System.Threading.Tasks;

namespace AM_BackendForFrontend_Core.Services
{
    public class NotificationService: INotificationService
    {
        private readonly INotificationsServiceRepository<Email> _emailRepository;
        private readonly INotificationsServiceRepository<SMS> _smsRepository;
        private readonly INotificationsServiceRepository<WebhookPost> _webHookPostRepository;

        public NotificationService(INotificationsServiceRepository<Email> emailRepository, INotificationsServiceRepository<SMS> smsRepository, INotificationsServiceRepository<WebhookPost> webHookPostRepository)
        {
            _emailRepository = emailRepository;
            _smsRepository = smsRepository;
            _webHookPostRepository = webHookPostRepository;
        }

        public Task<bool> SendEmailAsync(Email email) {

            _emailRepository.StoreNewAsync(email);

            return Task.FromResult(true);
        }

        public async Task<bool> SendSMSAsync(SMS sms)
        {
            bool result = await _smsRepository.StoreNewAsync(sms);

            return result;
        }

        public async Task<bool> PostToWebhookAsync(WebhookPost webpost)
        {
            bool result = await _webHookPostRepository.StoreNewAsync(webpost);

            return result;
        }
    }
}
