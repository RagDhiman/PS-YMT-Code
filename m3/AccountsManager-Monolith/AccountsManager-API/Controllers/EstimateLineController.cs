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
    [Route("api/Estimate/{EstimateId}/[controller]")]
    [ApiController]
    public class EstimateLineController : ControllerBase
    {
        private readonly IEstimateLineRepository _repository;
        private readonly IMapper _mapper;
        private readonly LinkGenerator _linkGenerator;
        private readonly ILogger<EstimateLineController> _logger;

        public EstimateLineController(IEstimateLineRepository repository, IMapper mapper, LinkGenerator linkGenerator,
            ILogger<EstimateLineController> logger)
        {
            _repository = repository;
            _mapper = mapper;
            _linkGenerator = linkGenerator;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<EstimateLineModel[]>> GetAll(int EstimateId)
        {
            try
            {
                var results = await _repository.GetAllEstimateLinesAsync(EstimateId);

                return _mapper.Map<EstimateLineModel[]>(results);
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
        }

        [HttpGet("{Id}")]
        public async Task<ActionResult<EstimateLineModel>> Get(int EstimateId, int Id)
        {
            try
            {
                var result = await _repository.GetEstimateLineAsync(EstimateId, Id);

                if (result == null) return NotFound();

                return _mapper.Map<EstimateLineModel>(result);

            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
        }

        [HttpPost]
        public async Task<ActionResult<EstimateLineModel>> Post(int EstimateId, EstimateLineModel model)
        {
            try
            {
                //Make sure EstimateLineId is not already taken
                var existing = await _repository.GetEstimateLineAsync(EstimateId, model.Id);
                if (existing != null)
                {
                    return BadRequest("EstimateLine Id in Use");
                }

                //map
                var EstimateLine = _mapper.Map<EstimateLine>(model);

                //save and return
                if (!await _repository.StoreNewEstimateLineAsync(EstimateId, EstimateLine))
                {
                    return BadRequest("Bad request, could not create record!");
                }
                else
                {
                    var location = _linkGenerator.GetPathByAction("Get",
                             "EstimateLine",
                            new { EstimateLine.EstimateId, EstimateLine.Id });

                    return Created(location, _mapper.Map<EstimateLineModel>(EstimateLine));
                }

            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
        }

        [HttpPut("{Id}")]
        public async Task<ActionResult<EstimateLineModel>> Put(int EstimateId, int Id, EstimateLineModel updatedModel)
        {
            try
            {
                var currentEstimateLine = await _repository.GetEstimateLineAsync(EstimateId, Id);
                if (currentEstimateLine == null) return NotFound($"Could not find EstimateLine with Id of {Id}");

                _mapper.Map(updatedModel, currentEstimateLine);

                if (await _repository.UpdateEstimateLineAsync(EstimateId, currentEstimateLine))
                {
                    return _mapper.Map<EstimateLineModel>(currentEstimateLine);
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
        public async Task<IActionResult> Delete(int EstimateId, int Id)
        {
            try
            {
                var EstimateLine = await _repository.GetEstimateLineAsync(EstimateId, Id);
                if (EstimateLine == null) return NotFound();

                if (await _repository.DeleteEstimateLine(EstimateLine))
                {
                    return Ok();
                }

            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }

            return BadRequest("Failed to delete the EstimateLine");
        }

    }
}