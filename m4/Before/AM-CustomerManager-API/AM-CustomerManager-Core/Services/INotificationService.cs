using System.Threading.Tasks;

namespace AM_CustomerManager_Core.Services
{
    public interface INotificationService
    {
        Task<bool> SendEmailAsync(object email);
        Task<bool> SendSMSAsync(object sms);
        Task<bool> PostToWebhookAsync(object webpost);
    }
}