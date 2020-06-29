using AM_CustomerManager_Core.ContractModels;
using AM_CustomerManager_Core.DataAccess;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace AM_CustomerManager_Core.Services
{
    public class CustomerService: ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly INotificationService _notificationService;
        private readonly IAccountService _accountService;

        public CustomerService(ICustomerRepository customerRepository, 
            IAccountService accountService,
            INotificationService notificationService)
        {
            _customerRepository = customerRepository;
            _notificationService = notificationService;
            _accountService = accountService;
;
        }

        public async Task<bool> ProcessNewCustomer(Customer newCustomer)
        {
            var emailSentOk = false;
            var smsSentOk = false;
            var webHookPost = false;

            var customerAccount = await _accountService.GetAccountAsync(newCustomer.AccountId);

            if (newCustomer.Email != null && customerAccount.CompanyEmail != null) {
                var customerEmail = new Email() { SendTo = newCustomer.Email,
                    Message = NewCustomerMessage(customerAccount, newCustomer),
                    Subject = NewCustomerMessageSubject(customerAccount), 
                    Sender = customerAccount.CompanyEmail, 
                    SentDateTime = System.DateTime.Now
                };

                emailSentOk = await _notificationService.SendEmailAsync(customerEmail);
            }

            if (newCustomer.Mobile != null && customerAccount.CompanySMSSender != null) {
                var customerSMS = new SMS() {  SendTo = newCustomer.Mobile, 
                    Sender = customerAccount.CompanySMSSender,
                    Message = NewCustomerMessage(customerAccount, newCustomer),
                    SentDateTime = System.DateTime.Now};

                smsSentOk = await _notificationService.SendSMSAsync(customerSMS);
            }

            if (customerAccount.NewCustomerCRMWebhook != null)
            {
                var newCustomerWebhookPost = new WebhookPost()
                {
                    URL = customerAccount.NewCustomerCRMWebhook,
                    Sender = customerAccount.CompanyName,
                    Body = JsonConvert.SerializeObject(newCustomer),
                    PostDateTime = System.DateTime.Now
                };

                webHookPost = await _notificationService.PostToWebhookAsync(newCustomerWebhookPost);
            }

            newCustomer.CreditAgreement = emailSentOk && smsSentOk && webHookPost;
            await _customerRepository.UpdateCustomerAsync(newCustomer);

            return await Task.FromResult(emailSentOk && smsSentOk && webHookPost);
        }

        private string NewCustomerMessage(Account customerAccount, Customer newCustomer) {

            return $"Welcome {newCustomer.FirstName}, please find attached credit agreement link from {customerAccount.CompanyName}.";
        }

        private string NewCustomerMessageSubject(Account customerAccount)
        {
            return $"Credit agreement link from {customerAccount.CompanyName}.";
        }
    }
}
