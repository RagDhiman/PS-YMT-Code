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
    [Route("api/delayedcharge/{delayedchargeId}/[controller]")]
    [ApiController]
    public class DelayedChargeLineController : ControllerBase
    {
        private readonly IAccountManagerRepository<DelayedChargeLine> _repository;
        private readonly IMapper _mapper;
        private readonly LinkGenerator _linkGenerator;
        private readonly ILogger<DelayedChargeLineController> _logger;

        public DelayedChargeLineController(IAccountManagerRepository<DelayedChargeLine> repository, IMapper mapper, LinkGenerator linkGenerator,
            ILogger<DelayedChargeLineController> logger)
        {
            _repository = repository;
            _mapper = mapper;
            _linkGenerator = linkGenerator;
            _logger = logger;
        }

        private void SetupPath(int delayedchargeId)
        {
            _repository.ResourcePath = $"api/delayedcharge/{delayedchargeId}/DelayedChargeLine";
        }

        [HttpGet]
        public async Task<ActionResult<DelayedChargeLineModel[]>> Get(int delayedchargeId)
        {
            try
            {
                SetupPath(delayedchargeId);

                var results = await _repository.GetAllAsync();

                return _mapper.Map<DelayedChargeLineModel[]>(results);
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Failed Request");
            }
        }

        [HttpGet("{Id}")]
        public async Task<ActionResult<DelayedChargeLineModel>> Get(int delayedchargeId, int Id)
        {
            try
            {
                SetupPath(delayedchargeId);

                var result = await _repository.GetByIdAsync(Id);

                if (result == null) return NotFound();

                return _mapper.Map<DelayedChargeLineModel>(result);

            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Failed Request");
            }
        }

        [HttpPost]
        public async Task<ActionResult<DelayedChargeLineModel>> Post(int delayedchargeId, DelayedChargeLineModel model)
        {
            try
            {
                SetupPath(delayedchargeId);

                //Make sure DelayedChargeLineId is not already taken
                var existing = await _repository.GetByIdAsync(model.Id);
                if (existing != null)
                {
                    return BadRequest("DelayedChargeLine Id in Use");
                }

                //map
                var DelayedChargeLine = _mapper.Map<DelayedChargeLine>(model);

                //save and return
                if (!await _repository.StoreNewAsync(DelayedChargeLine))
                {
                    return BadRequest("Bad request, could not create record!");
                }
                else
                {
                    var location = _linkGenerator.GetPathByAction("Get",
                             "DelayedChargeLine",
                            new { Id = DelayedChargeLine.Id });

                    return Created(location, _mapper.Map<DelayedChargeLineModel>(DelayedChargeLine));
                }

            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Failed Request");
            }
        }

        [HttpPut("{Id}")]
        public async Task<ActionResult<DelayedChargeLineModel>> Put(int delayedchargeId, int Id, DelayedChargeLineModel updatedModel)
        {
            try
            {
                SetupPath(delayedchargeId);

                var currentDelayedChargeLine = await _repository.GetByIdAsync(Id);
                if (currentDelayedChargeLine == null) return NotFound($"Could not find DelayedChargeLine with Id of {Id}");

                _mapper.Map(updatedModel, currentDelayedChargeLine);

                if (await _repository.UpdateAsync(currentDelayedChargeLine))
                {
                    return _mapper.Map<DelayedChargeLineModel>(currentDelayedChargeLine);
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
        public async Task<IActionResult> Delete(int delayedchargeId, int Id)
        {
            try
            {
                SetupPath(delayedchargeId);

                var DelayedChargeLine = await _repository.GetByIdAsync(Id);
                if (DelayedChargeLine == null) return NotFound();

                if (await _repository.DeleteAsync(DelayedChargeLine))
                {
                    return Ok();
                }

            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Failed Request");
            }

            return BadRequest("Failed to delete the DelayedChargeLine");
        }

    }
}