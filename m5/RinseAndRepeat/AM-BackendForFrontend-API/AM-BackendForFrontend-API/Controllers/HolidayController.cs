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
    public class HolidayController : ControllerBase
    {
        private readonly IEmployeeManagerRepository<Holiday> _repository;
        private readonly IMapper _mapper;
        private readonly LinkGenerator _linkGenerator;
        private readonly ILogger<HolidayController> _logger;

        public HolidayController(IEmployeeManagerRepository<Holiday> repository, IMapper mapper, LinkGenerator linkGenerator,
            ILogger<HolidayController> logger)
        {
            _repository = repository;
            _mapper = mapper;
            _linkGenerator = linkGenerator;
            _logger = logger;
        }

        private void SetupPath(int employeeId)
        {
            _repository.ResourcePath = $"api/employee/{employeeId}/Holiday";
        }

        [HttpGet]
        public async Task<ActionResult<HolidayModel[]>> Get(int employeeId)
        {
            try
            {
                SetupPath(employeeId);

                var results = await _repository.GetAllAsync();

                return _mapper.Map<HolidayModel[]>(results);
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Failed Request");
            }
        }

        [HttpGet("{Id}")]
        public async Task<ActionResult<HolidayModel>> Get(int employeeId, int Id)
        {
            try
            {
                SetupPath(employeeId);

                var result = await _repository.GetByIdAsync(Id);

                if (result == null) return NotFound();

                return _mapper.Map<HolidayModel>(result);

            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Failed Request");
            }
        }

        [HttpPost]
        public async Task<ActionResult<HolidayModel>> Post(int employeeId, HolidayModel model)
        {
            try
            {
                SetupPath(employeeId);

                //Make sure HolidayId is not already taken
                var existing = await _repository.GetByIdAsync(model.Id);
                if (existing != null)
                {
                    return BadRequest("Holiday Id in Use");
                }

                //map
                var Holiday = _mapper.Map<Holiday>(model);

                //save and return
                if (!await _repository.StoreNewAsync(Holiday))
                {
                    return BadRequest("Bad request, could not create record!");
                }
                else
                {
                    var location = _linkGenerator.GetPathByAction("Get",
                             "Holiday",
                            new { Id = Holiday.Id });

                    return Created(location, _mapper.Map<HolidayModel>(Holiday));
                }

            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Failed Request");
            }
        }

        [HttpPut("{Id}")]
        public async Task<ActionResult<HolidayModel>> Put(int employeeId, int Id, HolidayModel updatedModel)
        {
            try
            {
                SetupPath(employeeId);

                var currentHoliday = await _repository.GetByIdAsync(Id);
                if (currentHoliday == null) return NotFound($"Could not find Holiday with Id of {Id}");

                _mapper.Map(updatedModel, currentHoliday);

                if (await _repository.UpdateAsync(currentHoliday))
                {
                    return _mapper.Map<HolidayModel>(currentHoliday);
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

                var Holiday = await _repository.GetByIdAsync(Id);
                if (Holiday == null) return NotFound();

                if (await _repository.DeleteAsync(Holiday))
                {
                    return Ok();
                }

            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Failed Request");
            }

            return BadRequest("Failed to delete the Holiday");
        }

    }
}