using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AccountsManager_UI_Web.Data;
using AccountsManager_UI_Web.Data.DTOs;
using AccountsManager_UI_Web.Models;
using AccountsManager_UI_Web.ViewModels;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace AccountsManager_UI_Web.Controllers
{
    public class CustomerDashController : Controller
    {
        private readonly IGenericRepository<Customer> _customerRepository;
        private readonly IGenericRepository<Address> _addressRepository;
        private readonly IGenericRepository<Note> _noteRepository;
        private readonly IGenericRepository<TaxInfo> _taxInfoRepository;
        private readonly IGenericRepository<PaymentBilling> _paymentBillingRepository;
        private readonly IGenericRepository<BankAccount> _bankAccountRepository;
        private readonly IMapper _mapper;

        public CustomerDashController(IGenericRepository<Customer> customerRepository,
            IGenericRepository<Address> addressRepository,
            IGenericRepository<Note> noteRepository,
            IGenericRepository<TaxInfo> taxInfoRepository,
            IGenericRepository<PaymentBilling> paymentBillingRepository,
            IGenericRepository<BankAccount> bankAccountRepository,
            IMapper mapper)
        {
            _customerRepository = customerRepository;
            _customerRepository.ResourcePath = "api/customer";
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
            var customer = await _customerRepository.GetByIdAsync(Id);

            SetupChildRepositories(Id);
            var customerAddresses = await _addressRepository.GetAllAsync();
            var paymentBillings = await _paymentBillingRepository.GetAllAsync();
            var notes = await _noteRepository.GetAllAsync();
            var taxInfos = await _taxInfoRepository.GetAllAsync();
            var bankAccounts = await _bankAccountRepository.GetAllAsync();

            var dashViewModel = new CustomerDashViewModel();

            dashViewModel.CustomerDetails = _mapper.Map<CustomerDetailsModel>(customer);
            dashViewModel.AddressesDetails = _mapper.Map<IEnumerable<AddressIndexModel>>(customerAddresses);
            dashViewModel.NoteDetails = _mapper.Map<IEnumerable<NoteIndexModel>>(notes);
            dashViewModel.PaymentBillingDetails = _mapper.Map<IEnumerable<PaymentBillingIndexModel>>(paymentBillings);
            dashViewModel.TaxInfoDetails = _mapper.Map<IEnumerable<TaxInfoIndexModel>>(taxInfos);
            dashViewModel.BankAccounts = _mapper.Map<IEnumerable<BankAccountIndexModel>>(bankAccounts);

            return View(dashViewModel);
        }

        private void SetupChildRepositories(int forCustomerId)
        {
            _addressRepository.ResourcePath = $"api/customer/{forCustomerId}/address";
            _bankAccountRepository.ResourcePath = $"api/customer/{forCustomerId}/bankaccount";
            _paymentBillingRepository.ResourcePath = $"api/customer/{forCustomerId}/note";
            _noteRepository.ResourcePath = $"api/customer/{forCustomerId}/taxinfo";
            _taxInfoRepository.ResourcePath = $"api/customer/{forCustomerId}/paymentbilling";
        }
    }
}