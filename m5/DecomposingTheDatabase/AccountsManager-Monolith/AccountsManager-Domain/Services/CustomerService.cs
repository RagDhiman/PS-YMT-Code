using AccountsManager_Domain.DataAccess;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace AccountsManager_Domain.Services
{
    public class CustomerService: ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly INotificationService _notificationService;
        private readonly IAccountRepository _accountRepository;

        public CustomerService(ICustomerRepository customerRepository, 
            IAccountRepository accountRepository,
            INotificationService notificationService)
        {
            _customerRepository = customerRepository;
            _notificationService = notificationService;
            _accountRepository = accountRepository;
        }

        public async Task<bool> ProcessNewCustomer(Customer newCustomer)
        {
            var emailSentOk = false;
            var smsSentOk = false;
            var webHookPost = false;

            var customerAccount = await _accountRepository.GetAccountAsync(newCustomer.AccountId);

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
                    Body = JsonConvert.SerializeObject(newCustomer,new JsonSerializerSettings() {ReferenceLoopHandling = ReferenceLoopHandling.Ignore}),
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
