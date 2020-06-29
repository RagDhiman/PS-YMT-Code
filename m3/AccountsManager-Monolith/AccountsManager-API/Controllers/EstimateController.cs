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
    public class EstimateController : ControllerBase
    {
        private readonly IEstimateRepository _repository;
        private readonly IMapper _mapper;
        private readonly LinkGenerator _linkGenerator;
        private readonly ILogger<EstimateController> _logger;

        public EstimateController(IEstimateRepository repository, IMapper mapper, LinkGenerator linkGenerator,
            ILogger<EstimateController> logger)
        {
            _repository = repository;
            _mapper = mapper;
            _linkGenerator = linkGenerator;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<EstimateModel[]>> GetAll(int invoiceId)
        {
            try
            {
                var results = await _repository.GetAllEstimatesAsync(invoiceId);

                return _mapper.Map<EstimateModel[]>(results);
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
        }

        [HttpGet("{Id}")]
        public async Task<ActionResult<EstimateModel>> Get(int invoiceId, int Id)
        {
            try
            {
                var result = await _repository.GetEstimateAsync(invoiceId, Id);

                if (result == null) return NotFound();

                return _mapper.Map<EstimateModel>(result);

            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
        }

        [HttpPost]
        public async Task<ActionResult<EstimateModel>> Post(int invoiceId, EstimateModel model)
        {
            try
            {
                //Make sure EstimateId is not already taken
                var existing = await _repository.GetEstimateAsync(invoiceId, model.Id);
                if (existing != null)
                {
                    return BadRequest("Estimate Id in Use");
                }

                //map
                var Estimate = _mapper.Map<Estimate>(model);

                //save and return
                if (!await _repository.StoreNewEstimateAsync(invoiceId, Estimate))
                {
                    return BadRequest("Bad request, could not create record!");
                }
                else
                {
                    var location = _linkGenerator.GetPathByAction("Get",
                             "Estimate",
                            new { invoiceId = Estimate.InvoiceId, Estimate.Id });

                    return Created(location, _mapper.Map<EstimateModel>(Estimate));
                }

            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
        }

        [HttpPut("{Id}")]
        public async Task<ActionResult<EstimateModel>> Put(int invoiceId, int Id, EstimateModel updatedModel)
        {
            try
            {
                var currentEstimate = await _repository.GetEstimateAsync(invoiceId, Id);
                if (currentEstimate == null) return NotFound($"Could not find Estimate with Id of {Id}");

                _mapper.Map(updatedModel, currentEstimate);

                if (await _repository.UpdateEstimateAsync(invoiceId, currentEstimate))
                {
                    return _mapper.Map<EstimateModel>(currentEstimate);
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
                var Estimate = await _repository.GetEstimateAsync(invoiceId, Id);
                if (Estimate == null) return NotFound();

                if (await _repository.DeleteEstimate(Estimate))
                {
                    return Ok();
                }

            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }

            return BadRequest("Failed to delete the Estimate");
        }

    }
}