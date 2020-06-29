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
    [Route("api/employee/{employeeId}/[controller]")]
    [ApiController]
    public class TrainingController : ControllerBase
    {
        private readonly IAccountManagerRepository<Training> _repository;
        private readonly IMapper _mapper;
        private readonly LinkGenerator _linkGenerator;
        private readonly ILogger<TrainingController> _logger;

        public TrainingController(IAccountManagerRepository<Training> repository, IMapper mapper, LinkGenerator linkGenerator,
            ILogger<TrainingController> logger)
        {
            _repository = repository;
            _mapper = mapper;
            _linkGenerator = linkGenerator;
            _logger = logger;
        }

        private void SetupPath(int employeeId)
        {
            _repository.ResourcePath = $"api/employee/{employeeId}/Training";
        }

        [HttpGet]
        public async Task<ActionResult<TrainingModel[]>> Get(int employeeId)
        {
            try
            {
                SetupPath(employeeId);

                var results = await _repository.GetAllAsync();

                return _mapper.Map<TrainingModel[]>(results);
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Failed Request");
            }
        }

        [HttpGet("{Id}")]
        public async Task<ActionResult<TrainingModel>> Get(int employeeId, int Id)
        {
            try
            {
                SetupPath(employeeId);

                var result = await _repository.GetByIdAsync(Id);

                if (result == null) return NotFound();

                return _mapper.Map<TrainingModel>(result);

            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Failed Request");
            }
        }

        [HttpPost]
        public async Task<ActionResult<TrainingModel>> Post(int employeeId, TrainingModel model)
        {
            try
            {
                SetupPath(employeeId);

                //Make sure TrainingId is not already taken
                var existing = await _repository.GetByIdAsync(model.Id);
                if (existing != null)
                {
                    return BadRequest("Training Id in Use");
                }

                //map
                var Training = _mapper.Map<Training>(model);

                //save and return
                if (!await _repository.StoreNewAsync(Training))
                {
                    return BadRequest("Bad request, could not create record!");
                }
                else
                {
                    var location = _linkGenerator.GetPathByAction("Get",
                             "Training",
                            new { Id = Training.Id });

                    return Created(location, _mapper.Map<TrainingModel>(Training));
                }

            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Failed Request");
            }
        }

        [HttpPut("{Id}")]
        public async Task<ActionResult<TrainingModel>> Put(int employeeId, int Id, TrainingModel updatedModel)
        {
            try
            {
                SetupPath(employeeId);

                var currentTraining = await _repository.GetByIdAsync(Id);
                if (currentTraining == null) return NotFound($"Could not find Training with Id of {Id}");

                _mapper.Map(updatedModel, currentTraining);

                if (await _repository.UpdateAsync(currentTraining))
                {
                    return _mapper.Map<TrainingModel>(currentTraining);
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
        public async Task<IActionResult> Delete(int employeeId, int Id)
        {
            try
            {
                SetupPath(employeeId);

                var Training = await _repository.GetByIdAsync(Id);
                if (Training == null) return NotFound();

                if (await _repository.DeleteAsync(Training))
                {
                    return Ok();
                }

            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Failed Request");
            }

            return BadRequest("Failed to delete the Training");
        }

    }
}