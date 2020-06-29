using System.Threading.Tasks;

namespace AccountsManager_Domain.Services
{
    public interface INotificationService
    {
        Task<bool> SendEmailAsync(Email email);
        Task<bool> SendSMSAsync(SMS sms);
        Task<bool> PostToWebhookAsync(WebhookPost webpost);
    }
}