using AM_CustomerManager_API.Models;
using AM_CustomerManager_Core.DataAccess;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using AM_CustomerManager_Core;
using AutoMapper;
using AM_CustomerManager_Core.Services;

namespace AM_CustomerManager_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerRepository _repository;
        private readonly IMapper _mapper;
        private readonly LinkGenerator _linkGenerator;
        private readonly ILogger<CustomerController> _logger;
        private readonly ICustomerService _customerService;

        public CustomerController(ICustomerRepository repository, IMapper mapper, LinkGenerator linkGenerator, 
            ILogger<CustomerController> logger,
            ICustomerService customerService)
        {
            _repository = repository;
            _mapper = mapper;
            _linkGenerator = linkGenerator;
            _logger = logger;
            _customerService = customerService;
        }

        [HttpGet]
        public async Task<ActionResult<CustomerModel[]>> Get()
        {
            try
            {
                var results = await _repository.GetAllCustomersAsync();

                return _mapper.Map<CustomerModel[]>(results);
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Request Failure");
            }
        }

        [HttpGet("{Id}")]
        public async Task<ActionResult<CustomerModel>> Get(int Id)
        {
            try
            {
                var result = await _repository.GetCustomerAsync(Id);

                if (result == null) return NotFound();

                return _mapper.Map<CustomerModel>(result);

            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Request Failure");
            }
        }

        [HttpPost]
        public async Task<ActionResult<CustomerModel>> Post(CustomerModel model)
        {
            try
            {
                //Make sure CustomerId is not already taken
                var existing = await _repository.GetCustomerAsync(model.Id);
                if (existing != null)
                {
                    return BadRequest("Customer Id in Use");
                }

                //map
                var customer = _mapper.Map<Customer>(model);

                //save and return
                if (!await _repository.StoreNewCustomerAsync(customer))
                {
                    return BadRequest("Bad request, could not create record!");
                }
                else
                {
                    await _customerService.ProcessNewCustomer(customer);

                    var location = _linkGenerator.GetPathByAction("Get",
                             "Customer",
                            new { customer.Id });

                    return Created(location, _mapper.Map<CustomerModel>(customer));
                }

            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Request Failure");
            }
        }

        [HttpPut("{Id}")]
        public async Task<ActionResult<CustomerModel>> Put(int Id, CustomerModel updatedModel)
        {
            try
            {
                var currentCustomer = await _repository.GetCustomerAsync(Id);
                if (currentCustomer == null) return NotFound($"Could not find customer with Id of {Id}");

                _mapper.Map(updatedModel, currentCustomer);

                if (await _repository.UpdateCustomerAsync(currentCustomer))
                {
                    return _mapper.Map<CustomerModel>(currentCustomer);
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Request Failure");
            }

            return BadRequest();
        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> Delete(int Id)
        {
            try
            {
                var customer = await _repository.GetCustomerAsync(Id);
                if (customer == null) return NotFound();

                if (await _repository.DeleteCustomer(customer))
                {
                    return Ok();
                }

            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Request Failure");
            }

            return BadRequest("Failed to delete the customer");
        }

    }
}