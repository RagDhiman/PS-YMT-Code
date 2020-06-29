using AccountsManager_API.Models;
using AutoMapper;
using EmployeesManager_Domain.DataAccess;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using AccountsManager_Domain;

namespace EmployeesManager_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeRepository _repository;
        private readonly IMapper _mapper;
        private readonly LinkGenerator _linkGenerator;
        private readonly ILogger<EmployeeController> _logger;

        public EmployeeController(IEmployeeRepository repository, IMapper mapper, LinkGenerator linkGenerator,
            ILogger<EmployeeController> logger)
        {
            _repository = repository;
            _mapper = mapper;
            _linkGenerator = linkGenerator;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<EmployeeModel[]>> Get()
        {
            try
            {
                var results = await _repository.GetAllEmployeesAsync();

                return _mapper.Map<EmployeeModel[]>(results);
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
        }

        [HttpGet("{Id}")]
        public async Task<ActionResult<EmployeeModel>> Get(int Id)
        {
            try
            {
                var result = await _repository.GetEmployeeAsync(Id);

                if (result == null) return NotFound();

                return _mapper.Map<EmployeeModel>(result);

            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
        }

        [HttpPost]
        public async Task<ActionResult<EmployeeModel>> Post(EmployeeModel model)
        {
            try
            {
                //Make sure EmployeeId is not already taken
                var existing = await _repository.GetEmployeeAsync(model.Id);
                if (existing != null)
                {
                    return BadRequest("Employee Id in Use");
                }

                //map
                var Employee = _mapper.Map<Employee>(model);

                //save and return
                if (!await _repository.StoreNewEmployeeAsync(Employee))
                {
                    return BadRequest("Bad request, could not create record!");
                }
                else
                {
                    var location = _linkGenerator.GetPathByAction("Get",
                             "Employee",
                            new { Id = Employee.Id });

                    return Created(location, _mapper.Map<EmployeeModel>(Employee));
                }

            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
        }

        [HttpPut("{Id}")]
        public async Task<ActionResult<EmployeeModel>> Put(int Id, EmployeeModel updatedModel)
        {
            try
            {
                var currentEmployee = await _repository.GetEmployeeAsync(Id);
                if (currentEmployee == null) return NotFound($"Could not find Employee with Id of {Id}");

                _mapper.Map(updatedModel, currentEmployee);

                if (await _repository.UpdateEmployeeAsync(currentEmployee))
                {
                    return _mapper.Map<EmployeeModel>(currentEmployee);
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
        public async Task<IActionResult> Delete(int Id)
        {
            try
            {
                var Employee = await _repository.GetEmployeeAsync(Id);
                if (Employee == null) return NotFound();

                if (await _repository.DeleteEmployee(Employee))
                {
                    return Ok();
                }

            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }

            return BadRequest("Failed to delete the Employee");
        }

    }
}