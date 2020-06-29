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
    public class DelayedChargeController : ControllerBase
    {
        private readonly IInvoiceManagerRepository<DelayedCharge> _repository;
        private readonly IMapper _mapper;
        private readonly LinkGenerator _linkGenerator;
        private readonly ILogger<DelayedChargeController> _logger;

        public DelayedChargeController(IInvoiceManagerRepository<DelayedCharge> repository, IMapper mapper, LinkGenerator linkGenerator,
            ILogger<DelayedChargeController> logger)
        {
            _repository = repository;
            _mapper = mapper;
            _linkGenerator = linkGenerator;
            _logger = logger;
        }

        private void SetupPath(int invoiceId)
        {
            _repository.ResourcePath = $"api/invoice/{invoiceId}/DelayedCharge";
        }

        [HttpGet]
        public async Task<ActionResult<DelayedChargeModel[]>> Get(int invoiceId)
        {
            try
            {
                SetupPath(invoiceId);

                var results = await _repository.GetAllAsync();

                return _mapper.Map<DelayedChargeModel[]>(results);
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Failed Request");
            }
        }

        [HttpGet("{Id}")]
        public async Task<ActionResult<DelayedChargeModel>> Get(int invoiceId, int Id)
        {
            try
            {
                SetupPath(invoiceId);

                var result = await _repository.GetByIdAsync(Id);

                if (result == null) return NotFound();

                return _mapper.Map<DelayedChargeModel>(result);

            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Failed Request");
            }
        }

        [HttpPost]
        public async Task<ActionResult<DelayedChargeModel>> Post(int invoiceId, DelayedChargeModel model)
        {
            try
            {
                SetupPath(invoiceId);

                //Make sure DelayedChargeId is not already taken
                var existing = await _repository.GetByIdAsync(model.Id);
                if (existing != null)
                {
                    return BadRequest("DelayedCharge Id in Use");
                }

                //map
                var DelayedCharge = _mapper.Map<DelayedCharge>(model);

                //save and return
                if (!await _repository.StoreNewAsync(DelayedCharge))
                {
                    return BadRequest("Bad request, could not create record!");
                }
                else
                {
                    var location = _linkGenerator.GetPathByAction("Get",
                             "DelayedCharge",
                            new { Id = DelayedCharge.Id });

                    return Created(location, _mapper.Map<DelayedChargeModel>(DelayedCharge));
                }

            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Failed Request");
            }
        }

        [HttpPut("{Id}")]
        public async Task<ActionResult<DelayedChargeModel>> Put(int invoiceId, int Id, DelayedChargeModel updatedModel)
        {
            try
            {
                SetupPath(invoiceId);

                var currentDelayedCharge = await _repository.GetByIdAsync(Id);
                if (currentDelayedCharge == null) return NotFound($"Could not find DelayedCharge with Id of {Id}");

                _mapper.Map(updatedModel, currentDelayedCharge);

                if (await _repository.UpdateAsync(currentDelayedCharge))
                {
                    return _mapper.Map<DelayedChargeModel>(currentDelayedCharge);
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

                var DelayedCharge = await _repository.GetByIdAsync(Id);
                if (DelayedCharge == null) return NotFound();

                if (await _repository.DeleteAsync(DelayedCharge))
                {
                    return Ok();
                }

            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Failed Request");
            }

            return BadRequest("Failed to delete the DelayedCharge");
        }

    }
}