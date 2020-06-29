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

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AccountsManager_UI_Web.Controllers
{
    public class CustomerController : Controller
    {
        private readonly IGenericRepository<Customer> _customerRepository;
        private readonly IMapper _mapper;

        public CustomerController(IGenericRepository<Customer> customerRepository, IMapper mapper)
        {
            customerRepository.ResourcePath = "api/customer";
            _customerRepository = customerRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ViewResult> Index()
        {
            var customers = await _customerRepository.GetAllAsync();

            var customerModels = _mapper.Map<IEnumerable<CustomerIndexModel>>(customers);

            return View(customerModels);
        }

        [HttpGet("[controller]/[action]/{Id}")]
        public async Task<ViewResult> Edit(int Id)
        {
            var customer = await _customerRepository.GetByIdAsync(Id);

            if (customer == null)
            {
                Response.StatusCode = NotFound().StatusCode;
                return View("Customer not found!");
            }

            var customerModel = _mapper.Map<CustomerEditModel>(customer);
            ViewBag.FormAspAction = "Edit";

            return View(customerModel);
        }

        [HttpPost("[controller]/[action]/{Id}")]
        public async Task<IActionResult> Edit(int Id, [FromForm]CustomerEditModel model)
        {
            var customer = await _customerRepository.GetByIdAsync(Id);

            if (customer == null)
            {
                Response.StatusCode = NotFound().StatusCode;
                return View("Customer not found!");
            }

            _mapper.Map(model, customer);

            await _customerRepository.UpdateAsync(customer);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ViewResult Create()
        {
            var customerModel = new CustomerEditModel();
            ViewBag.FormAspAction = "Create";

            customerModel.FirstName = "Dummy";
            customerModel.LastName = "Test";
            customerModel.Title = "Mr";
            customerModel.Email = "rag.dhiman@gmail.com";
           
            return View("Edit", customerModel);
        }

        [HttpPost("[controller]/[action]")]
        public async Task<IActionResult> Create([FromForm]CustomerEditModel model)
        {
            var customer = new Customer();
            _mapper.Map(model, customer);

            customer.AccountId = GerUserAccountId();
            await _customerRepository.StoreNewAsync(customer);

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(int Id)
        {
            var customer = await _customerRepository.GetByIdAsync(Id);

            if (customer == null)
            {
                Response.StatusCode = NotFound().StatusCode;
                return View("Customer not found!");
            }

            await _customerRepository.DeleteAsync(customer);

            return RedirectToAction("Index");
        }

        private int GerUserAccountId()
        {
            return 1987; //User auth and accounts not implemented in demo!
        }
    }
}
