using AccountsManager_API.Models;
using AccountsManager_Domain;
using AccountsManager_Domain.DataAccess;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace AccountsManager_API.Controllers
{
    [Route("api/invoice/{invoiceId}/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentRepository _repository;
        private readonly IMapper _mapper;
        private readonly LinkGenerator _linkGenerator;
        private readonly ILogger<PaymentController> _logger;

        public PaymentController(IPaymentRepository repository, IMapper mapper, LinkGenerator linkGenerator,
            ILogger<PaymentController> logger)
        {
            _repository = repository;
            _mapper = mapper;
            _linkGenerator = linkGenerator;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<PaymentModel[]>> GetAll(int invoiceId)
        {
            try
            {
                var results = await _repository.GetAllPaymentsAsync(invoiceId);

                return _mapper.Map<PaymentModel[]>(results);
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
        }

        [HttpGet("{Id}")]
        public async Task<ActionResult<PaymentModel>> Get(int invoiceId, int Id)
        {
            try
            {
                var result = await _repository.GetPaymentAsync(invoiceId, Id);

                if (result == null) return NotFound();

                return _mapper.Map<PaymentModel>(result);

            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
        }

        [HttpPost]
        public async Task<ActionResult<PaymentModel>> Post(int invoiceId, PaymentModel model)
        {
            try
            {
                //Make sure PaymentId is not already taken
                var existing = await _repository.GetPaymentAsync(invoiceId, model.Id);
                if (existing != null)
                {
                    return BadRequest("Payment Id in Use");
                }

                //map
                var Payment = _mapper.Map<Payment>(model);

                //save and return
                if (!await _repository.StoreNewPaymentAsync(invoiceId, Payment))
                {
                    return BadRequest("Bad request, could not create record!");
                }
                else
                {
                    var location = _linkGenerator.GetPathByAction("Get",
                             "Payment",
                            new { invoiceId = Payment.InvoiceId, Payment.Id });

                    return Created(location, _mapper.Map<PaymentModel>(Payment));
                }

            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
        }

        [HttpPut("{Id}")]
        public async Task<ActionResult<PaymentModel>> Put(int invoiceId, int Id, PaymentModel updatedModel)
        {
            try
            {
                var currentPayment = await _repository.GetPaymentAsync(invoiceId, Id);
                if (currentPayment == null) return NotFound($"Could not find Payment with Id of {Id}");

                _mapper.Map(updatedModel, currentPayment);

                if (await _repository.UpdatePaymentAsync(invoiceId, currentPayment))
                {
                    return _mapper.Map<PaymentModel>(currentPayment);
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }

            return BadRequest();
        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> Delete(int invoiceId, int Id)
        {
            try
            {
                var Payment = await _repository.GetPaymentAsync(invoiceId, Id);
                if (Payment == null) return NotFound();

                if (await _repository.DeletePayment(Payment))
                {
                    return Ok();
                }

            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }

            return BadRequest("Failed to delete the Payment");
        }

    }
}