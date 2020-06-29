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
    [Route("api/Estimate/{EstimateId}/[controller]")]
    [ApiController]
    public class EstimateLineController : ControllerBase
    {
        private readonly IAccountManagerRepository<EstimateLine> _repository;
        private readonly IMapper _mapper;
        private readonly LinkGenerator _linkGenerator;
        private readonly ILogger<EstimateLineController> _logger;

        public EstimateLineController(IAccountManagerRepository<EstimateLine> repository, IMapper mapper, LinkGenerator linkGenerator,
            ILogger<EstimateLineController> logger)
        {
            _repository = repository;
            _mapper = mapper;
            _linkGenerator = linkGenerator;
            _logger = logger;
        }

        private void SetupPath(int EstimateId)
        {
            _repository.ResourcePath = $"api/Estimate/{EstimateId}/EstimateLine";
        }

        [HttpGet]
        public async Task<ActionResult<EstimateLineModel[]>> Get(int EstimateId)
        {
            try
            {
                SetupPath(EstimateId);

                var results = await _repository.GetAllAsync();

                return _mapper.Map<EstimateLineModel[]>(results);
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Failed Request");
            }
        }

        [HttpGet("{Id}")]
        public async Task<ActionResult<EstimateLineModel>> Get(int EstimateId, int Id)
        {
            try
            {
                SetupPath(EstimateId);

                var result = await _repository.GetByIdAsync(Id);

                if (result == null) return NotFound();

                return _mapper.Map<EstimateLineModel>(result);

            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Failed Request");
            }
        }

        [HttpPost]
        public async Task<ActionResult<EstimateLineModel>> Post(int EstimateId, EstimateLineModel model)
        {
            try
            {
                SetupPath(EstimateId);

                //Make sure EstimateLineId is not already taken
                var existing = await _repository.GetByIdAsync(model.Id);
                if (existing != null)
                {
                    return BadRequest("EstimateLine Id in Use");
                }

                //map
                var EstimateLine = _mapper.Map<EstimateLine>(model);

                //save and return
                if (!await _repository.StoreNewAsync(EstimateLine))
                {
                    return BadRequest("Bad request, could not create record!");
                }
                else
                {
                    var location = _linkGenerator.GetPathByAction("Get",
                             "EstimateLine",
                            new { Id = EstimateLine.Id });

                    return Created(location, _mapper.Map<EstimateLineModel>(EstimateLine));
                }

            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Failed Request");
            }
        }

        [HttpPut("{Id}")]
        public async Task<ActionResult<EstimateLineModel>> Put(int EstimateId, int Id, EstimateLineModel updatedModel)
        {
            try
            {
                SetupPath(EstimateId);

                var currentEstimateLine = await _repository.GetByIdAsync(Id);
                if (currentEstimateLine == null) return NotFound($"Could not find EstimateLine with Id of {Id}");

                _mapper.Map(updatedModel, currentEstimateLine);

                if (await _repository.UpdateAsync(currentEstimateLine))
                {
                    return _mapper.Map<EstimateLineModel>(currentEstimateLine);
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
        public async Task<IActionResult> Delete(int EstimateId, int Id)
        {
            try
            {
                SetupPath(EstimateId);

                var EstimateLine = await _repository.GetByIdAsync(Id);
                if (EstimateLine == null) return NotFound();

                if (await _repository.DeleteAsync(EstimateLine))
                {
                    return Ok();
                }

            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Failed Request");
            }

            return BadRequest("Failed to delete the EstimateLine");
        }

    }
}