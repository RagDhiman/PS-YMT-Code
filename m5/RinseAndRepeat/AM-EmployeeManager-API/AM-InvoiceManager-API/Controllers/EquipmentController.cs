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
    public class EquipmentController : ControllerBase
    {
        private readonly IEquipmentRepository _repository;
        private readonly IMapper _mapper;
        private readonly LinkGenerator _linkGenerator;
        private readonly ILogger<EquipmentController> _logger;

        public EquipmentController(IEquipmentRepository repository, IMapper mapper, LinkGenerator linkGenerator,
            ILogger<EquipmentController> logger)
        {
            _repository = repository;
            _mapper = mapper;
            _linkGenerator = linkGenerator;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<EquipmentModel[]>> Get(int employeeId)
        {
            try
            {
                var results = await _repository.GetAllEquipmentesAsync(employeeId);

                return _mapper.Map<EquipmentModel[]>(results);
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
        }

        [HttpGet("{Id}")]
        public async Task<ActionResult<EquipmentModel>> Get(int employeeId, int Id)
        {
            try
            {
                var result = await _repository.GetEquipmentAsync(employeeId, Id);

                if (result == null) return NotFound();

                return _mapper.Map<EquipmentModel>(result);

            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
        }

        [HttpPost]
        public async Task<ActionResult<EquipmentModel>> Post(int employeeId, EquipmentModel model)
        {
            try
            {
                //Make sure EquipmentId is not already taken
                var existing = await _repository.GetEquipmentAsync(employeeId, model.Id);
                if (existing != null)
                {
                    return BadRequest("Equipment Id in Use");
                }

                //map
                var Equipment = _mapper.Map<Equipment>(model);

                //save and return
                if (!await _repository.StoreNewEquipmentAsync(employeeId, Equipment))
                {
                    return BadRequest("Bad request, could not create record!");
                }
                else
                {
                    var location = _linkGenerator.GetPathByAction("Get",
                             "Equipment",
                            new { employeeId = Equipment.EmployeeId, Equipment.Id });

                    return Created(location, _mapper.Map<EquipmentModel>(Equipment));
                }

            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
        }

        [HttpPut("{Id}")]
        public async Task<ActionResult<EquipmentModel>> Put(int employeeId, int Id, EquipmentModel updatedModel)
        {
            try
            {
                var currentEquipment = await _repository.GetEquipmentAsync(employeeId, Id);
                if (currentEquipment == null) return NotFound($"Could not find Equipment with Id of {Id}");

                _mapper.Map(updatedModel, currentEquipment);

                if (await _repository.UpdateEquipmentAsync(employeeId, currentEquipment))
                {
                    return _mapper.Map<EquipmentModel>(currentEquipment);
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
                var Equipment = await _repository.GetEquipmentAsync(employeeId, Id);
                if (Equipment == null) return NotFound();

                if (await _repository.DeleteEquipment(Equipment))
                {
                    return Ok();
                }

            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }

            return BadRequest("Failed to delete the Equipment");
        }

    }
}