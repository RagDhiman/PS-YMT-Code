using System.Threading.Tasks;

namespace AM_BackendForFrontend_Core.Services
{
    public interface INotificationService
    {
        Task<bool> SendEmailAsync(Email email);
        Task<bool> SendSMSAsync(SMS sms);
        Task<bool> PostToWebhookAsync(WebhookPost webpost);
    }
}