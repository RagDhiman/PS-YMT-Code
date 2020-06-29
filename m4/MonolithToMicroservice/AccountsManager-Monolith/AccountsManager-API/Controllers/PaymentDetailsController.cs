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
    [Route("api/account/{accountId}/[controller]")]
    [ApiController]
    public class PaymentDetailsController : ControllerBase
    {
        private readonly IPaymentDetailsRepository _repository;
        private readonly IMapper _mapper;
        private readonly LinkGenerator _linkGenerator;
        private readonly ILogger<PaymentDetailsController> _logger;

        public PaymentDetailsController(IPaymentDetailsRepository repository, IMapper mapper, LinkGenerator linkGenerator,
            ILogger<PaymentDetailsController> logger)
        {
            _repository = repository;
            _mapper = mapper;
            _linkGenerator = linkGenerator;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<PaymentDetailsModel[]>> GetAll(int accountId)
        {
            try
            {
                var results = await _repository.GetAllPaymentDetailsAsync(accountId);

                return _mapper.Map<PaymentDetailsModel[]>(results);
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
        }

        [HttpGet("{Id}")]
        public async Task<ActionResult<PaymentDetailsModel>> Get(int AccountId, int Id)
        {
            try
            {
                var result = await _repository.GetPaymentDetailsAsync(AccountId, Id);

                if (result == null) return NotFound();

                return _mapper.Map<PaymentDetailsModel>(result);

            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
        }

        [HttpPost]
        public async Task<ActionResult<PaymentDetailsModel>> Post(int AccountId, PaymentDetailsModel model)
        {
            try
            {
                //Make sure PaymentDetailsId is not already taken
                var existing = await _repository.GetPaymentDetailsAsync(AccountId, model.Id);
                if (existing != null)
                {
                    return BadRequest("PaymentDetails Id in Use");
                }

                //map
                var PaymentDetails = _mapper.Map<PaymentDetails>(model);

                //save and return
                if (!await _repository.StoreNewPaymentDetailsAsync(AccountId, PaymentDetails))
                {
                    return BadRequest("Bad request, could not create record!");
                }
                else
                {
                    var location = _linkGenerator.GetPathByAction("Get",
                             "PaymentDetails",
                            new { PaymentDetails.AccountId, PaymentDetails.Id });

                    return Created(location, _mapper.Map<PaymentDetailsModel>(PaymentDetails));
                }

            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
        }

        [HttpPut("{Id}")]
        public async Task<ActionResult<PaymentDetailsModel>> Put(int AccountId,int Id, PaymentDetailsModel updatedModel)
        {
            try
            {
                var currentPaymentDetails = await _repository.GetPaymentDetailsAsync(AccountId, Id);
                if (currentPaymentDetails == null) return NotFound($"Could not find PaymentDetails with Id of {Id}");

                _mapper.Map(updatedModel, currentPaymentDetails);

                if (await _repository.UpdatePaymentDetailsAsync(AccountId, currentPaymentDetails))
                {
                    return _mapper.Map<PaymentDetailsModel>(currentPaymentDetails);
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
        public async Task<IActionResult> Delete(int AccountId, int Id)
        {
            try
            {
                var PaymentDetails = await _repository.GetPaymentDetailsAsync(AccountId, Id);
                if (PaymentDetails == null) return NotFound();

                if (await _repository.DeletePaymentDetails(PaymentDetails))
                {
                    return Ok();
                }

            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }

            return BadRequest("Failed to delete the PaymentDetails");
        }

    }
}