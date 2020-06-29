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
    public class EstimateController : ControllerBase
    {
        private readonly IAccountManagerRepository<Estimate> _repository;
        private readonly IMapper _mapper;
        private readonly LinkGenerator _linkGenerator;
        private readonly ILogger<EstimateController> _logger;

        public EstimateController(IAccountManagerRepository<Estimate> repository, IMapper mapper, LinkGenerator linkGenerator,
            ILogger<EstimateController> logger)
        {
            _repository = repository;
            _mapper = mapper;
            _linkGenerator = linkGenerator;
            _logger = logger;
        }

        private void SetupPath(int invoiceId)
        {
            _repository.ResourcePath = $"api/invoice/{invoiceId}/Estimate";
        }

        [HttpGet]
        public async Task<ActionResult<EstimateModel[]>> Get(int invoiceId)
        {
            try
            {
                SetupPath(invoiceId);

                var results = await _repository.GetAllAsync();

                return _mapper.Map<EstimateModel[]>(results);
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Failed Request");
            }
        }

        [HttpGet("{Id}")]
        public async Task<ActionResult<EstimateModel>> Get(int invoiceId, int Id)
        {
            try
            {
                SetupPath(invoiceId);

                var result = await _repository.GetByIdAsync(Id);

                if (result == null) return NotFound();

                return _mapper.Map<EstimateModel>(result);

            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Failed Request");
            }
        }

        [HttpPost]
        public async Task<ActionResult<EstimateModel>> Post(int invoiceId, EstimateModel model)
        {
            try
            {
                SetupPath(invoiceId);

                //Make sure EstimateId is not already taken
                var existing = await _repository.GetByIdAsync(model.Id);
                if (existing != null)
                {
                    return BadRequest("Estimate Id in Use");
                }

                //map
                var Estimate = _mapper.Map<Estimate>(model);

                //save and return
                if (!await _repository.StoreNewAsync(Estimate))
                {
                    return BadRequest("Bad request, could not create record!");
                }
                else
                {
                    var location = _linkGenerator.GetPathByAction("Get",
                             "Estimate",
                            new { Id = Estimate.Id });

                    return Created(location, _mapper.Map<EstimateModel>(Estimate));
                }

            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Failed Request");
            }
        }

        [HttpPut("{Id}")]
        public async Task<ActionResult<EstimateModel>> Put(int invoiceId, int Id, EstimateModel updatedModel)
        {
            try
            {
                SetupPath(invoiceId);

                var currentEstimate = await _repository.GetByIdAsync(Id);
                if (currentEstimate == null) return NotFound($"Could not find Estimate with Id of {Id}");

                _mapper.Map(updatedModel, currentEstimate);

                if (await _repository.UpdateAsync(currentEstimate))
                {
                    return _mapper.Map<EstimateModel>(currentEstimate);
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

                var Estimate = await _repository.GetByIdAsync(Id);
                if (Estimate == null) return NotFound();

                if (await _repository.DeleteAsync(Estimate))
                {
                    return Ok();
                }

            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Failed Request");
            }

            return BadRequest("Failed to delete the Estimate");
        }

    }
}