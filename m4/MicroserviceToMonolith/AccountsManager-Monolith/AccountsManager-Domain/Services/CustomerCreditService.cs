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
        private ICustomerRepository _customerRepository;
        private IInvoiceRepository _invoiceRepository;
        private IBankAccountRepository _bankAccountRepository;
        private ILogger<CustomerCreditService> _logger;

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
            var bankAccount = await _bankAccountRepository.GetBankAccountAsync(customer.Id, null);

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
