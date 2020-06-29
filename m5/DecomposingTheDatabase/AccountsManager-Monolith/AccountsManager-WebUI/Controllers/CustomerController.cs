using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AccountsManager_WebUI.Data;
using AccountsManager_WebUI.ViewModels;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AccountsManager_WebUI.Controllers
{
    public class CustomerController : Controller
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomerController(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public async Task<ViewResult> List() 
        {
            var viewModel = new CustomerListViewModel();
            viewModel.Customers = await _customerRepository.GetAllCustomersAsync();
            viewModel.Company = "Neha KD Ltd";

            return View(viewModel);
        }

        public async Task<IActionResult> Details(int Id)
        {
            var customer = await _customerRepository.GetCustomerAsync(Id);
            if (customer == null)
                return NotFound();
            return View(customer);
        }
    }
}
