using System.Threading.Tasks;

namespace AccountsManager_Domain.Services
{
    public interface INotificationService
    {
        Task<bool> PostToWebhookAsync(WebhookPost webpost);
        Task<bool> SendEmailAsync(Email email);
        Task<bool> SendSMSAsync(SMS sms);
    }
}