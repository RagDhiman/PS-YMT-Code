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
    public class AbsenceController : ControllerBase
    {
        private readonly IAccountManagerRepository<Absence> _repository;
        private readonly IMapper _mapper;
        private readonly LinkGenerator _linkGenerator;
        private readonly ILogger<AbsenceController> _logger;

        public AbsenceController(IAccountManagerRepository<Absence> repository, IMapper mapper, LinkGenerator linkGenerator,
            ILogger<AbsenceController> logger)
        {
            _repository = repository;
            _mapper = mapper;
            _linkGenerator = linkGenerator;
            _logger = logger;
        }

        private void SetupPath(int employeeId)
        {
            _repository.ResourcePath = $"api/employee/{employeeId}/absence";
        }

        [HttpGet]
        public async Task<ActionResult<AbsenceModel[]>> GetAll(int employeeId)
        {
            try
            {
                SetupPath(employeeId);

                var results = await _repository.GetAllAsync();

                return _mapper.Map<AbsenceModel[]>(results);
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Failed Request");
            }
        }

        [HttpGet("{Id}")]
        public async Task<ActionResult<AbsenceModel>> Get(int employeeId, int Id)
        {
            try
            {
                SetupPath(employeeId);

                var result = await _repository.GetByIdAsync(Id);

                if (result == null) return NotFound();

                return _mapper.Map<AbsenceModel>(result);

            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Failed Request");
            }
        }

        [HttpPost]
        public async Task<ActionResult<AbsenceModel>> Post(int employeeId, AbsenceModel model)
        {
            try
            {
                SetupPath(employeeId);

                //Make sure AbsenceId is not already taken
                var existing = await _repository.GetByIdAsync(model.Id);
                if (existing != null)
                {
                    return BadRequest("Absence Id in Use");
                }

                //map
                var Absence = _mapper.Map<Absence>(model);

                //save and return
                if (!await _repository.StoreNewAsync(Absence))
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
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Failed Request");
            }
        }

        [HttpPut("{Id}")]
        public async Task<ActionResult<AbsenceModel>> Put(int employeeId, int Id, AbsenceModel updatedModel)
        {
            try
            {
                SetupPath(employeeId);

                var currentAbsence = await _repository.GetByIdAsync(Id);
                if (currentAbsence == null) return NotFound($"Could not find Absence with Id of {Id}");

                _mapper.Map(updatedModel, currentAbsence);

                if (await _repository.UpdateAsync(currentAbsence))
                {
                    return _mapper.Map<AbsenceModel>(currentAbsence);
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

                var Absence = await _repository.GetByIdAsync(Id);
                if (Absence == null) return NotFound();

                if (await _repository.DeleteAsync(Absence))
                {
                    return Ok();
                }

            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Failed Request");
            }

            return BadRequest("Failed to delete the Absence");
        }

    }
}