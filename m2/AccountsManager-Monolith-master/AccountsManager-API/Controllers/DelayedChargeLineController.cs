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
    [Route("api/delayedcharge/{DelayedChargeId}/[controller]")]
    [ApiController]
    public class DelayedChargeLineController : ControllerBase
    {
        private readonly IDelayedChargeLineRepository _repository;
        private readonly IMapper _mapper;
        private readonly LinkGenerator _linkGenerator;
        private readonly ILogger<DelayedChargeLineController> _logger;

        public DelayedChargeLineController(IDelayedChargeLineRepository repository, IMapper mapper, LinkGenerator linkGenerator,
            ILogger<DelayedChargeLineController> logger)
        {
            _repository = repository;
            _mapper = mapper;
            _linkGenerator = linkGenerator;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<DelayedChargeLineModel[]>> GetAll(int DelayedChargeId)
        {
            try
            {
                var results = await _repository.GetAllDelayedChargeLinesAsync(DelayedChargeId);

                return _mapper.Map<DelayedChargeLineModel[]>(results);
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
        }

        [HttpGet("{Id}")]
        public async Task<ActionResult<DelayedChargeLineModel>> Get(int DelayedChargeId, int Id)
        {
            try
            {
                var result = await _repository.GetDelayedChargeLineAsync(DelayedChargeId, Id);

                if (result == null) return NotFound();

                return _mapper.Map<DelayedChargeLineModel>(result);

            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
        }

        [HttpPost]
        public async Task<ActionResult<DelayedChargeLineModel>> Post(int DelayedChargeId, DelayedChargeLineModel model)
        {
            try
            {
                //Make sure DelayedChargeLineId is not already taken
                var existing = await _repository.GetDelayedChargeLineAsync(DelayedChargeId, model.Id);
                if (existing != null)
                {
                    return BadRequest("DelayedChargeLine Id in Use");
                }

                //map
                var DelayedChargeLine = _mapper.Map<DelayedChargeLine>(model);

                //save and return
                if (!await _repository.StoreNewDelayedChargeLineAsync(DelayedChargeId, DelayedChargeLine))
                {
                    return BadRequest("Bad request, could not create record!");
                }
                else
                {
                    var location = _linkGenerator.GetPathByAction("Get",
                             "DelayedChargeLine",
                            new { DelayedChargeLine.DelayedChargeId, DelayedChargeLine.Id });

                    return Created(location, _mapper.Map<DelayedChargeLineModel>(DelayedChargeLine));
                }

            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
        }

        [HttpPut("{Id}")]
        public async Task<ActionResult<DelayedChargeLineModel>> Put(int DelayedChargeId, int Id, DelayedChargeLineModel updatedModel)
        {
            try
            {
                var currentDelayedChargeLine = await _repository.GetDelayedChargeLineAsync(DelayedChargeId, Id);
                if (currentDelayedChargeLine == null) return NotFound($"Could not find DelayedChargeLine with Id of {Id}");

                _mapper.Map(updatedModel, currentDelayedChargeLine);

                if (await _repository.UpdateDelayedChargeLineAsync(DelayedChargeId, currentDelayedChargeLine))
                {
                    return _mapper.Map<DelayedChargeLineModel>(currentDelayedChargeLine);
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
        public async Task<IActionResult> Delete(int DelayedChargeId, int Id)
        {
            try
            {
                var DelayedChargeLine = await _repository.GetDelayedChargeLineAsync(DelayedChargeId, Id);
                if (DelayedChargeLine == null) return NotFound();

                if (await _repository.DeleteDelayedChargeLine(DelayedChargeLine))
                {
                    return Ok();
                }

            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }

            return BadRequest("Failed to delete the DelayedChargeLine");
        }

    }
}