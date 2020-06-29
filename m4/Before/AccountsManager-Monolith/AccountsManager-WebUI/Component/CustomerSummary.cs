using AccountsManager_WebUI.Data;
using AccountsManager_WebUI.Models;
using AccountsManager_WebUI.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AccountsManager_WebUI.Component
{
    public class CustomerSummary: ViewComponent
    {
        private ICustomerRepository _customerRepository;

        public CustomerSummary(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var customers = await _customerRepository.GetAllCustomersAsync();

            var customersList = new List<CustomerModel>();
            customersList.AddRange(customers);

            var customerSummaryViewModel = new CustomerSummaryViewModel
            {
                CustomerCount = customersList.Count()
            };

            return View(customerSummaryViewModel);
        }
    }
}
