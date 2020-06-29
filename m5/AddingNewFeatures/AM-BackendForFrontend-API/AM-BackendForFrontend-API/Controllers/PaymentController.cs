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
    [Route("api/invoice/{invoiceId}/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IAccountManagerRepository<Payment> _repository;
        private readonly IMapper _mapper;
        private readonly LinkGenerator _linkGenerator;
        private readonly ILogger<PaymentController> _logger;

        public PaymentController(IAccountManagerRepository<Payment> repository, IMapper mapper, LinkGenerator linkGenerator,
            ILogger<PaymentController> logger)
        {
            _repository = repository;
            _mapper = mapper;
            _linkGenerator = linkGenerator;
            _logger = logger;
        }

        private void SetupPath(int invoiceId)
        {
            _repository.ResourcePath = $"api/invoice/{invoiceId}/Payment";
        }

        [HttpGet]
        public async Task<ActionResult<PaymentModel[]>> Get(int invoiceId)
        {
            try
            {
                SetupPath(invoiceId);

                var results = await _repository.GetAllAsync();

                return _mapper.Map<PaymentModel[]>(results);
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Failed Request");
            }
        }

        [HttpGet("{Id}")]
        public async Task<ActionResult<PaymentModel>> Get(int invoiceId, int Id)
        {
            try
            {
                SetupPath(invoiceId);

                var result = await _repository.GetByIdAsync(Id);

                if (result == null) return NotFound();

                return _mapper.Map<PaymentModel>(result);

            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Failed Request");
            }
        }

        [HttpPost]
        public async Task<ActionResult<PaymentModel>> Post(int invoiceId, PaymentModel model)
        {
            try
            {
                SetupPath(invoiceId);

                //Make sure PaymentId is not already taken
                var existing = await _repository.GetByIdAsync(model.Id);
                if (existing != null)
                {
                    return BadRequest("Payment Id in Use");
                }

                //map
                var Payment = _mapper.Map<Payment>(model);

                //save and return
                if (!await _repository.StoreNewAsync(Payment))
                {
                    return BadRequest("Bad request, could not create record!");
                }
                else
                {
                    var location = _linkGenerator.GetPathByAction("Get",
                             "Payment",
                            new { Id = Payment.Id });

                    return Created(location, _mapper.Map<PaymentModel>(Payment));
                }

            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Failed Request");
            }
        }

        [HttpPut("{Id}")]
        public async Task<ActionResult<PaymentModel>> Put(int invoiceId, int Id, PaymentModel updatedModel)
        {
            try
            {
                SetupPath(invoiceId);

                var currentPayment = await _repository.GetByIdAsync(Id);
                if (currentPayment == null) return NotFound($"Could not find Payment with Id of {Id}");

                _mapper.Map(updatedModel, currentPayment);

                if (await _repository.UpdateAsync(currentPayment))
                {
                    return _mapper.Map<PaymentModel>(currentPayment);
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
        public async Task<IActionResult> Delete(int invoiceId, int Id)
        {
            try
            {
                SetupPath(invoiceId);

                var Payment = await _repository.GetByIdAsync(Id);
                if (Payment == null) return NotFound();

                if (await _repository.DeleteAsync(Payment))
                {
                    return Ok();
                }

            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Failed Request");
            }

            return BadRequest("Failed to delete the Payment");
        }

    }
}