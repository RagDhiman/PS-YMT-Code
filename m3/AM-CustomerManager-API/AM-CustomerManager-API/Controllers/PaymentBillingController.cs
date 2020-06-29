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

namespace AM_CustomerManager_API.Controllers
{
    [Route("api/customer/{customerId}/[controller]")]
    [ApiController]
    public class PaymentBillingController : ControllerBase
    {
        private readonly IPaymentBillingRepository _repository;
        private readonly IMapper _mapper;
        private readonly LinkGenerator _linkGenerator;
        private readonly ILogger<PaymentBillingController> _logger;

        public PaymentBillingController(IPaymentBillingRepository repository, IMapper mapper, LinkGenerator linkGenerator,
            ILogger<PaymentBillingController> logger)
        {
            _repository = repository;
            _mapper = mapper;
            _linkGenerator = linkGenerator;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<PaymentBillingModel[]>> Get(int customerId)
        {
            try
            {
                var results = await _repository.GetAllPaymentBillingesAsync(customerId);

                return _mapper.Map<PaymentBillingModel[]>(results);
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Request Failure");
            }
        }

        [HttpGet("{Id}")]
        public async Task<ActionResult<PaymentBillingModel>> Get(int customerId, int Id)
        {
            try
            {
                var result = await _repository.GetPaymentBillingAsync(customerId, Id);

                if (result == null) return NotFound();

                return _mapper.Map<PaymentBillingModel>(result);

            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Request Failure");
            }
        }

        [HttpPost]
        public async Task<ActionResult<PaymentBillingModel>> Post(int customerId, PaymentBillingModel model)
        {
            try
            {
                //Make sure PaymentBillingId is not already taken
                var existing = await _repository.GetPaymentBillingAsync(customerId, model.Id);
                if (existing != null)
                {
                    return BadRequest("PaymentBilling Id in Use");
                }

                //map
                var PaymentBilling = _mapper.Map<PaymentBilling>(model);

                //save and return
                if (!await _repository.StoreNewPaymentBillingAsync(customerId, PaymentBilling))
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
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Request Failure");
            }
        }

        [HttpPut("{Id}")]
        public async Task<ActionResult<PaymentBillingModel>> Put(int customerId, int Id, PaymentBillingModel updatedModel)
        {
            try
            {
                var currentPaymentBilling = await _repository.GetPaymentBillingAsync(customerId, Id);
                if (currentPaymentBilling == null) return NotFound($"Could not find PaymentBilling with Id of {Id}");

                _mapper.Map(updatedModel, currentPaymentBilling);

                if (await _repository.UpdatePaymentBillingAsync(customerId, currentPaymentBilling))
                {
                    return _mapper.Map<PaymentBillingModel>(currentPaymentBilling);
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
        public async Task<IActionResult> Delete(int customerId, int Id)
        {
            try
            {
                var PaymentBilling = await _repository.GetPaymentBillingAsync(customerId, Id);
                if (PaymentBilling == null) return NotFound();

                if (await _repository.DeletePaymentBilling(PaymentBilling))
                {
                    return Ok();
                }

            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Request Failure");
            }

            return BadRequest("Failed to delete the PaymentBilling");
        }

    }
}