using AccountsManager_Domain.DataAccess;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AccountsManager_Domain.Services
{
    public class CustomerCreditService: ICustomerCreditService
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IInvoiceRepository _invoiceRepository;
        private readonly IBankAccountRepository _bankAccountRepository;
        private readonly ILogger<CustomerCreditService> _logger;

        public CustomerCreditService(ICustomerRepository customerRepository,
            IInvoiceRepository invoiceRepository,
            IBankAccountRepository bankAccountRepository,
            ILogger<CustomerCreditService> logger)
        {
            _customerRepository = customerRepository;
            _invoiceRepository = invoiceRepository;
            _bankAccountRepository = bankAccountRepository;
            _logger = logger;
        }

        public async Task<bool> SaveAndProcessCredit(ICreditRepository creditRepositery, int invoiceId, Credit credit)
        {
            try {
                await creditRepositery.StoreNewCreditAsync(invoiceId, credit);
                credit = await ProcessCreditWithBankAsync(credit);
                await creditRepositery.UpdateCreditAsync(invoiceId, credit);
            }
            catch (Exception e) {
                _logger.LogError(e,e.Message);
                return false;
            }

            return true;
        }

        public async Task<Credit> ProcessCreditWithBankAsync(Credit credit) {

            var invoice = await _invoiceRepository.GetInvoiceAsync(credit.InvoiceId);
            var customer = await _customerRepository.GetCustomerAsync(invoice.CustomerId);
            var bankAccounts = new List<BankAccount>(await _bankAccountRepository.GetAllBankAccountsAsync(customer.Id));
            var bankAccount = bankAccounts.FindLast(b => b.CustomerId == invoice.CustomerId);

            credit.CustomerName = $"{customer.Title} {customer.FirstName} {customer.MiddleName} {customer.LastName}";
            credit.AccountNo = bankAccount.AccountNo;
            credit.SortCode = bankAccount.SortCode;
            credit.HasCreditAgreement = customer.CreditAgreement;

            SendCreditToBack(credit);

            return credit;
        }

        private void SendCreditToBack(Credit credit)
        {
            //this is where we would transact with a bank!
        }
    }
}
