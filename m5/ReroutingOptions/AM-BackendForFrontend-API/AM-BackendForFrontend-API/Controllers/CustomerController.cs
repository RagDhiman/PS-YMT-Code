using AM_BackendForFrontend_API.Models;
using AM_BackendForFrontend_Core;
using AM_BackendForFrontend_Data.Data;
using AM_BackendForFrontend_Data.Generic;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace AM_BackendForFrontend_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerManagerRepository<Customer> _repository;
        private readonly IMapper _mapper;
        private readonly LinkGenerator _linkGenerator;
        private readonly ILogger<CustomerController> _logger;

        public CustomerController(ICustomerManagerRepository<Customer> repository, IMapper mapper, LinkGenerator linkGenerator,
            ILogger<CustomerController> logger)
        {
            _repository = repository;
            _repository.ResourcePath = "api/Customer";

            _mapper = mapper;
            _linkGenerator = linkGenerator;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<CustomerModel[]>> Get()
        {
            try
            {
                var results = await _repository.GetAllAsync();

                return _mapper.Map<CustomerModel[]>(results);
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Failed Request");
            }
        }

        [HttpGet("{Id}")]
        public async Task<ActionResult<CustomerModel>> Get(int Id)
        {
            try
            {
                var result = await _repository.GetByIdAsync(Id);

                if (result == null) return NotFound();

                return _mapper.Map<CustomerModel>(result);

            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Failed Request");
            }
        }

        [HttpPost]
        public async Task<ActionResult<CustomerModel>> Post(CustomerModel model)
        {
            try
            {
                //Make sure CustomerId is not already taken
                var existing = await _repository.GetByIdAsync(model.Id);
                if (existing != null && existing.Id >0)
                {
                    return BadRequest("Customer Id in Use");
                }

                //map
                var Customer = _mapper.Map<Customer>(model);

                //save and return
                if (!await _repository.StoreNewAsync(Customer))
                {
                    return BadRequest("Bad request, could not create record!");
                }
                else
                {
                    var location = _linkGenerator.GetPathByAction("Get",
                             "Customer",
                            new { Id = Customer.Id });

                    return Created(location, _mapper.Map<CustomerModel>(Customer));
                }

            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Failed Request");
            }
        }

        [HttpPut("{Id}")]
        public async Task<ActionResult<CustomerModel>> Put(int Id, CustomerModel updatedModel)
        {
            try
            {
                var currentCustomer = await _repository.GetByIdAsync(Id);
                if (currentCustomer == null) return NotFound($"Could not find Customer with Id of {Id}");

                _mapper.Map(updatedModel, currentCustomer);

                if (await _repository.UpdateAsync(currentCustomer))
                {
                    return _mapper.Map<CustomerModel>(currentCustomer);
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Failed Request");
            }

            return BadRequest();
        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> Delete(int Id)
        {
            try
            {
                var Customer = await _repository.GetByIdAsync(Id);
                if (Customer == null) return NotFound();

                if (await _repository.DeleteAsync(Customer))
                {
                    return Ok();
                }

            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Failed Request");
            }

            return BadRequest("Failed to delete the Customer");
        }

    }
}