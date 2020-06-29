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
    [Route("api/customer/{customerId}/[controller]")]
    [ApiController]
    public class PaymentBillingController : ControllerBase
    {
        private readonly ICustomerManagerRepository<PaymentBilling> _repository;
        private readonly IMapper _mapper;
        private readonly LinkGenerator _linkGenerator;
        private readonly ILogger<PaymentBillingController> _logger;

        public PaymentBillingController(ICustomerManagerRepository<PaymentBilling> repository, IMapper mapper, LinkGenerator linkGenerator,
            ILogger<PaymentBillingController> logger)
        {
            _repository = repository;
            _mapper = mapper;
            _linkGenerator = linkGenerator;
            _logger = logger;
        }

        private void SetupPath(int customerId)
        {
            _repository.ResourcePath = $"api/customer/{customerId}/address";
        }

        [HttpGet]
        public async Task<ActionResult<PaymentBillingModel[]>> Get(int customerId)
        {
            try
            {
                SetupPath(customerId);

                var results = await _repository.GetAllAsync();

                return _mapper.Map<PaymentBillingModel[]>(results);
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Failed Request");
            }
        }

        [HttpGet("{Id}")]
        public async Task<ActionResult<PaymentBillingModel>> Get(int customerId, int Id)
        {
            try
            {
                SetupPath(customerId);

                var result = await _repository.GetByIdAsync(Id);

                if (result == null) return NotFound();

                return _mapper.Map<PaymentBillingModel>(result);

            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Failed Request");
            }
        }

        [HttpPost]
        public async Task<ActionResult<PaymentBillingModel>> Post(int customerId, PaymentBillingModel model)
        {
            try
            {
                SetupPath(customerId);

                //Make sure PaymentBillingId is not already taken
                var existing = await _repository.GetByIdAsync(model.Id);
                if (existing != null)
                {
                    return BadRequest("PaymentBilling Id in Use");
                }

                //map
                var PaymentBilling = _mapper.Map<PaymentBilling>(model);

                //save and return
                if (!await _repository.StoreNewAsync(PaymentBilling))
                {
                    return BadRequest("Bad request, could not create record!");
                }
                else
                {
                    var location = _linkGenerator.GetPathByAction("Get",
                             "PaymentBilling",
                            new { Id = PaymentBilling.Id });

                    return Created(location, _mapper.Map<PaymentBillingModel>(PaymentBilling));
                }

            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Failed Request");
            }
        }

        [HttpPut("{Id}")]
        public async Task<ActionResult<PaymentBillingModel>> Put(int customerId, int Id, PaymentBillingModel updatedModel)
        {
            try
            {
                SetupPath(customerId);

                var currentPaymentBilling = await _repository.GetByIdAsync(Id);
                if (currentPaymentBilling == null) return NotFound($"Could not find PaymentBilling with Id of {Id}");

                _mapper.Map(updatedModel, currentPaymentBilling);

                if (await _repository.UpdateAsync(currentPaymentBilling))
                {
                    return _mapper.Map<PaymentBillingModel>(currentPaymentBilling);
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
        public async Task<IActionResult> Delete(int customerId, int Id)
        {
            try
            {
                SetupPath(customerId);

                var PaymentBilling = await _repository.GetByIdAsync(Id);
                if (PaymentBilling == null) return NotFound();

                if (await _repository.DeleteAsync(PaymentBilling))
                {
                    return Ok();
                }

            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Failed Request");
            }

            return BadRequest("Failed to delete the PaymentBilling");
        }

    }
}