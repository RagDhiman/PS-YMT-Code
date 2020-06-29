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
    [Route("api/employee/{employeeId}/[controller]")]
    [ApiController]
    public class TrainingController : ControllerBase
    {
        private readonly ITrainingRepository _repository;
        private readonly IMapper _mapper;
        private readonly LinkGenerator _linkGenerator;
        private readonly ILogger<TrainingController> _logger;

        public TrainingController(ITrainingRepository repository, IMapper mapper, LinkGenerator linkGenerator,
            ILogger<TrainingController> logger)
        {
            _repository = repository;
            _mapper = mapper;
            _linkGenerator = linkGenerator;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<TrainingModel[]>> GetAll(int employeeId)
        {
            try
            {
                var results = await _repository.GetAllTrainingsAsync(employeeId);

                return _mapper.Map<TrainingModel[]>(results);
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
        }

        [HttpGet("{Id}")]
        public async Task<ActionResult<TrainingModel>> Get(int employeeId, int Id)
        {
            try
            {
                var result = await _repository.GetTrainingAsync(employeeId, Id);

                if (result == null) return NotFound();

                return _mapper.Map<TrainingModel>(result);

            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
        }

        [HttpPost]
        public async Task<ActionResult<TrainingModel>> Post(int employeeId, TrainingModel model)
        {
            try
            {
                //Make sure TrainingId is not already taken
                var existing = await _repository.GetTrainingAsync(employeeId, model.Id);
                if (existing != null)
                {
                    return BadRequest("Training Id in Use");
                }

                //map
                var Training = _mapper.Map<Training>(model);

                //save and return
                if (!await _repository.StoreNewTrainingAsync(employeeId, Training))
                {
                    return BadRequest("Bad request, could not create record!");
                }
                else
                {
                    var location = _linkGenerator.GetPathByAction("Get",
                             "Training",
                            new { employeeId = Training.EmployeeId, Training.Id });

                    return Created(location, _mapper.Map<TrainingModel>(Training));
                }

            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
        }

        [HttpPut("{Id}")]
        public async Task<ActionResult<TrainingModel>> Put(int employeeId, int Id, TrainingModel updatedModel)
        {
            try
            {
                var currentTraining = await _repository.GetTrainingAsync(employeeId, Id);
                if (currentTraining == null) return NotFound($"Could not find Training with Id of {Id}");

                _mapper.Map(updatedModel, currentTraining);

                if (await _repository.UpdateTrainingAsync(employeeId, currentTraining))
                {
                    return _mapper.Map<TrainingModel>(currentTraining);
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
        public async Task<IActionResult> Delete(int employeeId, int Id)
        {
            try
            {
                var Training = await _repository.GetTrainingAsync(employeeId, Id);
                if (Training == null) return NotFound();

                if (await _repository.DeleteTraining(Training))
                {
                    return Ok();
                }

            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }

            return BadRequest("Failed to delete the Training");
        }

    }
}