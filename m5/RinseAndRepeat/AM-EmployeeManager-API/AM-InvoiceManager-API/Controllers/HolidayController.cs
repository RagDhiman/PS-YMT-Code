using AM_EmployeeManager_API.Models;
using AM_EmployeeManager_Core;
using AM_EmployeeManager_Core.DataAccess;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
namespace AM_CustomerManager_API.Controllers
{
    [Route("api/employee/{employeeId}/[controller]")]
    [ApiController]
    public class HolidayController : ControllerBase
    {
        private readonly IHolidayRepository _repository;
        private readonly IMapper _mapper;
        private readonly LinkGenerator _linkGenerator;
        private readonly ILogger<HolidayController> _logger;

        public HolidayController(IHolidayRepository repository, IMapper mapper, LinkGenerator linkGenerator,
            ILogger<HolidayController> logger)
        {
            _repository = repository;
            _mapper = mapper;
            _linkGenerator = linkGenerator;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<HolidayModel[]>> Get(int employeeId)
        {
            try
            {
                var results = await _repository.GetAllHolidayesAsync(employeeId);

                return _mapper.Map<HolidayModel[]>(results);
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
        }

        [HttpGet("{Id}")]
        public async Task<ActionResult<HolidayModel>> Get(int employeeId, int Id)
        {
            try
            {
                var result = await _repository.GetHolidayAsync(employeeId, Id);

                if (result == null) return NotFound();

                return _mapper.Map<HolidayModel>(result);

            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
        }

        [HttpPost]
        public async Task<ActionResult<HolidayModel>> Post(int employeeId, HolidayModel model)
        {
            try
            {
                //Make sure HolidayId is not already taken
                var existing = await _repository.GetHolidayAsync(employeeId, model.Id);
                if (existing != null)
                {
                    return BadRequest("Holiday Id in Use");
                }

                //map
                var Holiday = _mapper.Map<Holiday>(model);

                //save and return
                if (!await _repository.StoreNewHolidayAsync(employeeId, Holiday))
                {
                    return BadRequest("Bad request, could not create record!");
                }
                else
                {
                    var location = _linkGenerator.GetPathByAction("Get",
                             "Holiday",
                            new { employeeId = Holiday.EmployeeId, Holiday.Id });

                    return Created(location, _mapper.Map<HolidayModel>(Holiday));
                }

            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
        }

        [HttpPut("{Id}")]
        public async Task<ActionResult<HolidayModel>> Put(int employeeId, int Id, HolidayModel updatedModel)
        {
            try
            {
                var currentHoliday = await _repository.GetHolidayAsync(employeeId, Id);
                if (currentHoliday == null) return NotFound($"Could not find Holiday with Id of {Id}");

                _mapper.Map(updatedModel, currentHoliday);

                if (await _repository.UpdateHolidayAsync(employeeId, currentHoliday))
                {
                    return _mapper.Map<HolidayModel>(currentHoliday);
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
                var Holiday = await _repository.GetHolidayAsync(employeeId, Id);
                if (Holiday == null) return NotFound();

                if (await _repository.DeleteHoliday(Holiday))
                {
                    return Ok();
                }

            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }

            return BadRequest("Failed to delete the Holiday");
        }

    }
}