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
    public class AbsenceController : ControllerBase
    {
        private readonly IAbsenceRepository _repository;
        private readonly IMapper _mapper;
        private readonly LinkGenerator _linkGenerator;
        private readonly ILogger<AbsenceController> _logger;

        public AbsenceController(IAbsenceRepository repository, IMapper mapper, LinkGenerator linkGenerator,
            ILogger<AbsenceController> logger)
        {
            _repository = repository;
            _mapper = mapper;
            _linkGenerator = linkGenerator;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<AbsenceModel[]>> GetAll(int employeeId)
        {
            try
            {
                var results = await _repository.GetAllAbsencesAsync(employeeId);

                return _mapper.Map<AbsenceModel[]>(results);
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
        }

        [HttpGet("{Id}")]
        public async Task<ActionResult<AbsenceModel>> Get(int employeeId, int Id)
        {
            try
            {
                var result = await _repository.GetAbsenceAsync(employeeId, Id);

                if (result == null) return NotFound();

                return _mapper.Map<AbsenceModel>(result);

            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
        }

        [HttpPost]
        public async Task<ActionResult<AbsenceModel>> Post(int employeeId, AbsenceModel model)
        {
            try
            {
                //Make sure AbsenceId is not already taken
                var existing = await _repository.GetAbsenceAsync(employeeId, model.Id);
                if (existing != null)
                {
                    return BadRequest("Absence Id in Use");
                }

                //map
                var Absence = _mapper.Map<Absence>(model);

                //save and return
                if (!await _repository.StoreNewAbsenceAsync(employeeId, Absence))
                {
                    return BadRequest("Bad request, could not create record!");
                }
                else
                {
                    var location = _linkGenerator.GetPathByAction("Get",
                             "Absence",
                            new { employeeId = Absence.EmployeeId, Absence.Id });

                    return Created(location, _mapper.Map<AbsenceModel>(Absence));
                }

            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
        }

        [HttpPut("{Id}")]
        public async Task<ActionResult<AbsenceModel>> Put(int employeeId, int Id, AbsenceModel updatedModel)
        {
            try
            {
                var currentAbsence = await _repository.GetAbsenceAsync(employeeId, Id);
                if (currentAbsence == null) return NotFound($"Could not find Absence with Id of {Id}");

                _mapper.Map(updatedModel, currentAbsence);

                if (await _repository.UpdateAbsenceAsync(employeeId, currentAbsence))
                {
                    return _mapper.Map<AbsenceModel>(currentAbsence);
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
                var Absence = await _repository.GetAbsenceAsync(employeeId, Id);
                if (Absence == null) return NotFound();

                if (await _repository.DeleteAbsence(Absence))
                {
                    return Ok();
                }

            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }

            return BadRequest("Failed to delete the Absence");
        }

    }
}