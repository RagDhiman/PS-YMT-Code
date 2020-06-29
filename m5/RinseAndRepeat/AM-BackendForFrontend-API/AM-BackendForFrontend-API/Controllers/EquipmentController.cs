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
    public class EquipmentController : ControllerBase
    {
        private readonly IEmployeeManagerRepository<Equipment> _repository;
        private readonly IMapper _mapper;
        private readonly LinkGenerator _linkGenerator;
        private readonly ILogger<EquipmentController> _logger;

        public EquipmentController(IEmployeeManagerRepository<Equipment> repository, IMapper mapper, LinkGenerator linkGenerator,
            ILogger<EquipmentController> logger)
        {
            _repository = repository;
            _mapper = mapper;
            _linkGenerator = linkGenerator;
            _logger = logger;
        }

        private void SetupPath(int employeeId)
        {
            _repository.ResourcePath = $"api/employee/{employeeId}/Equipment";
        }

        [HttpGet]
        public async Task<ActionResult<EquipmentModel[]>> Get(int employeeId)
        {
            try
            {
                SetupPath(employeeId);

                var results = await _repository.GetAllAsync();

                return _mapper.Map<EquipmentModel[]>(results);
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Failed Request");
            }
        }

        [HttpGet("{Id}")]
        public async Task<ActionResult<EquipmentModel>> Get(int employeeId, int Id)
        {
            try
            {
                SetupPath(employeeId);

                var result = await _repository.GetByIdAsync(Id);

                if (result == null) return NotFound();

                return _mapper.Map<EquipmentModel>(result);

            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Failed Request");
            }
        }

        [HttpPost]
        public async Task<ActionResult<EquipmentModel>> Post(int employeeId, EquipmentModel model)
        {
            try
            {
                SetupPath(employeeId);

                //Make sure EquipmentId is not already taken
                var existing = await _repository.GetByIdAsync(model.Id);
                if (existing != null)
                {
                    return BadRequest("Equipment Id in Use");
                }

                //map
                var Equipment = _mapper.Map<Equipment>(model);

                //save and return
                if (!await _repository.StoreNewAsync(Equipment))
                {
                    return BadRequest("Bad request, could not create record!");
                }
                else
                {
                    var location = _linkGenerator.GetPathByAction("Get",
                             "Equipment",
                            new { Id = Equipment.Id });

                    return Created(location, _mapper.Map<EquipmentModel>(Equipment));
                }

            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Failed Request");
            }
        }

        [HttpPut("{Id}")]
        public async Task<ActionResult<EquipmentModel>> Put(int employeeId, int Id, EquipmentModel updatedModel)
        {
            try
            {
                SetupPath(employeeId);

                var currentEquipment = await _repository.GetByIdAsync(Id);
                if (currentEquipment == null) return NotFound($"Could not find Equipment with Id of {Id}");

                _mapper.Map(updatedModel, currentEquipment);

                if (await _repository.UpdateAsync(currentEquipment))
                {
                    return _mapper.Map<EquipmentModel>(currentEquipment);
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

                var Equipment = await _repository.GetByIdAsync(Id);
                if (Equipment == null) return NotFound();

                if (await _repository.DeleteAsync(Equipment))
                {
                    return Ok();
                }

            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Failed Request");
            }

            return BadRequest("Failed to delete the Equipment");
        }

    }
}