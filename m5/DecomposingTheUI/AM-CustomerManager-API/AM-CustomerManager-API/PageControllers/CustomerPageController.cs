using AM_CustomerManager_API.PageModels;
using AM_CustomerManager_Core;
using AM_CustomerManager_Core.DataAccess;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AM_CustomerManager_API.Controllers
{
    [ApiExplorerSettings(IgnoreApi = true)]
    [Route("/customerpage")]
    public class CustomerPageController : Controller
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IMapper _mapper;

        public CustomerPageController(ICustomerRepository customerRepository, IMapper mapper)
        {
            _customerRepository = customerRepository;
            _mapper = mapper;
        }

        [Route("/customerpage/index")]
        [HttpGet]
        public async Task<ViewResult> Index()
        {
            var customers = await _customerRepository.GetAllCustomersAsync();

            var customerModels = _mapper.Map<IEnumerable<CustomerIndexModel>>(customers);

            return View(customerModels);
        }

        [HttpGet("[controller]/[action]/{Id}")]
        public async Task<ViewResult> Edit(int Id)
        {
            var customer = await _customerRepository.GetCustomerAsync(Id);

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
            var customer = await _customerRepository.GetCustomerAsync(Id);

            if (customer == null)
            {
                Response.StatusCode = NotFound().StatusCode;
                return View("Customer not found!");
            }

            _mapper.Map(model, customer);

            await _customerRepository.UpdateCustomerAsync(customer);

            return RedirectToAction("Index");
        }

        [HttpGet("[controller]/[action]")]
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
            await _customerRepository.StoreNewCustomerAsync(customer);

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(int Id)
        {
            var customer = await _customerRepository.GetCustomerAsync(Id);

            if (customer == null)
            {
                Response.StatusCode = NotFound().StatusCode;
                return View("Customer not found!");
            }

            await _customerRepository.DeleteCustomer(customer);

            return RedirectToAction("Index");
        }

        private int GerUserAccountId()
        {
            return 1987; //User auth and accounts not implemented in demo!
        }
    }
}
