using AM_CustomerManager_Core.DataAccess;
using System.Threading.Tasks;

namespace AM_CustomerManager_Core.Services
{
    public class CustomerService: ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly INotificationService _notificationService;

        public CustomerService(ICustomerRepository customerRepository, 
            INotificationService notificationService)
        {
            _customerRepository = customerRepository;
            _notificationService = notificationService;
        }

        public async Task<bool> ProcessNewCustomer(Customer newCustomer)
        {
            var emailSentOk = await _notificationService.SendEmailAsync(null);


            var smsSentOk = await _notificationService.SendSMSAsync(null);


            var webHookPost = await _notificationService.PostToWebhookAsync(null);


            newCustomer.CreditAgreement = emailSentOk && smsSentOk && webHookPost;
            await _customerRepository.UpdateCustomerAsync(newCustomer);

            return await Task.FromResult(emailSentOk && smsSentOk && webHookPost);
        }

        private string NewCustomerMessage(string CompanyName, Customer newCustomer) {

            return $"Welcome {newCustomer.FirstName}, please find attached credit agreement link from {CompanyName}.";
        }

        private string NewCustomerMessageSubject(string CompanyName)
        {
            return $"Credit agreement link from {CompanyName}.";
        }
    }
}
