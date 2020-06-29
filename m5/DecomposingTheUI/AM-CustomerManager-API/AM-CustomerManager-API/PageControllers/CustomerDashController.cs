using AM_CustomerManager_API.PageModels;
using AM_CustomerManager_Core.DataAccess;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AM_CustomerManager_API.Controllers
{
    [ApiExplorerSettings(IgnoreApi = true)]
    [Route("/customerdash")]
    public class CustomerDashController : Controller
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IAddressRepository _addressRepository;
        private readonly INoteRepository _noteRepository;
        private readonly ITaxInfoRepository _taxInfoRepository;
        private readonly IPaymentBillingRepository _paymentBillingRepository;
        private readonly IBankAccountRepository _bankAccountRepository;
        private readonly IMapper _mapper;

        public CustomerDashController(ICustomerRepository customerRepository,
            IAddressRepository addressRepository,
            INoteRepository noteRepository,
            ITaxInfoRepository taxInfoRepository,
            IPaymentBillingRepository paymentBillingRepository,
            IBankAccountRepository bankAccountRepository,
            IMapper mapper)
        {
            _customerRepository = customerRepository;
            _addressRepository = addressRepository;
            _noteRepository = noteRepository;
            _taxInfoRepository = taxInfoRepository;
            _paymentBillingRepository = paymentBillingRepository;
            _bankAccountRepository = bankAccountRepository;
            _mapper = mapper;
        }

        [HttpGet("Id")]
        public async Task<IActionResult> Main(int Id)
        {
            var customer = await _customerRepository.GetCustomerAsync(Id);

            var customerAddresses = await _addressRepository.GetAllAddressesAsync(Id);
            var paymentBillings = await _paymentBillingRepository.GetAllPaymentBillingesAsync(Id);
            var notes = await _noteRepository.GetAllNotesAsync(Id);
            var taxInfos = await _taxInfoRepository.GetAllTaxInfoesAsync(Id);
            var bankAccounts = await _bankAccountRepository.GetAllBankAccountesAsync(Id);

            var dashViewModel = new CustomerDashViewModel();

            dashViewModel.CustomerDetails = _mapper.Map<CustomerDetailsModel>(customer);
            dashViewModel.AddressesDetails = _mapper.Map<IEnumerable<AddressIndexModel>>(customerAddresses);
            dashViewModel.NoteDetails = _mapper.Map<IEnumerable<NoteIndexModel>>(notes);
            dashViewModel.PaymentBillingDetails = _mapper.Map<IEnumerable<PaymentBillingIndexModel>>(paymentBillings);
            dashViewModel.TaxInfoDetails = _mapper.Map<IEnumerable<TaxInfoIndexModel>>(taxInfos);
            dashViewModel.BankAccounts = _mapper.Map<IEnumerable<BankAccountIndexModel>>(bankAccounts);

            return View(dashViewModel);
        }

    }
}