using AM_InvoiceManager_API.Models;
using AM_InvoiceManager_Core;
using AM_InvoiceManager_Core.DataAccess;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
namespace AM_CustomerManager_API.Controllers
{
    [Route("api/invoice/{invoiceId}/[controller]")]
    [ApiController]
    public class DelayedChargeController : ControllerBase
    {
        private readonly IDelayedChargeRepository _repository;
        private readonly IMapper _mapper;
        private readonly LinkGenerator _linkGenerator;
        private readonly ILogger<DelayedChargeController> _logger;

        public DelayedChargeController(IDelayedChargeRepository repository, IMapper mapper, LinkGenerator linkGenerator,
            ILogger<DelayedChargeController> logger)
        {
            _repository = repository;
            _mapper = mapper;
            _linkGenerator = linkGenerator;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<DelayedChargeModel[]>> Get(int invoiceId)
        {
            try
            {
                var results = await _repository.GetAllDelayedChargeesAsync(invoiceId);

                return _mapper.Map<DelayedChargeModel[]>(results);
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
        }

        [HttpGet("{Id}")]
        public async Task<ActionResult<DelayedChargeModel>> Get(int invoiceId, int Id)
        {
            try
            {
                var result = await _repository.GetDelayedChargeAsync(invoiceId, Id);

                if (result == null) return NotFound();

                return _mapper.Map<DelayedChargeModel>(result);

            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
        }

        [HttpPost]
        public async Task<ActionResult<DelayedChargeModel>> Post(int invoiceId, DelayedChargeModel model)
        {
            try
            {
                //Make sure DelayedChargeId is not already taken
                var existing = await _repository.GetDelayedChargeAsync(invoiceId, model.Id);
                if (existing != null)
                {
                    return BadRequest("DelayedCharge Id in Use");
                }

                //map
                var DelayedCharge = _mapper.Map<DelayedCharge>(model);

                //save and return
                if (!await _repository.StoreNewDelayedChargeAsync(invoiceId, DelayedCharge))
                {
                    return BadRequest("Bad request, could not create record!");
                }
                else
                {
                    var location = _linkGenerator.GetPathByAction("Get",
                             "DelayedCharge",
                            new { invoiceId = DelayedCharge.InvoiceId, DelayedCharge.Id });

                    return Created(location, _mapper.Map<DelayedChargeModel>(DelayedCharge));
                }

            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
        }

        [HttpPut("{Id}")]
        public async Task<ActionResult<DelayedChargeModel>> Put(int invoiceId, int Id, DelayedChargeModel updatedModel)
        {
            try
            {
                var currentDelayedCharge = await _repository.GetDelayedChargeAsync(invoiceId, Id);
                if (currentDelayedCharge == null) return NotFound($"Could not find DelayedCharge with Id of {Id}");

                _mapper.Map(updatedModel, currentDelayedCharge);

                if (await _repository.UpdateDelayedChargeAsync(invoiceId, currentDelayedCharge))
                {
                    return _mapper.Map<DelayedChargeModel>(currentDelayedCharge);
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
                var DelayedCharge = await _repository.GetDelayedChargeAsync(invoiceId, Id);
                if (DelayedCharge == null) return NotFound();

                if (await _repository.DeleteDelayedCharge(DelayedCharge))
                {
                    return Ok();
                }

            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }

            return BadRequest("Failed to delete the DelayedCharge");
        }

    }
}